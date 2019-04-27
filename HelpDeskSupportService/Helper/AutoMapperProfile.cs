using AutoMapper;
using HelpDeskSupportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helpdesksupport.service.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ticket, TicketModel>();
            CreateMap<TicketModel, Ticket>();
        }
    }
}
