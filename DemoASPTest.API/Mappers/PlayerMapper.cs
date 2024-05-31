using DemoASPTest.API.Models;
using DemoASPTest.Domain.Entities;

namespace DemoASPTest.API.Mappers;

public static class PlayerMapper
{
    public static PlayerSummaryDTO ToSummary(this Player player)
    {
        return new PlayerSummaryDTO
        {
            Id = player.Id,
            FullName = player.FirstName + " " + player.LastName,
            Email = player.Email,
        };
    }

    public static PlayerDetailsDTO ToDetails(this Player player)
    {
        return new PlayerDetailsDTO
        {
            Id = player.Id,
            FirstName = player.FirstName,
            LastName = player.LastName,
            Email = player.Email
        };
    }

    public static Player ToEntity(this PlayerRegisterFormDTO player)
    {
        return new Player
        {
            FirstName = player.FirstName,
            LastName = player.LastName,
            Email = player.Email,
            Password = player.Password
        };
    }
}
