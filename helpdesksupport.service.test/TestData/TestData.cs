using HelpDeskSupportService;
using System;
using System.Collections.Generic;
using System.Text;

namespace helpdesksupport.service.test
{
    class TestData
    {
        internal static IEnumerable<Ticket> GetTestTickets
        {
            get
            {
                return new List<Ticket> { new Ticket { ID = 1, Name = "test ticket", Active = true } };
            }
        }
    }
}
