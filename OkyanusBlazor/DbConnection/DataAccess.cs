using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace OkyanusBlazor.DbConnection;

public class DataAccess : IDataAccess
{
    private readonly IConfiguration _config;

    public DataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<object> GetScalar<U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180)
    {
        var constr = _config.GetConnectionString("dbOkyanusConStr");
        using IDbConnection connection = new SqlConnection(constr);
        return await connection.ExecuteScalarAsync(query, parameters, commandType: commandType, commandTimeout: timeout);
    }

    public async Task<T> GetFirstRow<T, U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180)
    {
        var constr = _config.GetConnectionString("dbOkyanusConStr");
        using IDbConnection connection = new SqlConnection(constr);
        return await connection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType, commandTimeout: timeout);
    }

    public async Task<T> GetSingleRow<T, U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180)
    {
        var constr = _config.GetConnectionString("dbOkyanusConStr");
        using IDbConnection connection = new SqlConnection(constr);
        return await connection.QuerySingleOrDefaultAsync<T>(query, parameters, commandType: commandType, commandTimeout: timeout);
    }

    public async Task<IEnumerable<T>> GetAllData<T, U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180)
    {
        var constr = _config.GetConnectionString("dbOkyanusConStr");
        using IDbConnection connection = new SqlConnection(constr);
        return await connection.QueryAsync<T>(query, parameters, commandType: commandType, commandTimeout: timeout);
    }

    public async Task<SqlMapper.GridReader> GetMultiData<U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180)
    {
        var constr = _config.GetConnectionString("dbOkyanusConStr");
        using IDbConnection connection = new SqlConnection(constr);
        var multiData = await connection.QueryMultipleAsync(query, parameters, commandType: commandType, commandTimeout: timeout);
        return multiData;
    }

    public async Task SaveData<T>(string query, T parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180)
    {
        var constr = _config.GetConnectionString("dbOkyanusConStr");
        using IDbConnection connection = new SqlConnection(constr);
        await connection.ExecuteAsync(query, parameters, commandType: commandType, commandTimeout: timeout);
    }
}


//using System.Data;
//using System.Data.SqlClient;
//using Dapper;
//using Microsoft.Extensions.Configuration;

//namespace DbAccess;

//public class DataAccess : IDataAccess
//{
//    private readonly string connectionString;
//    public DataAccess(IConfiguration _config)
//    {
//        connectionString = _config.GetConnectionString("dbOkyanusConnStr");
//    }

//    //public async Task<object> GetScalarObject_Async<U>(string sql, U parameters, CommandType commandType)
//    //{

//    //}

//    public async Task<T> QuerySingleRow_Async<T, U>(string sql, U parameters, CommandType commandType)
//    {
//        using IDbConnection connection = new SqlConnection(connectionString);
//        var satir = await connection.QuerySingleAsync<T>(sql, parameters, commandType: commandType);
//        return satir;
//    }

//    //ASenkron olarak "select/load/get" liste/tablo/data işlemleri için
//    public async Task<List<T>> GetData_Async<T, U>(string sql, U parameters, CommandType commandType)
//    {
//        using IDbConnection connection = new SqlConnection(connectionString);
//        IEnumerable<T> rows = await connection.QueryAsync<T>(sql, parameters, commandType: commandType);
//        return rows.ToList();
//    }

//    //Senkron olarak "select/load/get" liste/tablo/data işlemleri için
//    public IEnumerable<T> GetData_Sync<T, U>(string sql, U parameters, CommandType commandType)
//    {
//        using IDbConnection connection = new SqlConnection(connectionString);
//        IEnumerable<T> rows = connection.Query<T>(sql, parameters, commandType: commandType);
//        return rows;
//    }

//    //Senkron olarak insert/Update/delete işlemleri için
//    public void ExecData_Sync<T>(string sql, T parameters, CommandType commandType)
//    {
//        using IDbConnection connection = new SqlConnection(connectionString);
//        connection.Execute(sql, parameters, commandType: commandType);
//    }

//    //public async Task ExecData_Async<T>(string sql, T parameters, CommandType commandType)
//    //{
//    //    using IDbConnection connection = new SqlConnection(connectionString);
//    //    await connection.ExecuteAsync(sql, parameters, commandType: commandType);
//    //}

//    //ASenkron olarak insert/Update/delete işlemleri için
//    //public async Task ExecData_Async<T>(string sql, T parameters, CommandType commandType)
//    //{
//    //    using IDbConnection connection = new SqlConnection(connectionString);
//    //    var numberOfRowsAffected = await connection.ExecuteAsync(sql, parameters, commandType: commandType);
//    //    //return numberOfRowsAffected;
//    //}

//    //ASenkron olarak dataset (çoklu tablo) veri çekme işlemleri için
//    public async Task<SqlMapper.GridReader> GetDataset_Async<T, U>(string sql, U parameters, CommandType commandType)
//    {
//        using IDbConnection connection = new SqlConnection(connectionString);
//        connection.Open();
//        var sonuclar = connection.QueryMultipleAsync(sql, parameters, commandType: commandType);
//        return await sonuclar;
//    }

//    public Task<T> GetScalar_Async<T, U>(string sql, U parameters, CommandType commandType)
//    {
//        using IDbConnection connection = new SqlConnection(connectionString);
//        var sonuc =  connection.ExecuteScalarAsync<T>(sql, parameters, commandType: commandType);
//        return sonuc;
//    }
//}   