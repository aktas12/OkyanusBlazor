using System.Data;
using Dapper;

namespace OkyanusBlazor.DbConnection;

public interface IDataAccess
{

    /// <summary>
    /// The ExecuteScalar is an extension method that can be called from any object of type IDbConnection.
    /// It executes the query, and returns the "first column" of the "first row" in the result set returned by the query.
    /// The additional columns or rows are ignored.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<object> GetScalar<U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180);

    /// <summary>
    /// The QueryFirstOrDefaultAsync can execute a query and map asynchronously the first result, or a default value if the sequence contains no elements.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<T> GetFirstRow<T, U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180);

    /// <summary>
    /// The QuerySingleOrDefaultAsync can execute a query and map asynchronously the first result, or a default value if the sequence is empty;
    /// this method throws an exception if there is more than one element in the sequence.
    /// /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<T> GetSingleRow<T, U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180);

    Task<IEnumerable<T>> GetAllData<T, U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180);

    /// <summary>
    /// ASenkron olarak dataset (çoklu tablo) veri çekme işlemleri için
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    Task<SqlMapper.GridReader> GetMultiData<U>(string query, U parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180);

    Task SaveData<T>(string query, T parameters, CommandType commandType = CommandType.StoredProcedure, int timeout = 180);
}


//using System.Data;
//using Dapper;

//namespace SqlDataAccess.DbAccess;

//public interface IDataAccess
//{
//    Task<T> GetScalar_Async<T, U>(string sql, U parameters, CommandType commandType);
//    void ExecData_Sync<T>(string sql, T parameters, CommandType commandType);
//    Task<SqlMapper.GridReader> GetDataset_Async<T, U>(string sql, U parameters, CommandType commandType);
//    Task<List<T>> GetData_Async<T, U>(string sql, U parameters, CommandType commandType);
//    IEnumerable<T> GetData_Sync<T, U>(string sql, U parameters, CommandType commandType);
//    Task<T> QuerySingleRow_Async<T, U>(string sql, U parameters, CommandType commandType);
//}