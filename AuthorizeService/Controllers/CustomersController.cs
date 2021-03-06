﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ocelot.JwtAuthorize;
using System.Security.Claims;
using Authorize.Model;
using Microsoft.AspNetCore.Http;
using AuthorizeService.Service;
using AutoMapper;

namespace Authorize.Controllers
{
    /// <summary>
    /// Authorize
    /// </summary>
    [Route("authorize-microservice/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        readonly ILogger<CustomersController> _logger;
        readonly ITokenBuilder _tokenBuilder;
        ICustomerService _customerService;
        IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenBuilder"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="customerService"></param>
        public CustomersController(ITokenBuilder tokenBuilder, ILogger<CustomersController> logger,ICustomerService customerService,IMapper mapper)
        {
            _logger = logger;
            _tokenBuilder = tokenBuilder;
            _customerService=customerService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>returns all customers</returns>
        [HttpGet(Name = "get-customers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomerModel>> Get()
        {
            _logger.LogInformation($"getting customers ...");
            var customers = _customerService.GetCustomers();
            if (customers == null) NotFound();
            var customersModel = _mapper.Map<IEnumerable<CustomerModel>>(customers);
            return Ok(new { Customers = customersModel });
        }
        /// <summary>
        /// Customer login
        /// </summary>
        /// <param name="loginModel">Model</param>
        /// <returns>login customer</returns>
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginModel loginModel)
        {
            // No model state validation code here in dotnet ore 2.1, hooray!
            string message = string.Empty;
            var login = _mapper.Map<Login>(loginModel);
            var userIsValid=_customerService.Login(login);
            message = $"{loginModel.UserName} login！";
            _logger.LogInformation(message);
            if (userIsValid)
            {
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Role, "admin"),

                };
                var token = _tokenBuilder.BuildJwtToken(claims);
                message = $"{loginModel.UserName} login success，and generate token return";
                _logger.LogInformation(message);
                return Ok(token);
            }
            else
            {
                message = $"{loginModel.UserName} login fails";
                _logger.LogInformation(message);
                return BadRequest(message);
            }
        }
        /// <summary>
        /// Customer register 
        /// </summary>
        /// <param name="customerModel">Model</param>
        /// <returns>return regiter message</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create")]
        public ActionResult Register([FromBody]CustomerModel customerModel)
        {
            // No model state validation code here in dotnet core 2.1, hooray!

            _logger.LogInformation($"{customerModel.UserName} register！");
            var customer = _mapper.Map<Customer>(customerModel);

            var createdCustomer=_customerService.AddCustomer(customer);
            if (createdCustomer != null)
            {
                _logger.LogInformation($"{customer.UserName} register success");
                return CreatedAtAction("login", new { id = createdCustomer.ID}, createdCustomer);
            }
            else
            {
                string message = $"{customer.UserName} register failes";
                _logger.LogInformation(message);
                return BadRequest(message);
            }
        }
        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="userName"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{userName}")]
        public ActionResult Delete(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest();


            var customer = _customerService.DeleteCustomer(userName);
            if (customer == null) return NotFound();

            _logger.LogInformation($"deleted customer with userName : {userName}！");

            return Ok($"{userName} deleted !!!" );
        }
    }
}
