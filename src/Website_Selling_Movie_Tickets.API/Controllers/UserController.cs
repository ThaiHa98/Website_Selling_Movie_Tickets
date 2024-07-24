using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Diagnostics.Metrics;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Users.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Users.Common.Login;
using Website_Selling_Movie_Tickets.Application.Features.Users.Queries.GetAll;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class UserController : AdminApiV2Controller
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private static string Methods = "UserController";
        #endregion

        #region Create
        public UserController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} CreateUser");
                var query = new CreateUserRequest(request.UserModel);
                var result = await _mediator.Send(query);
                _logger.Information($"End {Methods} CreateUser repose : {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create user successfully"
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
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetAll");
                var query = new GetAllUserQuery
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
                var result = await _mediator.Send(query);
                _logger.Information($"End GetAll response: {JsonConvert.SerializeObject(result)}");

                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.TotalRecords,
                    message = "GetAll"
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in GetAll: {ex.Message}");
                return BadRequest(new ApiResultBase
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} LoginUser");
                var result = await _mediator.Send(request);
                _logger.Information($"End LoginUser response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Login Successfully"
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
