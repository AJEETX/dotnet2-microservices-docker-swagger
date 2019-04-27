using AutoMapper;
using HelpDeskSupportService;
using HelpDeskSupportService.Persistence;
using HelpDeskSupportService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace helpdesksupport.service.test
{
    public class HelpdeskServiceTest
    {
        [Fact(DisplayName = "AddTicket adds ticket")]
        public void AddTicket_adds_ticket()
        {
            //given
            var ticket = new Ticket { Name = "test", Active = true };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Add_to_database").Options;

            //when
            using (var context = new ApplicationDbContext(options))
            {
                var sut = new HelpdeskService(context);
                sut.AddTicket(ticket);
            }

            //then
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(1, context.Tickets.Count());
                Assert.Equal(ticket.Name, context.Tickets.Single().Name);
                Assert.Equal(ticket.Active, context.Tickets.Single().Active);
            }
        }
    }
}
