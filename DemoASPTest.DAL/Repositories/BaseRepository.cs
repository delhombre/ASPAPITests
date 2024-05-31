using System.Data;
using System.Data.SqlClient;
using DemoASPTest.DAL.Interfaces;

namespace DemoASPTest.DAL.Repositories;

public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
{
    private readonly string _tableName;
    private readonly string _columnIdName;
    protected readonly SqlConnection _connection;

    protected BaseRepository(SqlConnection connection, string tableName, string columnIdName)
    {
        _tableName = tableName;
        _columnIdName = columnIdName;
        _connection = connection;
    }

    protected abstract TEntity Convert(IDataRecord record);

    public abstract TId Create(TEntity entity);

    public IEnumerable<TEntity> GetAll()
    {
        using SqlCommand cmd = _connection.CreateCommand();
        cmd.CommandText =
            $@"SELECT * 
                                 FROM {_tableName}";

        _connection.Open();

        SqlDataReader r = cmd.ExecuteReader();

        while (r.Read())
        {
            yield return Convert(r);
        }

        _connection.Close();
    }

    public TEntity? GetById(TId id)
    {
        using SqlCommand cmd = _connection.CreateCommand();

        cmd.CommandText =
            $@"SELECT * 
                                 FROM {_tableName} 
                                 WHERE {_columnIdName} = @id";

        cmd.Parameters.AddWithValue("@id", id);

        _connection.Open();

        IDataReader reader = cmd.ExecuteReader();

        TEntity? entity = null;

        if (reader.Read())
        {
            entity = Convert(reader);
        }

        _connection.Close();

        return entity;
    }

    public abstract bool Update(TId id, TEntity entity);

    public bool Delete(TId id)
    {
        using SqlCommand cmd = _connection.CreateCommand();

        cmd.CommandText =
            $@"DELETE {_tableName} 
                                 WHERE {_columnIdName} = @id";

        cmd.Parameters.AddWithValue("@id", id);

        _connection.Open();

        int nbRows = cmd.ExecuteNonQuery();

        _connection.Close();

        return nbRows == 1;
    }
}
