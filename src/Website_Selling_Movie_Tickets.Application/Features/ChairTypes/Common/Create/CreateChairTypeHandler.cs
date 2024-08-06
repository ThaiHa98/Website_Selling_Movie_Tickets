using FluentValidation;
using MediatR;
using Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Application.Common.Interfaces;
using Website_Selling_Movie_Tickets.Domain.Entities;
using Website_Selling_Movie_Tickets.Infrastructure.Persistence;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Create
{
    public class CreateChairTypeHandler : IRequestHandler<CreateChairTypeRequest, ChairType>
    {
        private readonly IChairTypeRepository _chairTypeRepository;
        private readonly IValidator<CreateChairTypeRequest> _validator;

        public CreateChairTypeHandler(IChairTypeRepository chairTypeRepository, IValidator<CreateChairTypeRequest> validator)
        {
            _chairTypeRepository = chairTypeRepository;
            _validator = validator;
        }

        public async Task<ChairType> Handle(CreateChairTypeRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            try
            {
                var chairType = new ChairType
                {
                    Name = request.Name,
                    Price = request.Price
                };

                var response = await _chairTypeRepository.AddAsync(chairType);

                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }

                return response.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the chair type.", ex);
            }
        }
    }
}

