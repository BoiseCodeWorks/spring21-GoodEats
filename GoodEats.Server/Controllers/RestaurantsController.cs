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
    }
}