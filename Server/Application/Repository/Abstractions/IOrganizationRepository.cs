using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Abstractions
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<Organization> GetByNameAsync(string name);
    }
}
