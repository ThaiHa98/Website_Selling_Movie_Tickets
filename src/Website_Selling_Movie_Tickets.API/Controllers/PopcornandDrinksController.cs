using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetImage;
using Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.PopcornandDrinks.Queries.GetImage;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class PopcornandDrinksController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private static string Methord = "PopcornandDrinksController";
        #endregion

        #region Ctor
        public PopcornandDrinksController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<IActionResult> CreatePopcornandDrinks([FromForm] CreatePopcornandDrinksRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} CreatePopcornandDrinks");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} CreatePopcornandDrinks reponse: {JsonConvert.SerializeObject(result)}");
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
        [HttpPut]
        public async Task<IActionResult> UpdatepopcornandDrinks(UpdatePopcornandDrinksRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} UpdatepopcornandDrinks");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} UpdatepopcornandDrinks reponse: {JsonConvert.SerializeObject(result)}");
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
        [HttpDelete]
        public async Task<IActionResult> DeletePopcornandDrinks(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methord} DeletePopcornandDrinks");
                var request = new DeletePopcornandDrinksRequest
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} DeletePopcornandDrinks reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> GetAllPopcornandDrinks()
        {
            try
            {
                _logger.Information($"Begin {Methord} GetAllPopcornandDrinks");
                var request = new GetAllPopcornandDrinksQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} GetAllPopcornandDrinks reponse: {JsonConvert.SerializeObject(result)}");
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
                    message = ex.Message
                });
            }
        }
        #endregion

        #region Image
        [HttpGet("Image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                _logger.Information($"Begin GetImage for ID: {id}");
                var query = new GetImagePopcornandDrinksQuery
                {
                    Id = id
                };
                var imageBytes = await _mediator.Send(query);

                if (imageBytes == null || imageBytes.Length == 0)
                {
                    return NotFound(new ApiResultBase
                    {
                        success = false,
                        httpStatusCode = (int)HttpStatusCode.NotFound,
                        message = "Image not found"
                    });
                }
                var contentType = "image/jpeg";
                return File(imageBytes, contentType);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while retrieving the image");
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
