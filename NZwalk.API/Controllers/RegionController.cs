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
            foreach (var RegionDomain in regions)
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
        //create region
        [HttpPost]
        public IActionResult AddRegion(Model.DTO.AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomain = new Model.Domain.Region
            {
                Name = addRegionRequestDTO.Name,
                Code = addRegionRequestDTO.Code,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();
            var regionDto = new Model.DTO.RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

        }
    }
}
