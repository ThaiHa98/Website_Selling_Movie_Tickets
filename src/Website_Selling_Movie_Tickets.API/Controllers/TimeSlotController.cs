using Elasticsearch.Net.Specification.SecurityApi;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Diagnostics.Metrics;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Theaters.Queries.GetPagination;
using Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.TimeSlots.Queries.GetPagination;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class TimeSlotController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methods = "TimeSlotController";
        #endregion

        #region Ctor
        public TimeSlotController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTimeSlot([FromBody] CreateTimeSlotRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} CreateTimeSlot");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} CreateTimeSlot reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
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

        #region Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTimeSlot([FromBody] UpdateTimeSlotRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} UpdateTimeSlot");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} UpdateTimeSlot reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
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

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} DeleteTimeSlot");
                var request = new DeleteTimeSlotRequest
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} DeleteTimeSlot reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
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

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTimeSlot()
        {
            try
            {
                _logger.Information($"Begin {Methods} GetAllTimeSlot");
                var request = new GetAllTimeSlotQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetAllTimeSlot reponse: {JsonConvert.SerializeObject(result)}");
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

        #region GetPagination
        [HttpGet("GetPagination")]
        public async Task<IActionResult> GetPagination(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetPagination");
                var query = new GetPaginationTimeSlotQuery
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

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdTimeSlot(int Id)
        {
            try
            {
                _logger.Information($"Begin {nameof(GetByIdTimeSlot)}");
                var request = new GetByIdTimeSlotQuery
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {nameof(GetByIdTimeSlot)} response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result != null ? 1 : 0,
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
    }
}
