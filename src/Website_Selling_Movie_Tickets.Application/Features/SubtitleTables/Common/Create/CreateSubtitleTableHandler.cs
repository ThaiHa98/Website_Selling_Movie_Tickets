using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.SubtitleTables.Common.Create
{
    public class CreateSubtitleTableHandler : IRequestHandler<CreateSubtitleTableRequest, Response<List<SubtitleTable>>>
    {
        private readonly DBContext _dbContext;
        private readonly IValidator<CreateSubtitleTableRequest> _validator;
        private readonly ISubtitleTableRepository _subtitleTableRepository;

        public CreateSubtitleTableHandler(DBContext dbContext, IValidator<CreateSubtitleTableRequest> validator, ISubtitleTableRepository subtitleTableRepository)
        {
            _subtitleTableRepository = subtitleTableRepository;
            _validator = validator;
            _dbContext = dbContext;
        }

        public async Task<Response<List<SubtitleTable>>> Handle(CreateSubtitleTableRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                throw new ValidationException(errors);
            }

            var subtitleTables = new List<SubtitleTable>();

            try
            {
                var timeSlotIds = new List<int>();

                // Kiểm tra các TimeSlot_Id
                foreach (var timeSlotId in request.TimeSlot_Id)
                {
                    var timeSlot = await _dbContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == timeSlotId);
                    if (timeSlot == null)
                    {
                        throw new Exception($"Id {timeSlotId} không tồn tại");
                    }
                    timeSlotIds.Add(timeSlot.Id);
                }

                // Tạo một SubtitleTable với tất cả TimeSlot_Id
                var subtitleTable = new SubtitleTable
                {
                    Name = request.Name,
                    TimeSlot_Id = string.Join(", ", timeSlotIds)
                };

                var response = await _subtitleTableRepository.Create(subtitleTable);
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                subtitleTables.Add(subtitleTable);

                return new Response<List<SubtitleTable>>
                {
                    Success = true,
                    Data = subtitleTables,
                    Message = "Tạo SubtitleTables thành công"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<SubtitleTable>>
                {
                    Success = false,
                    Message = $"Đã xảy ra lỗi trong quá trình tạo SubtitleTables: {ex.Message}"
                };
            }
        }
    }
}
