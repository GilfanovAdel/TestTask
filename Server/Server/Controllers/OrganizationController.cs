using Core.Dto;
using Core.Entity;
using Core.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("GetallOrganizations")]
        public async Task<List<Organization>> GetAllOrganizations()
        {
           return await _organizationService.GetAllOrganizationsAsync();
        }
         
    }
}
