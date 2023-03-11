using Entities.DTO_s.Reponses;
using Entities.DTO_s.Requests;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace spot_a_hub.Controllers
{
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        public HubController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }


        [HttpGet]
        [Route("/api/gethubs")]
        public ActionResult GetAllHubs()
        {
            var getHubs = _repositoryManager.Hubb.GetHubs(trackChanges: false);

			return Ok(new ApiResponseDTO<string> { Code = 200, Status = "success", Data = getHubs });
		}


        [HttpGet]
        [Route("/api/getbyname")]
        public async Task<ActionResult> GetHubByName(string name)
        {
            var getHubByName = await _repositoryManager.Hubb.GetHubbByName(name.ToLower(), trackchanges: false);

            if (getHubByName == null)
            {
                return NotFound(new ApiResponseDTO<string> { Code = 404, Status = "not found", Message = $"hub with the name '{name.ToUpper()}' not found" });
            }

            return Ok(new ApiResponseDTO<string> { Code = 200, Status = "success", Message = $"'{name.ToUpper()}' found",  Data = getHubByName });
        }


        [HttpGet]
        [Route("/api/getbystate")]
        public async Task<ActionResult> GetHubsByState(string state)
        {
            var getHubsByState = await _repositoryManager.Hubb.GetHubbsByState(state.ToLower(), trackchanges : false);

            if (getHubsByState == null)
            {
				return NotFound(new ApiResponseDTO<string> { Code = 404, Status = "not found", Message = $"hub located in {state.ToUpper()} not found" });
            }

            var displayHubsInfo = GetHubDetails(getHubsByState);

			return Ok(new ApiResponseDTO<string> { Code = 200, Status = "success", Message = $"'hub by {state.ToUpper()} found", Data = displayHubsInfo });

		}


		[HttpGet]
        [Route("/api/getbytag")]
        public async Task<ActionResult> GetHubsByTag(string tag)
        {
            var getHubsByTag = await _repositoryManager.Hubb.GetHubbsByTag(tag.ToLower(), trackchanges: false);

            if (getHubsByTag == null)
            {
				return NotFound(new ApiResponseDTO<string> { Code = 404, Status = "not found", Message = $"hub with the tag(s) {tag.ToUpper()} not found" });
			}

            var displayHubsInfo = GetHubDetails(getHubsByTag);

			return Ok(new ApiResponseDTO<string> { Code = 200, Status = "success", Message = $"hub with the tag(s) {tag.ToUpper()} found", Data = displayHubsInfo });
		}

        [HttpPost]
        [Route("/api/addhub")]
        public async Task<ActionResult> AddHub([FromBody] CreateHubDTO model)
        {

            await AddNewHub(model);

			return Ok(new ApiResponseDTO<string> { Code = 201, Status = "success", Message = "Hub successfully added"});

		}

        private async Task<Hubb> AddNewHub(CreateHubDTO model)
        {

            var hub = new Hubb
            {
                Name = model.Name,
                Address = model.Address,
                State = model.State,
                Image = model.Image,
                Tags = model.Tags,
                Website = model.Website
            };

             _repositoryManager.Hubb.CreateHubb(hub);


            await _repositoryManager.SaveAsync();

            return hub;

        }

        private List<CreateHubDTO> GetHubDetails(IEnumerable<Hubb> getHubsInfo)
        {
            List<CreateHubDTO> getHubs = new List<CreateHubDTO>();

            foreach (var item in getHubsInfo)
            {
                var hubs = new CreateHubDTO
                {
                    Address = item.Address,
                    Image = item.Image,
                    Name = item.Name,
                    State = item.State,
                    Tags = item.Tags,
                    Website = item.Website
                };

                getHubs.Add(hubs);
            }

            return getHubs;
        }

    }
}
