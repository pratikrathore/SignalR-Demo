using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using server.Hubs;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushController : ControllerBase
    {
        private IHubContext<PushHub> context;

        public PushController(IHubContext<PushHub> hub)
        {
            this.context = hub;
        }

        // POST api/push
        [HttpPost]
        public async void Post([FromBody] string workstation)
        {
            await context.Clients.All.SendAsync("Push", workstation);
        }
    }
}
