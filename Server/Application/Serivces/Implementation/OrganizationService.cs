using Core.Entity;
using Core.Repository.Abstractions;
using Core.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Serivces.Implementation
{
    public class OrganizationService : IOrganizationService
    {
        private  IOrganizationRepository organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        public async Task<List<Organization>> GetAllOrganizationsAsync()
        {
           return await organizationRepository.GetAllAsync();
        }
    }
}
