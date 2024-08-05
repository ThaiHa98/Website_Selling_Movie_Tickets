using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website_Selling_Movie_Tickets.Domain.Entities;

namespace Website_Selling_Movie_Tickets.Application.Features.ChairTypes.Common.Delete
{
    public class DeleteChairTypeRequest : IRequest<ChairType>
    {
        public int Id { get; set; }
    }
}
