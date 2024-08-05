using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetPagination;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchByKey;
using Website_Selling_Movie_Tickets.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class MoviesController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methods = "MoviesController";
        #endregion

        #region Ctor
        public MoviesController(IMediator mediator, ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMovies([FromForm] CreateMoviesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} CreateMovies");
                if (request.Image == null || request.Image.Length == 0)
                {
                    return BadRequest("Image is required.");
                }
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} CreateMovies reponse: {JsonConvert.SerializeObject(result)}");
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
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMoviesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} UpdateMovie");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} UpdateMovie reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> DeleteMovie(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} DeleteMovie");
                var request = new DeleteMoviesRequest
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} DeleteMovie reponse: {JsonConvert.SerializeObject(result)}");
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

        #region GetPagination
        [HttpGet("GetPagination")]
        public async Task<IActionResult> GetPaginationMovies(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methods} PaginationMovies");
                var request = new GetPaginationMoviesQuery
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} PaginationMovies reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.PageCount,
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
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                _logger.Information($"Begin {Methods} GetAllMovies");
                var request = new GetAllMoviesQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetAllMovies reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
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

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdMovie(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetByIdMovie");
                var request = new GetByIdMoviesQuery
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetByIdMovie reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
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

        #region SearchByKey
        [HttpPatch("SearchByKey")]
        public async Task<IActionResult> SearchByKeyMovieQuery(string key)
        {
            try
            {
                _logger.Information($"Begin {Methods} SearchByKeyMovieQuery");
                var request = new SearchByKeyMovieQuery
                {
                    key = key
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} SearchByKeyMovieQuery reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
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

    }
}
