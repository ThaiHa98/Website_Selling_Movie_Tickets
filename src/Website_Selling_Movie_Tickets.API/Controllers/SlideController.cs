using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetImage;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetImage;
using Website_Selling_Movie_Tickets.Application.Features.Slides.Queries.GetPagination;
using Website_Selling_Movie_Tickets.Domain.Entities;
using ILogger = Serilog.ILogger;


namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class SlideController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methord = "SlideController";
        #endregion

        #region Ctor
        public SlideController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSlide([FromForm] CreateSlidesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} CreateSlide");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} CreateSlide reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Create Slide Successfully"
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
        public async Task<IActionResult> UpdateSlide([FromBody] UpdateSlidesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methord} UpdateSlide");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methord} UpdateSlide reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Update Slide Successfully"
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
        public async Task<IActionResult> DeleteSlide(int id)
        {
            try
            {
                _logger.Information($"Begin {Methord} DeleteSlide");
                var resquest = new DeleteSlideRequest
                {
                    Id = id
                };
                var result = await _mediator.Send(resquest);
                return Ok(new ApiResultBase
                {
                    data = resquest,
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
        public async Task<IActionResult> GetAllSlide()
        {
            try
            {
                _logger.Information($"Begin {Methord} GetAllSlide");
                var resquest = new GetAllSlidesQuery();
                var result = await _mediator.Send(resquest);
                _logger.Information($"End {Methord} GetAllSlide reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> GetByIdSlide(int id)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetByIdSlide");
                var resquest = new GetByIdSlidesQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(resquest);
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
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

        #region GetPagination
        [HttpGet("GetPagination")]
        public async Task<IActionResult> GetPaginationSlide(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methord} GetPaginationSlide");
                var resquest = new GetPaginationSlideQuery
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize
                };
                var result = await _mediator.Send(resquest);
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

        #region Image
        [HttpGet("Image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                _logger.Information($"Begin GetImage for ID: {id}");
                var query = new GetImageSlideQuery
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
