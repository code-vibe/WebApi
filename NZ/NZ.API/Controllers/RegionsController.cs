using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZ.API.Data;
using NZ.API.Domain.Models;
using NZ.API.Domain.Response;

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

        //Get All Uers
        [HttpPost]
        [Route("{id}")]
       public IActionResult GetUserById(Guid id)
        {
            var regionById = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (regionById == null)
            {
                return NotFound();
            }
            //Map it to the client response 
            var regionResponse = new RegionsResponse()
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
    }
}
