using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using System.Net.NetworkInformation;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Tickets.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.Tickets.Queries.GetPagination;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class TicketController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methord = "TicketController";
        #endregion

        #region Ctor
        public TicketController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} CreateTicket");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} CreateTicket reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create Ticket Successfully"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} UpdateTicket");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} UpdateTicket response: {JsonConvert.SerializeObject(result)}");

                if (result.Success)
                {
                    return Ok(new ApiResultBase
                    {
                        data = result.Data,
                        success = true,
                        httpStatusCode = (int)HttpStatusCode.OK,
                        message = "Update Ticket Successfully"
                    });
                }
                else
                {
                    return BadRequest(new ApiResultBase
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.BadRequest,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    message = "An unexpected error occurred."
                });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            try
            {
                _logger.Information($"Begin {Methord} DeleteTicket");
                var request = new DeleteTicketRequest
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} DeleteTicket response: {JsonConvert.SerializeObject(result)}");

                if (result.Success)
                {
                    return Ok(new ApiResultBase
                    {
                        data = result.Data,
                        success = true,
                        httpStatusCode = (int)HttpStatusCode.OK,
                        message = "Delete Ticket Successfully"
                    });
                }
                else
                {
                    return BadRequest(new ApiResultBase
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.BadRequest,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    message = "An unexpected error occurred."
                });
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTicket()
        {
            try
            {
                _logger.Information($"Begin {Methord} GetAllTicket");
                var request = new GetAllTicketQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetAllTicket response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "GetAll Successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    message = "An unexpected error occurred."
                });
            }
        }
        #endregion

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdTicket(string id)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetByIdTicket");
                var request = new GetByIdTicketQuery
                {
                    Id = id
                };
                var response = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetByIdTicket reponse: {JsonConvert.SerializeObject(request)}");
                return Ok(new ApiResultBase
                {
                    data = response,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetById Successfully"
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(new ApiResultBase
                {
                    success= false,
                    httpStatusCode= (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region GetPagination
        [HttpGet("GetPagination")]
        public async Task<IActionResult> GetPaginationTicket(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetPaginationTicket");
                var request = new GetPaginationTicketQuery
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize
                };
                var response = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetPaginationTicket reponse: {JsonConvert.SerializeObject(request)}");
                return Ok(new ApiResultBase
                {
                    data = response,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetById Successfully"
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
