using System.Security.Claims;
using DemoASPTest.API.Mappers;
using DemoASPTest.API.Models;
using DemoASPTest.BLL.Interfaces;
using DemoASPTest.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPTest.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlayerSummaryDTO>> GetAll()
    {
        return _playerService.GetAll().Select(player => player.ToSummary()).ToList();
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDetailsDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PlayerDetailsDTO> GetById([FromRoute] int id)
    {
        Claim? claimId = User.Claims.FirstOrDefault(
            claim => claim.Type == ClaimTypes.NameIdentifier
        )!;
        int adminId = int.Parse(claimId.Value);
        if (adminId != id)
        {
            return Unauthorized("You are not authorized to access this resource");
        }
        try
        {
            return _playerService.GetById(id).ToDetails();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete([FromRoute] int id)
    {
        try
        {
            bool isDeleted = _playerService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Update([FromRoute] int id, [FromBody] PlayerRegisterFormDTO player)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            bool isUpdated = _playerService.Update(id, player.ToEntity());
            if (isUpdated)
            {
                return NoContent();
            }
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        return BadRequest();
    }
}
