using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApi.Controllers
{
    public class DemoController : ControllerBase
    {
        [HttpGet("/raw")]
        public ActionResult Raw()
        {
            return Ok("Looks Good!");
        }

        [HttpGet("/topics")]
        [Authorize]
        public ActionResult GetTopics()
        {
            return Ok(new string[] { "Dog", "Cat", "Mouse", this.PreferredUserName() ?? "Unknown" });
        }
    }
    public static class UserExtensions
    {
        public static string? PreferredUserName(this ControllerBase controller)
        {
            return controller.User.Claims.Where(c => c.Type == "preferred_username").Select(c => c.Value).SingleOrDefault();
        }
    }
}

