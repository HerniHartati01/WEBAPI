﻿using WEBAPI.Models;

namespace WEBAPI.Contracts
{
    public interface IEducationRepository
    {
        Education Create(Education education);
        bool Update(Education education);
        bool Delete(Guid guid);
        IEnumerable<Education> GetAll();
        Education? GetByGuid(Guid guid);
    }
}
