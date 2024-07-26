using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Claims;
using Website_Selling_Movie_Tickets.Application.Features.Users.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Users.Common.Login;
using Website_Selling_Movie_Tickets.Application.Features.Users.Common.Logout;
using Website_Selling_Movie_Tickets.Application.Features.Users.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Users.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Users.Queries.GetById;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class UserController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private static string Methods = "UserController";
        #endregion

        #region Ctor
        public UserController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
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

        #region GetById
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetById");
                var query = new GetByIdUserQuery
                {
                    Id = Id
                };
                var result = await _mediator.Send(query);
                _logger.Information($"End GetById response:{JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
                    message = "Successfully"
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

        #region Logout
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutUser()
        {
            try
            {
                _logger.Information($"Begin {Methods} LogoutUser");
                //Lấy thông tin người dùng từ HttpContext
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);    
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    _logger.Error("User not found or invalid user ID");
                    return BadRequest(new ApiResultBase
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.BadRequest,
                        message = "User not found or invalid user ID"
                    });
                }
                // Gửi yêu cầu LogoutRequest tới MediatR
                var response = await _mediator.Send(new LogoutRequest { Id = userId });

                _logger.Information($"End LogoutUser response:{JsonConvert.SerializeObject(response)}");
                return Ok(new ApiResultBase
                {
                    data = response,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Logout Successfully"
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
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} UpdateUser");
                var result = await _mediator.Send(request);
                _logger.Information($"End UpdateUser response : {JsonConvert.SerializeObject(result)}");
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
    }
}
