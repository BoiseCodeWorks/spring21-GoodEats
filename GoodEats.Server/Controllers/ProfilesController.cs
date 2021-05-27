using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodEats.Models;
using GoodEats.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodEats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {

        private readonly AccountService _accountService;

        public ProfilesController(AccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfile(string id)
        {
            try
            {
                Profile p = _accountService.GetProfileById(id);
                return Ok(p);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}