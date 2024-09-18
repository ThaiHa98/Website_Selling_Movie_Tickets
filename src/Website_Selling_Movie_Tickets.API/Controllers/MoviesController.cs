using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Shared.SeedWork;
using System.Net;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Create;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Delete;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Common.Update;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetAll;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetBooking;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetById;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetImage;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetListTimeSlot;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetMoviesDetails;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetPagination;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetSubtitleTable;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetTheaterAddressesByMovieId;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.GetTheaterNames;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.LoadUserImage;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchByKey;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchIronfilmreleased;
using Website_Selling_Movie_Tickets.Application.Features.Movies.Queries.SearchStatus;
using Website_Selling_Movie_Tickets.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace Website_Selling_Movie_Tickets.API.Controllers
{
    public class MoviesController : ApiController
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public static string Methods = "MoviesController";
        #endregion

        #region Ctor
        public MoviesController(IMediator mediator, ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMovies([FromForm] CreateMoviesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} CreateMovies");
                if (request.Image == null || request.Image.Length == 0)
                {
                    return BadRequest("Image is required.");
                }
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} CreateMovies reponse: {JsonConvert.SerializeObject(result)}");
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
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMoviesRequest request)
        {
            try
            {
                _logger.Information($"Begin {Methods} UpdateMovie");
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} UpdateMovie reponse: {JsonConvert.SerializeObject(result)}");
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
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteMovie(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} DeleteMovie");
                var request = new DeleteMoviesRequest
                {
                    Id = Id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} DeleteMovie reponse: {JsonConvert.SerializeObject(result)}");
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

        #region GetPagination
        [HttpGet("GetPagination")]
        public async Task<IActionResult> GetPaginationMovies(int pageIndex, int pageSize)
        {
            try
            {
                _logger.Information($"Begin {Methods} PaginationMovies");
                var request = new GetPaginationMoviesQuery
                {
                    pageIndex = pageIndex,
                    pageSize = pageSize
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} PaginationMovies reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.PageCount,
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
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                _logger.Information($"Begin {Methods} GetAllMovies");
                var request = new GetAllMoviesQuery();
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetAllMovies reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
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

        #region GetPremiere
        [HttpGet("GetPremiere")]
        public async Task<IActionResult> GetPremiereMovie(int Id, DateTime Premiere)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetPremiereMovie");
                var request = new GetPremiereMoviesQuery
                {
                    Id = Id,
                    premiere = Premiere

                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetPremiereMovie reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "GetPremiereMovie Successfully"
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
        [HttpPatch("SearchByKey")]
        public async Task<IActionResult> SearchByKeyMovieQuery(string key)
        {
            try
            {
                _logger.Information($"Begin {Methods} SearchByKeyMovieQuery");
                var request = new SearchByKeyMovieQuery
                {
                    key = key
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} SearchByKeyMovieQuery reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
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

        #region Image
        [HttpGet("Image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                _logger.Information($"Begin GetImage for ID: {id}");
                var query = new GetImageMoviesQuery 
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

        #region SearchStatus
        [HttpGet("SearchStatus")]
        public async Task<IActionResult> SearchStatusMovies(string status)
        {
            try
            {
                _logger.Information($"Begin {Methods} SearchByKeyMovieQuery");
                var request = new SearchStatusMoviesQuery
                {
                    status = status
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} SearchByKeyMovieQuery reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
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

        #region SearchIronfilmreleased
        [HttpGet("SearchIronfilmreleased")]
        public async Task<IActionResult> SearchIronfilmreleasedMovie(string status)
        {
            try
            {
                _logger.Information($"Begin {Methods} SearchIronfilmreleasedMovie");
                var request = new SearchIronfilmreleasedMovieQuery
                {
                    key = status
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} SearchIronfilmreleasedMovie reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
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

        #region GetMoviesDetails
        [HttpGet("GetMoviesDetails")]
        public async Task<IActionResult> GetMoviesDetails(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetMoviesDetails");
                var request = new GetMoviesDetailsQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetMoviesDetails response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Id,
                    message = "GetMoviesDetails Successfully"
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

        #region GetTheaterAddressesByMovieId
        [HttpGet("GetTheaterAddressesByMovieId")]
        public async Task<IActionResult> GetTheaterAddressesByMovieId(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetTheaterAddressesByMovieId");
                var request = new GetTheaterAddressesByMovieIdQuery
                {
                    Id = id
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetTheaterAddressesByMovieId response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "GetTheaterAddressesByMovieId Successfully"
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

        #region GetTheaterNames
        [HttpGet("GetTheaterNames")]
        public async Task<IActionResult> GetTheaterNames(int id, string address)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetTheaterNames");
                var request = new GetTheaterNamesQuery
                {
                    Id = id,
                    address = address
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetTheaterNames response: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "GetTheaterNames Successfully"
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

        #region GetSubtitleTable
        [HttpGet("GetSubtitleTable")]
        public async Task<IActionResult> GetSubtitleTable(int id)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetSubtitleTable");
                var request = new GetSubtitleTableQuery
                {
                    Id = id,
                };
                var result = await _mediator.Send(request);
                _logger.Information($"End {Methods} GetSubtitleTable reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "GetSubtitleTable Successfully"
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

        #region getBooking
        [HttpGet("getBooking")]
        public async Task<IActionResult> getBooking(int movie_Id, string theater_Address, int subtitleTable_Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} getBooking");
                var response = new GetBookingQuery
                {
                    movie_Id = movie_Id,
                    theater_Address = theater_Address,
                    subtitleTable_Id = subtitleTable_Id
                };
                var result = await _mediator.Send(response);
                _logger.Information($"End {Methods} getBooking reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = result.Count,
                    message = "getBooking Successfully"
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

        #region LoadUserImage
        [HttpGet("LoadUserImage")]
        public async Task<IActionResult> LoadUserImage(int Id)
        {
            try
            {
                _logger.Information($"Begin {Methods} LoadUserImage");
                var response = new LoadUserImageMoviesQuery
                {
                    Id = Id,
                };
                var result = await _mediator.Send(response);
                _logger.Information($"End {Methods} LoadUserImage reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "LoadUserImage Successfully"
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

        #region GetTimeSlot
        [HttpGet("GetTimeSlot")]
        public async Task<IActionResult> GetTimeSlot(int movie_id, string nameSubtitleTable)
        {
            try
            {
                _logger.Information($"Begin {Methods} GetTimeSlot");
                var response = new GetListTimeSlotQuery
                {
                    movie_Id = movie_id,
                    nameSubtitleTable = nameSubtitleTable
                };
                var result = await _mediator.Send(response);
                _logger.Information($"End {Methods} GetTimeSlot reponse: {JsonConvert.SerializeObject(result)}");
                return Ok(new ApiResultBase
                {
                    data = result,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "GetTimeSlot Successfully"
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
