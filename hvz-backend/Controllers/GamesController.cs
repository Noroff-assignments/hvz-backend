﻿using AutoMapper;
using hvz_backend.Exceptions;
using hvz_backend.Models;
using hvz_backend.Models.DTOs.Game;
using hvz_backend.Models.DTOs.Game;
using hvz_backend.Services.GameServices;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace hvz_backend.Controllers
{
    [Route("api/v1/game")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GamesController : ControllerBase
    {
        #region Fields & Constructor
        private readonly IGameService _service;
        private readonly IMapper _mapper;

        // Sets the service and mapper for this controller via constructor.
        public GamesController(IGameService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #endregion


        #region HTTP POSTs

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame(GameCreateDTO createGameDto)
        {
            try
            {
                var game = _mapper.Map<Game>(createGameDto);
                await _service.CreateGame(game);
                return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HTTP GETs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameReadDTO>>> GetAllGames()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<GameReadDTO>>(await _service.GetAllGames()));
            }
            catch (GameNotFoundException e)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = e.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameReadDTO>> GetGameById(int id)
        {
            try
            {
                return Ok(_mapper.Map<GameReadDTO>(await _service.GetGameById(id)));
            }
            catch (GameNotFoundException e)
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
        /// Possible to update one game.
        /// </summary>
        /// <param name="id">Identifier of the game.</param>
        /// <param name="gameDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameUpdateDTO gameDTO)
        {
            try
            {
                var game = _mapper.Map<Game>(gameDTO);
                game.Id = id;
                await _service.UpdateGame(game);
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
        /// Delete one game.
        /// </summary>
        /// <param name="id">Identifier of game.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                await _service.DeleteGame(id);
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