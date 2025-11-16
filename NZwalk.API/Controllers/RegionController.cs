using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZwalk.API.Data;
using NZwalk.API.Model.Domain;

namespace NZwalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZwalkDBContext dbContext;

        public RegionController(NZwalkDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {


            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }
    }
}
