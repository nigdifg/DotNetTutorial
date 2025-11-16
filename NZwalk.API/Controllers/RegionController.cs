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

            var RegionDtos = new List<Model.DTO.RegionDto>();
            foreach(var RegionDomain in regions)
            {
                RegionDtos.Add(new Model.DTO.RegionDto
                {
                    Id = RegionDomain.Id,
                    Name = RegionDomain.Name,
                    Code = RegionDomain.Code,
                    RegionImageUrl = RegionDomain.RegionImageUrl
                });
            }

            return Ok(RegionDtos);
        }

        //get fetch region by ID
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            var RegionDto = new Model.DTO.RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(RegionDto);
        }

    }
}
