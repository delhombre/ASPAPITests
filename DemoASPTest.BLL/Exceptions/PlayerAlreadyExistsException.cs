using System.Text;

namespace DemoASPTest.BLL.Exceptions;

public class PlayerAlreadyExistsException : Exception
{
    public List<string> ErrorMessages { get; set; }

    public PlayerAlreadyExistsException()
    {
        ErrorMessages = ["Player already exists"];
    }

    public PlayerAlreadyExistsException(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (string error in ErrorMessages)
        {
            sb.AppendLine(error);
        }
        return sb.ToString();
    }
}
