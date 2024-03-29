﻿using AutoMapper;
using hvz_backend.Exceptions;
using hvz_backend.Models;
using hvz_backend.Models.DTOs.Game;
using hvz_backend.Models.DTOs.Map;
using hvz_backend.Models.DTOs.Safezone;
using hvz_backend.Models.DTOs.Supply;
using hvz_backend.Services.MapServices;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace hvz_backend.Controllers
{
    [Route("api/v1/map")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MapsController : ControllerBase
    {
        #region Fields & Constructor
        private readonly IMapService _service;
        private readonly IMapper _mapper;

        // Sets the service and mapper for this controller via constructor.
        public MapsController(IMapService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #endregion

        #region HTTP POSTs
        /// <summary>
        /// Create a new map.
        /// </summary>
        /// <param name="createMapDto"></param>
        /// <returns>The newly created map</returns>
        [HttpPost]
        public async Task<ActionResult<Map>> CreateMap(MapCreateDTO createMapDto)
        {
            try
            {
                var map = _mapper.Map<Map>(createMapDto);
                await _service.CreateMap(map);
                return CreatedAtAction(nameof(GetMapById), new { id = map.Id }, map);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HTTP GETs
        /// <summary>
        /// Get all the maps of the database.
        /// </summary>
        /// <returns>List of maps.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapReadDTO>>> GetAllMaps()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<MapReadDTO>>(await _service.GetAllMaps()));
            }
            catch (MapNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Get one specific map.
        /// </summary>
        /// <param name="id">Identifier of a map.</param>
        /// <returns>Map object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MapReadDTO>> GetMapById(int id)
        {
            try
            {
                return Ok(_mapper.Map<MapReadDTO>(await _service.GetMapById(id)));
            }
            catch (MapNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Get the name of map
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <returns>String</returns>
        [HttpGet("{id}/name")]
        public async Task<ActionResult<MapNameDTO>> GetNameMap(int id)
        {
            try
            {
                return Ok(_mapper.Map<MapNameDTO>(await _service.GetMapById(id)));
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }
        /// <summary>
        /// Get the description of map
        /// </summary>
        /// <param name="id">Identifer of map</param>
        /// <returns>String</returns>
        [HttpGet("{id}/description")]
        public async Task<ActionResult<MapDescriptionDTO>> GetDescriptionMap(int id)
        {
            try
            {
                return Ok(_mapper.Map<MapDescriptionDTO>(await _service.GetMapById(id)));
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }
        /// <summary>
        /// Get the latitude of map center
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <returns>Double</returns>
        [HttpGet("{id}/latitude")]
        public async Task<ActionResult<MapLatDTO>> GetLatitudeMap(int id)
        {
            try
            {
                return Ok(_mapper.Map<MapLatDTO>(await _service.GetMapById(id)));
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }
        /// <summary>
        /// Get the longitude of map center
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <returns>Double</returns>
        [HttpGet("{id}/longitude")]
        public async Task<ActionResult<MapLongDTO>> GetLongitudeMap(int id)
        {
            try
            {
                return Ok(_mapper.Map<MapLongDTO>(await _service.GetMapById(id)));
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }

        /// <summary>
        /// Get the radius of map
        /// </summary>
        /// <param name="id">Identifer of map</param>
        /// <returns>Int</returns>
        [HttpGet("/{id}/radius")]
        public async Task<ActionResult<MapRadiusDTO>> GetRadiusMap(int id)
        {
            try
            {
                return Ok(_mapper.Map<MapRadiusDTO>(await _service.GetMapById(id)));
            }
            catch (KillNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }

        #endregion

        #region HTTP PUT
        /// <summary>
        /// Update the whole map object
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <param name="mapDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMap(int id, MapUpdateDTO mapDTO)
        {
            try
            {
                var map = _mapper.Map<Map>(mapDTO);
                map.Id = id;
                await _service.UpdateMap(map);
            }
            catch (MapNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
            return NoContent();
        }
        #endregion

        #region PATCH 
        /// <summary>
        /// Update the name of map
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <param name="mapNameDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/name")]
        public async Task<ActionResult> PatchNameMap(int id, [FromBody] MapNameDTO mapNameDTO)
        {
            try
            {
                await _service.PatchNameMap(id, mapNameDTO.MapName);
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
            return NoContent();
        }
        /// <summary>
        /// Update the description of map
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <param name="mapDescriptionDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/description")]
        public async Task<ActionResult> PatchDescriptionMap(int id, [FromBody] MapDescriptionDTO mapDescriptionDTO)
        {
            try
            {
                await _service.PatchDescriptionMap(id, mapDescriptionDTO.MapDescription);
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
            return NoContent();
        }
        /// <summary>
        /// Update latitude of map
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <param name="mapLatDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/latitude")]
        public async Task<ActionResult> PatchLatitudeMap(int id, [FromBody] MapLatDTO mapLatDTO)
        {
            try
            {
                await _service.PatchLatitudeMap(id, mapLatDTO.Latitude);
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
            return NoContent();
        }
        /// <summary>
        /// Update longitude of map
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <param name="mapLongDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/longitude")]
        public async Task<ActionResult> PatchLongitudeMap(int id, [FromBody] MapLongDTO mapLongDTO)
        {
            try
            {
                await _service.PatchLongitudeMap(id, mapLongDTO.Longitude);
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
            return NoContent();
        }
        /// <summary>
        /// Update radius of map
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <param name="mapRadiusDTO"></param>
        /// <returns></returns>
        [HttpPatch("{id}/radius")]
        public async Task<ActionResult> PatchRadiusMap(int id, [FromBody] MapRadiusDTO mapRadiusDTO)
        {
            try
            {
                await _service.PatchRadiusMap(id, mapRadiusDTO.Radius);
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
            return NoContent();
        }


        #endregion

        #region HTTP DELETE
        /// <summary>
        /// Delete map and corresponding supplies, missions and safezones
        /// </summary>
        /// <param name="id">Identifier of map</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMap(int id)
        {
            try
            {
                await _service.DeleteMap(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
        #endregion


    }
}
