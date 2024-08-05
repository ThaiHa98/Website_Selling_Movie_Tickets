using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.GetPagination;
using Website_Selling_Movie_Tickets.Application.Features.ScreeningRooms.Queries.SearchByKey;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class ScreeningRoomController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methord = "ScreeningRoomController";
        #endregion

        #region Ctor
        public ScreeningRoomController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateScreeningRoom([FromBody] CreateScreeningRoomRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} CreateScreeningRoom");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} CreateScreeningRoom reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> UpdateScreeningRoom([FromBody] UpdateScreeningRoosRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} UpdateScreeningRoom");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} UpdateScreeningRoom reponse: {JsonConvert.SerializeObject(result)}");
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

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteScreeningRoom(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methord} DeleteScreeningRoom");
                var request = new DeleteScreeningRoomRequest
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} DeleteScreeningRoom reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> GetAllScreeningRoom()
        {
            try
            {
                _logger.Information($"Begin {Methord} GetAllScreeningRoom");
                var request = new GetAllScreeningRoomQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetAllScreeningRoom reponse: {JsonConvert.SerializeObject(result)}");
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

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdScreeningRoom(int id)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetByIdScreeningRoom");
                var request = new GetByIdScreeningRoomQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetByIdScreeningRoom reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
                    message = "Successfully retrieved the screening room."
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
                _logger.Information($"Begin {Methord} GetPagination");
                var request = new GetPaginationScreeningRoomQuery
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetPagination reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.PageCount,
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

        #region SearchByKey
        [HttpGet("SearchByKey")]
        public async Task<IActionResult> SearchByKeyScreeningRoom([FromQuery] SearchByKeyScreeningRoomQuery request)
        {
            try
            {
                _logger.Information($"Begin {Methord} SearchByKeyScreeningRoom");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} SearchByKeyScreeningRoom reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.PageCount,
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
