using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.Utility;
using WEBAPI.ViewModels.Accounts;
using WEBAPI.ViewModels.Login;

namespace WEBAPI.Repositories
{
    public class AccountRepository : RepositoryGeneric<Account>, IAccountRepository
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEducationRepository _educationRepository;
        public AccountRepository(BookingMangementDbContext context,
            IUniversityRepository universityRepository,
            IEmployeeRepository employeeRepository,
            IRoleRepository roleRepository,
            IAccountRoleRepository accountRoleRepository,
            IEducationRepository educationRepository) : base(context)
        {
            _universityRepository = universityRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
        }

        public AccountEmpVM Login(LoginVM loginVM)
        {
            var account = GetAll();
            var employee = _employeeRepository.GetAll();
            var query = from emp in employee
                        join acc in account
                        on emp.Guid equals acc.Guid
                        where emp.Email == loginVM.Email
                        select new AccountEmpVM
                        {
                            Email = emp.Email,
                            Password = acc.Password

                        };
            return query.FirstOrDefault();
        }

        public int ChangePasswordById(Guid? employeeId, ChangePasswordVM changePasswordVM)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            if (account == null || account.Otp != changePasswordVM.Otp)
            {
                return 2;
            }

            // Cek apakah OTP sudah digunakan
            if (account.IsUsed)
            {
                return 3;
            }

            // Cek apakah OTP sudah expired
            /*if (account.ExpiredTime < DateTime.Now)
            {
                return 4;
            }*/

            // Cek apakah NewPassword dan ConfirmPassword sesuai
            if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
            {
                return 5;
            }

            // Update password
            account.Password = changePasswordVM.NewPassword;
            account.IsUsed = true;
            try
            {
                var updatePassword = Update(account);
                if (!updatePassword)
                {
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public bool UpdatePassword(Account account)
        {
            try
            {
                _context.Entry(account).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int UpdateOTP(Guid? employeeId)
        {
            var account = new Account();
            account = _context.Set<Account>().FirstOrDefault(a => a.Guid == employeeId);
            //Generate OTP
            Random rnd = new Random();
            var getOtp = rnd.Next(100000, 999999);
            account.Otp = getOtp;

            //Add 5 minutes to expired time
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            try
            {
                var check = Update(account);


                if (!check)
                {
                    return 0;
                }
                return getOtp;
            }
            catch
            {
                return 0;
            }
        }

        // coba

        public int Register(RegisterVM registerVM)
        {
            try
            {
                var university = new University
                {
                    Code = registerVM.UniversityCode,
                    Name = registerVM.UniversityName

                };
                _universityRepository.CreateWithValidate(university);

                var employee = new Employee
                {
                    Nik = GenerateNIK(),
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    BirthDate = registerVM.BirthDate,
                    Gender = registerVM.Gender,
                    HiringDate = registerVM.HiringDate,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                };

                var result = _employeeRepository.Create(employee);

                /*if (result != 3)
                {
                    return result;
                }*/

                var education = new Education
                {
                    Guid = employee.Guid,
                    Major = registerVM.Major,
                    Degree = registerVM.Degree,
                    Gpa = registerVM.GPA,
                    UniversityGuid = university.Guid
                };
                _educationRepository.Create(education);

                // Proses Hasing untuk password
                var hashingPass = Hashing.HashPassword(registerVM.Password);
                var account = new Account
                {
                    Guid = employee.Guid,
                    Password = hashingPass,
                    IsDeleted = false,
                    IsUsed = true,
                    Otp = 0
                };

                Create(account);

                var accountRole = new AccountRole
                {
                    RoleGuid = Guid.Parse("e68b96e8-2279-4b4d-262e-08db60bf5fd4"),
                    AccountGuid = employee.Guid
                };
                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();

                return 3;

            }
            catch
            {
                return 0;
            }

        }

        private string GenerateNIK()
        {
            var lastNik = _employeeRepository.GetAll().OrderByDescending(e => int.Parse(e.Nik)).FirstOrDefault();

            if (lastNik != null)
            {
                int lastNikNumber;
                if (int.TryParse(lastNik.Nik, out lastNikNumber))
                {
                    return (lastNikNumber + 1).ToString();
                }
            }

            return "100000";
        }

        public IEnumerable<string> GetRoles(Guid guid)
        {
            var accountrole = _accountRoleRepository.GetAll();
            var roles = _roleRepository.GetAll();

            var getAccount = GetByGuid(guid);
            if (getAccount == null) return Enumerable.Empty<string>();
            var GetRoles = from a in accountrole
                           join r in roles on
                           a.RoleGuid equals r.Guid
                           where a.AccountGuid == guid
                           select r.Name;

            return GetRoles.ToList();

        }
    }
}
