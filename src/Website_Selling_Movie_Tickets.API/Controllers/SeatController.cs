using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Seats.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Seats.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Seats.Queries.GetById;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class SeatController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methor = "SeatController";
        #endregion

        #region Ctor
        public SeatController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> CreateSeat([FromBody] CreateSeatRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methor} CreateSeat");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methor} CreateSeat response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create Seat Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message,
                });
            }
        }
        #endregion

        #region Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteSeat(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methor} DeleteSeat");
                var response = new DeleteSeatRequest
                {
                    SeatId = Id
                };
                var result = await _mediator.Send(response);
                _logger.Information($"End {Methor} DeleteSeat response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delete Seat Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message,
                });
            }
        }
        #endregion

        #region Update
        [HttpPut]
        public async Task<IActionResult> UpdateSeat([FromBody] UpdateSeatRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methor} UpdateSeat");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methor} UpdateSeat response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Update Seat Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message,
                });
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSeat()
        {
            try
            {
                _logger.Information($"Begin {Methor} GetAllSeat");
                var response = new GetAllSeatQuery();
                var result = await _mediator.Send(response);
                _logger.Information($"End {Methor} GetAllSeat response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetAll Seat Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message,
                });
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdSeat(int id)
        {
            try
            {
                _logger.Information($"Begin {Methor} GetByIdSeat");
                var request = new GetByIdSeatQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methor} GetByIdSeat reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetById Seat Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message,
                });
            }
        }
        #endregion
    }
}
