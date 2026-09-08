using System.Data;
using System.Data.SqlClient;
namespace BKS;
/// <summary>Shared connection/command lifetime. Queries and company filters belong to the caller.</summary>
internal sealed class SqlDataAccess
{
    private readonly string connectionString;
    public SqlDataAccess(string connectionString) => this.connectionString = connectionString;
    public TResult Execute<TResult>(string sql, CommandType type, Func<SqlCommand, TResult> operation,
    params SqlParameter[] parameters)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(sql, connection)
        {
            CommandType = type,
            CommandTimeout = 30
        };
        command.Parameters.AddRange(parameters);
        connection.Open();
        return operation(command);
    }
    public DataTable Table(string sql, CommandType type = CommandType.Text, params SqlParameter[] parameters)
    {
        return Execute(sql, type, command =>
        {
            using var adapter = new SqlDataAdapter(command);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }, parameters);
    }
    public IReadOnlyList<T> Query<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] parameters)
    {
        return Execute(sql, CommandType.Text, command =>
        {
            var result = new List<T>();
            using var reader = command.ExecuteReader();
            while (reader.Read()) result.Add(map(reader));
            return result;
        }, parameters);
    }
    public object? Scalar(string sql, CommandType type, params SqlParameter[] parameters) =>
    Execute(sql, type, command => command.ExecuteScalar(), parameters);
    public int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] parameters) =>
    Execute(sql, type, command => command.ExecuteNonQuery(), parameters);
}
