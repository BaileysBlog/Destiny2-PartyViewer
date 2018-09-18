using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace D2DataAccess.SqLite
{
    public class SQLiteDestinyEngine
    {
        private readonly SQLiteConnection Connection;

        public SQLiteDestinyEngine(FileInfo LocalPath)
        {
            var builder = new SQLiteConnectionStringBuilder { DataSource = LocalPath.FullName };
            Connection = new SQLiteConnection(builder.ToString());
            Connection.Open();
        }


        public async Task<Dictionary<long, String>> GetTableDump(string TableName)
        {
            return await await Task.Factory.StartNew(async () =>
            {
                var dataSet = new Dictionary<long, String>();
                var query = $"select id, json from {TableName}";
                using (var command = new SQLiteCommand(query, Connection))
                {
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var body = reader.GetString(1);
                        dataSet.Add(reader.GetInt64(0), body);
                    }
                }
                return dataSet;
            }).ConfigureAwait(false);
        }

        public async Task<Dictionary<long, T>> GetTableDump<T>(string TableName)
        {
            return await await Task.Factory.StartNew(async () =>
            {
                var dataSet = new Dictionary<long, T>();

                var stringData = await GetTableDump(TableName);

                foreach (var data in stringData)
                {
                    dataSet.Add(data.Key, JsonConvert.DeserializeObject<T>(data.Value));
                }

                return dataSet;
            }).ConfigureAwait(false);
        }

        public async Task<Dictionary<long, T>> GetDetailedInformationFromHash<T>(string TableName, params long[] itemHashes)
        {
            return await await Task.Factory.StartNew(async () =>
            {
                var dataSet = new Dictionary<long, T>();
                var query = $"select id, json from '{TableName}' where id in (@hashes)";
                using (var command = new SQLiteCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@hashes", itemHashes);
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var body = reader.GetString(1);
                        dataSet.Add(reader.GetInt64(0), JsonConvert.DeserializeObject<T>(body));
                    }
                }
                return dataSet;
            }).ConfigureAwait(false);
        }
    }
}
