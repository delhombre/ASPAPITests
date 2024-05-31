using DemoASPTest.Domain.Entities;

namespace DemoASPTest.DAL.Interfaces;

public interface IPlayerRepository : IBaseRepository<Player, int>
{
    bool ExistByEmail(string email);

    bool ExistById(int id);
    Player? GetByEmail(string email);
}
