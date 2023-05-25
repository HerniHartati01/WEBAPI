using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Contexts;
using WEBAPI.Contracts;
using WEBAPI.Models;
using WEBAPI.ViewModels.Accounts;

namespace WEBAPI.Repositories
{
    public class AccountRepository : RepositoryGeneric<Account>, IAccountRepository
    {


        public AccountRepository(BookingMangementDbContext context) : base(context) { }

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



    }
}
