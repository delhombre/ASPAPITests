using DemoASPTest.Domain.Entities;

namespace DemoASPTest.BLL.Interfaces;

public interface IPlayerService
{
    IEnumerable<Player> GetAll();
    Player GetById(int id);
    int Create(Player p);
    bool Update(int id, Player p);
    bool Delete(int id);
    Player Login(string email, string password);
    string LoginToken(string email, string password);
}
