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
    public class RestaurantsController : ControllerBase
    {

        private readonly RestaurantsService _rs;

        public RestaurantsController(RestaurantsService rs)
        {
            _rs = rs;
        }

        [HttpGet]
        public ActionResult<List<Restaurant>> GetAll()
        {
            try
            {
                List<Restaurant> restaurants = _rs.GetAll();
                return Ok(restaurants);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get(int id)
        {
            try
            {
                Restaurant restaurant = _rs.Get(id);
                return Ok(restaurant);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/reviews")]
        public ActionResult<List<Review>> GetReviews(int id)
        {
            try
            {
                List<Review> reviews = _rs.GetReviews(id);
                return Ok(reviews);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant r)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                // REVIEW DO NOT TRUST THE CLIENT..... EVER
                r.OwnerId = userInfo.Id;
                Restaurant newR = _rs.Create(r);
                // REVIEW cool inheritance thing account : profile
                newR.Owner = userInfo;
                return Ok(newR);

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]

        public async Task<ActionResult<Restaurant>> Update(int id, [FromBody] Restaurant r)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                // REVIEW DO NOT TRUST THE CLIENT..... EVER
                r.Id = id;
                Restaurant newR = _rs.Update(r, userInfo.Id);
                // REVIEW cool inheritance thing account : profile
                newR.Owner = userInfo;
                return Ok(newR);

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult<string>> Remove(int id)
        {
            try
            {
                // REVIEW DO NOT TRUST THE CLIENT..... EVER
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _rs.Remove(id, userInfo.Id);
                return Ok("Successfully Removed");

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}