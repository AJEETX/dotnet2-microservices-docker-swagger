using AutoMapper;
using helpdesksupport.service.test;
using HelpDeskSupportService;
using HelpDeskSupportService.Controllers;
using HelpDeskSupportService.Persistence;
using HelpDeskSupportService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace helpdesksupport.controller.test
{
    public class HelpdeskControllerTest
    {
        [Fact(DisplayName ="get all tickets")]
        public void GetTickets_returns_all_tickets()
        {
            //given
            var moqMapper = new Mock<IMapper>();
            var moqHelpdeskService = new Mock<IHelpdeskService>();
            moqHelpdeskService.Setup(m => m.GetTickets()).Returns(TestData.GetTestTickets);
            var logger = Mock.Of<ILogger<HelpDeskController>>();
            var controller = new HelpDeskController(moqHelpdeskService.Object, logger, moqMapper.Object);

            //when
            var actual = controller.Get();

            //then
            Assert.IsAssignableFrom<ActionResult<IEnumerable<TicketModel>>>(actual);
        }
    }
}
