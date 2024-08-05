using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Queries.GetPagination;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class ChairTypeController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methord = "ChairTypeController";
        #endregion

        #region Ctor
        public ChairTypeController(IMediator mediator, ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateChairType([FromBody] CreateChairTypeRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} CreateChairType");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} CreateChairType reponse: {JsonConvert.SerializeObject(result)}");
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
                    message = "An error occurred while creating the genre."
                });
            }
        }
        #endregion

        #region Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateChairType([FromBody] UpdateChairTypeRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} UpdateChairType");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} UpdateChairType reponse: {JsonConvert.SerializeObject(result)}");
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
                    message = "An error occurred while creating the genre."
                });
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteChairType(int id)
        {
            try
            {
                _logger.Information($"Begin {Methord} DeleteChairType");
                var request = new DeleteChairTypeRequest
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} DeleteChairType reponse: {JsonConvert.SerializeObject(request)}");
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
                    message = "An error occurred while creating the genre."
                });
            }
        }
        #endregion

        #region GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllChairType()
        {
            try
            {
                _logger.Information($"Begin {Methord} GetAllChairType");
                var request = new GetAllChairTypeQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetAllChairType reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetAll Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = "An error occurred while creating the genre."
                });
            }

        }
        #endregion

        #region GetPagination
        [HttpGet("GetPagination")]
        public async Task<IActionResult> GetPaginationChairtype(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetPaginationChairtype");
                var request = new GetPaginationchairTypeQuery
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize
                };
                var result = await _mediator.Send(request);
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetPagination Successfully"
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
        public async Task<IActionResult> GetByIdChairType (int Id)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetByIdChairType");
                var request = new GetByIdChairTypeQuery
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                return Ok(new ApiResultBase
                {
                    data = request,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = request.Id,
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
