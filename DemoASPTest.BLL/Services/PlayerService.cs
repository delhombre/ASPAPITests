using DemoASPTest.BLL.Exceptions;
using DemoASPTest.BLL.Interfaces;
using DemoASPTest.DAL.Interfaces;
using DemoASPTest.Domain.Entities;

namespace DemoASPTest.BLL.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IAuthService _authService;

    public PlayerService(IPlayerRepository playerRepository, IAuthService authService)
    {
        _playerRepository = playerRepository;
        _authService = authService;
    }

    public int Create(Player player)
    {
        bool error = false;
        List<string> errorMessages = [];

        if (_playerRepository.ExistByEmail(player.Email))
        {
            error = true;
            errorMessages.Add($"Player with email {player.Email} already exists");
        }

        if (error)
        {
            throw new PlayerAlreadyExistsException(errorMessages);
        }
        player.Password = BCrypt.Net.BCrypt.HashPassword(player.Password);
        return _playerRepository.Create(player);
    }

    public Player GetById(int id)
    {
        return _playerRepository.GetById(id)
            ?? throw new KeyNotFoundException($"Player with id {id} not found");
    }

    public IEnumerable<Player> GetAll()
    {
        return _playerRepository.GetAll();
    }

    public bool Update(int id, Player player)
    {
        if (!_playerRepository.ExistById(id))
        {
            throw new Exception("Player not found");
        }
        return _playerRepository.Update(id, player);
    }

    public bool Delete(int id)
    {
        if (!_playerRepository.ExistById(id))
        {
            throw new Exception("Player not found");
        }
        return _playerRepository.Delete(id);
    }

    public Player Login(string login, string password)
    {
        Player? player =
            _playerRepository.GetByEmail(login)
            ?? throw new KeyNotFoundException("User doesn't exists");
        if (!BCrypt.Net.BCrypt.Verify(password, player.Password))
        {
            throw new Exception("Wrong password");
        }

        return player;
    }

    public string LoginToken(string login, string password)
    {
        Player? player =
            _playerRepository.GetByEmail(login)
            ?? throw new KeyNotFoundException("User doesn't exists");
        if (!BCrypt.Net.BCrypt.Verify(password, player.Password))
        {
            throw new Exception("Wrong password");
        }

        return _authService.Generate(player);
    }
}
