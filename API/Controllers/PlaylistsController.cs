using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Playlists;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlaylistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Playlist>>> List()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }


        [HttpDelete("{Name}")]
        public async Task<ActionResult<Unit>> Delete(string name)
        {
            return await _mediator.Send(new Delete.Command{Name = name});
        }

        [HttpPost("{Name}")]
        public async Task<ActionResult<Unit>> Add(Add.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}