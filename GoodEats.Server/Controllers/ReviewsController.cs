using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GoodEats.Models;
using GoodEats.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodEats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {

        private readonly ReviewsService _rs;

        public ReviewsController(ReviewsService rs)
        {
            _rs = rs;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Review>> Create([FromBody] Review r)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                // REVIEW DO NOT TRUST THE CLIENT..... EVER
                r.CreatorId = userInfo.Id;
                Review newR = _rs.Create(r);
                // REVIEW cool inheritance thing account : profile
                newR.Creator = userInfo;
                return Ok(newR);

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // TODO you not me
        // [Authorize]
        // [HttpPut("{id}")]

        // public async Task<ActionResult<Restaurant>> Update(int id, [FromBody] Restaurant r)
        // {
        //     try
        //     {
        //         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        //         // REVIEW DO NOT TRUST THE CLIENT..... EVER
        //         r.Id = id;
        //         Restaurant newR = _rs.Update(r, userInfo.Id);
        //         // REVIEW cool inheritance thing account : profile
        //         newR.Owner = userInfo;
        //         return Ok(newR);

        //     }
        //     catch (System.Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        // [Authorize]
        // [HttpDelete("{id}")]

        // public async Task<ActionResult<string>> Remove(int id)
        // {
        //     try
        //     {
        //         // REVIEW DO NOT TRUST THE CLIENT..... EVER
        //         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        //         _rs.Remove(id, userInfo.Id);
        //         return Ok("Successfully Removed");

        //     }
        //     catch (System.Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

    }
}