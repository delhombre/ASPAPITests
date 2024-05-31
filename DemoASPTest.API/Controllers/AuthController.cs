using DemoASPTest.API.Mappers;
using DemoASPTest.API.Models;
using DemoASPTest.BLL.Interfaces;
using DemoASPTest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPTest.API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public AuthController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Player> Register(PlayerRegisterFormDTO player)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            int id = _playerService.Create(player.ToEntity());
            return Created(
                "http://localhost:5220/player/" + id,
                _playerService.GetById(id).ToSummary()
            );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Player> Login(PlayerLoginFormDTO player)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            return Ok(_playerService.LoginToken(player.Email, player.Password));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
