using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDeskSupportService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelpDeskSupportService.Controllers
{
    /// <summary>
    /// ticket controller
    /// </summary>
    [Authorize("permission")]
    [Route("helpdesk-microservice/[controller]")]
    [ApiController]
    public class HelpDeskController : ControllerBase
    {
        IHelpdeskService _helpdekService;
        ILogger<HelpDeskController> _logger;
        private IMapper _mapper;
        public HelpDeskController(IHelpdeskService helpdekService, ILogger<HelpDeskController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _helpdekService = helpdekService;
        }
        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <returns>returns all tickets</returns>
        [HttpGet(Name="get-tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TicketModel>> Get()
        {
            _logger.LogInformation($"getting tickets ...");
            var tickets= _helpdekService.GetTickets();
            if (tickets == null ) NotFound();
            var ticketsModel = _mapper.Map<IEnumerable<TicketModel>>(tickets);
            return Ok(new{Tickets= ticketsModel });
        }

        /// <summary>
        /// Get a specific ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns the specified ticket</returns>
        [HttpGet("{id}",Name="get-ticket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            // No model state validation code here in dotnet ore 2.1, hooray!

            _logger.LogInformation($"getting ticket by id= {id} ...");
            var ticket= _helpdekService.GetTicketById(id);
            if(ticket==null) return NotFound();
            var ticketModel = _mapper.Map<TicketModel>(ticket);
            return Ok(new { Ticket = ticketModel });
        }

        /// <summary>
        /// Post a new ticket
        /// </summary>
        /// <param name="ticketModel"></param>
        [HttpPost(Name="add-ticket")]
        [ProducesResponseType( StatusCodes.Status201Created)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] TicketModel ticketModel)
        {
            var ticket = _mapper.Map<Ticket>(ticketModel);
            ticket = _helpdekService.AddTicket(ticket);
            if (ticket == null) return BadRequest();
            return CreatedAtAction("Get", new { id = ticket.ID }, ticket);
        }

        /// <summary>
        /// Put to modify a ticket
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticketModel"></param>
        [HttpPut("{id}",Name="update-ticket")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(int id, [FromBody] TicketModel ticketModel)
        {
            if (id < 0) return BadRequest();

            var ticket = _mapper.Map<Ticket>(ticketModel);

            var updatedTicket = _helpdekService.UpdateTicket(id,ticket);

            if (updatedTicket==null) return NotFound();

            ticketModel = _mapper.Map<TicketModel>(updatedTicket);

            return Ok(new {Ticket= ticketModel });    
        }
        /// <summary>
        /// Delete a ticket
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType( StatusCodes.Status204NoContent)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}",Name="delete-ticket")]
        public ActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var ticket = _helpdekService.DeleteTicket(id);  
            if (ticket == null) return NotFound();  
            return new NoContentResult();  
        }
    }
}
