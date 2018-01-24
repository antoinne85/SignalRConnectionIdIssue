using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalRConnectionIdIssue.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IHubContext<IssueHub> _hub;

        public TestController(IHubContext<IssueHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        // GET api/values
        [HttpGet]
        [Route("feedback")]
        public async Task<string> Get([FromQuery] string connectionId)
        {
            await _hub.Clients.All.SendAsync("receiveMessage", "This is a test message to everyone.");

            var possibleMultipleUsers = _hub.Clients.Users(new List<string> {connectionId});
            await possibleMultipleUsers.SendAsync("receiveMessage", "This is a test message to specific clients.");

            var singleUser = _hub.Clients.User(connectionId);
            await singleUser.SendAsync("receiveMessage", "This is a test message to exactly one client.");

            return "Done";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
