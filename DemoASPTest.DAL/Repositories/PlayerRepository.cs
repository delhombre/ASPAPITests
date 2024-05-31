using System.Data;
using System.Data.SqlClient;
using DemoASPTest.DAL.Interfaces;
using DemoASPTest.Domain.Entities;

namespace DemoASPTest.DAL.Repositories;

public class PlayerRepository : BaseRepository<Player, int>, IPlayerRepository
{
    public PlayerRepository(SqlConnection connection)
        : base(connection, "Player", "Id") { }

    protected override Player Convert(IDataRecord record)
    {
        return new Player()
        {
            Id = (int)record["Id"],
            FirstName = (string)record["FirstName"],
            LastName = (string)record["LastName"],
            Email = (string)record["Email"],
            Password = (string)record["Password"]
        };
    }

    public override int Create(Player entity)
    {
        using SqlCommand command = _connection.CreateCommand();
        command.CommandText =
            @"INSERT INTO Player (FirstName, LastName, Email, Password)
            OUTPUT INSERTED.Id
             VALUES (@FirstName, @LastName, @Email, @Password)";
        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@Email", entity.Email);
        command.Parameters.AddWithValue("@Password", entity.Password);
        _connection.Open();
        int id = (int)command.ExecuteScalar();
        _connection.Close();
        return id;
    }

    public override bool Update(int id, Player entity)
    {
        using SqlCommand command = _connection.CreateCommand();
        command.CommandText =
            "UPDATE Player SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password WHERE Id = @Id";
        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@Email", entity.Email);
        command.Parameters.AddWithValue("@Password", entity.Password);
        command.Parameters.AddWithValue("@Id", id);
        _connection.Open();
        int nbRows = command.ExecuteNonQuery();
        _connection.Close();
        return nbRows == 1;
    }

    public bool ExistByEmail(string email)
    {
        using SqlCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Player WHERE Email = @Email";
        command.Parameters.AddWithValue("@Email", email);
        _connection.Open();
        int count = (int)command.ExecuteScalar();
        _connection.Close();
        return count > 0;
    }

    public bool ExistById(int id)
    {
        using SqlCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Player WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        _connection.Open();
        int count = (int)command.ExecuteScalar();
        _connection.Close();
        return count > 0;
    }

    public Player? GetByEmail(string email)
    {
        using SqlCommand command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM Player WHERE Email = @Email";
        command.Parameters.AddWithValue("@Email", email);
        _connection.Open();
        SqlDataReader record = command.ExecuteReader();
        Player? player = null;
        if (record.Read())
        {
            player = Convert(record);
        }
        _connection.Close();
        return player;
    }
}
