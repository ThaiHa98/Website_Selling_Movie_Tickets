using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Diagnostics.Metrics;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Genres.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetPagination;
using Website_Selling_Movie_Tickets.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class TheaterController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methods = "TheaterController";
        #endregion

        #region Ctor
        public TheaterController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTheater([FromBody] CreateTheatersRequest request)
        {
            try
            {
                _logger.Information("Begin CreateTheater");
                var result = await _mediator.Send(request);
                _logger.Information($"End CreateTheater reponse: {JsonConvert.SerializeObject(result)}");

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
        public async Task<IActionResult> UpdateTheater([FromBody] UpdateTheaterRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} UpdateTheater");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} UpdateTheater reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> DeleteTheater(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} DeleteTheater");
                var request = new DeleteTheatersRequest
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} DeleteTheater reponse: {JsonConvert.SerializeObject(result)}");
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

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetById");
                var request = new GetByIdTheatersQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetById reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> GetPagination(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetPagination");
                var query = new GetPaginationTheaterQuery
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                var result = await _mediator.Send(query);
                _logger.Information($"End {Methods} GetPagination reponse : {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.PageCount,
                    message = "GetPagination"
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
                _logger.Information($"Beging {nameof(GetAll)}");

                var query = new GetAllTheaterQuery();
                var result = await _mediator.Send(query);

                _logger.Information($"End {nameof(GetAll)} reponse: {JsonConvert.SerializeObject(result)}");

                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "Result"
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
