using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Queries.GetById;
using Website_Selling_Movie_Tickets.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class GenreController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private static string Methods = "GenreController";
        #endregion

        #region Ctor
        public GenreController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} CreateGenre");

                var result = await _mediator.Send(request);

                _logger.Information($"End {Methods} CreateGenre reponse: {JsonConvert.SerializeObject(result)}");

                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create Successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in CreateGenre: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} UpdateGenre");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} UpdateGenre reponse {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Update Successfully"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteGenre(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} DeleteGenre");
                var request = new DeleteGenreRequest { Id = Id };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} DeleteGenre reponse :{JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delete Successfully"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.Information($"Begin {Methods} GetAll");
                var query = new GetAllGenreQuery();
                var result = await _mediator.Send(query);
                _logger.Information($"End {Methods} GetAll reponse : {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count(),
                    message = "GetAll"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetByIdGenre");
                var query = new GetByIdGenreQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(query);
                _logger.Information($"End {Methods} GetByIdGenre reponse : {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
                    message = "GetById Seuccessfully"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion
    }
}
