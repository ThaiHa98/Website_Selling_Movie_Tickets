using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Queries.GetById;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class SubtitleTableController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methord = "SubtitleTableController";
        #endregion

        #region Ctor
        public SubtitleTableController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> CreareSubtitleTable([FromBody] CreateSubtitleTableRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} CreareSubtitleTable");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} CreareSubtitleTable reponse: {JsonConvert.SerializeObject(request)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create SubtitleTable Successfully"
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
        [HttpGet]
        public async Task<IActionResult> GetByIdSubtitleTable(int id)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetByIdSubtitleTable");
                var request = new GetByIdSubtitleTablesQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetByIdSubtitleTable response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create SubtitleTable Successfully"
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
        [HttpPut]
        public async Task<IActionResult> UpdateSubtitleTables([FromBody] UpdateSubtitleTablesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} UpdateSubtitleTables");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} UpdateSubtitleTables response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Update SubtitleTable Successfully"
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
        [HttpDelete]
        public async Task<IActionResult> DeleteSubtitleTables(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methord} DeleteSubtitleTables");
                var request = new DeleteSubtitleTableRequest
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} DeleteSubtitleTables response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delete SubtitleTable Successfully"
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
        public async Task<IActionResult> GetAllSubtitleTable()
        {
            try
            {
                _logger.Information($"Begin {Methord} GetAllSubtitleTable");
                var request = new GetAllSubtitleTablesQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetAllSubtitleTable response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetAll SubtitleTable Successfully"
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
