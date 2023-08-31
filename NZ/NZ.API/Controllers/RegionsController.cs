using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZ.API.Data;
using NZ.API.Domain.DTOs.Request;
using NZ.API.Domain.DTOs.Response;
using NZ.API.Domain.Models;

namespace NZ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZDbContext dbContext;
        public RegionsController(NZDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get Uer by Id
        [HttpGet]
        [Route("{id}")]
       public IActionResult GetUserById([FromRoute] Guid id)
        {
            var regionById = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (regionById == null)
            {
                return NotFound();
            }
            //Map it to the client response 
            var regionResponse = new RegionsResponse
            {
                Id = regionById.Id,
                Code = regionById.Code,
                Name = regionById.Name,
                RegionImageUrl = regionById.RegionImageUrl,
            };
            return Ok(regionResponse);

        }

        //Create a GetAll Method
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var allRegions = dbContext.Regions.ToList();
            if (allRegions == null)
            {
                return NotFound();
            }

            //Map it to the client response
            var regionResponse = new List<RegionsResponse>();
            foreach (var region in allRegions)
            {
                regionResponse.Add(new RegionsResponse()
                {
                    Id = region.Id,
                    RegionImageUrl = region.RegionImageUrl,
                    Name = region.Name,
                    Code = region.Code,
                });
            }
            return Ok(regionResponse);
        }
        [HttpPost]
        public IActionResult CreateRegion([FromBody] CreateRegionRequest createRegionRequest )
        {
            //Convert DTO to Domain Model
            var region = new Regions
            {
                Code = createRegionRequest.Code,
                RegionImageUrl = createRegionRequest.RegionImageUrl,
                Name = createRegionRequest.Name,
            };
            //Use Domain Model to create Region
            dbContext.Add(region);
            dbContext.SaveChangesAsync();

            //Convert Domain Model to DTO
            var response = new RegionsResponse
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl

            };
            return CreatedAtAction(nameof(GetUserById), new {id = region.Id}, region);


        }

        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest )
        {
            //Check if region exist
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            
            if (region == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model
            region.Code = updateRegionRequest.Code;
            region.Name = updateRegionRequest.Name;
            region.RegionImageUrl = updateRegionRequest.RegionImageUrl;

            dbContext.SaveChanges();

            //Map Domain models to Dto
            var response = new RegionsResponse
            {
                Id = region.Id,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
                Code = region.Code,
            };

            return Ok(response);

        }

    }
}
