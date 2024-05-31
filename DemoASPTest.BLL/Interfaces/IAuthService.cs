using DemoASPTest.Domain.Entities;

namespace DemoASPTest.BLL.Interfaces;

public interface IAuthService
{
    public string Generate(Player player);
}
