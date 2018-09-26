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
        private SQLiteConnection Connection;

        public FileInfo CurrentDatabase;

        public SQLiteDestinyEngine(FileInfo LocalPath)
        {
            InitDatabase(LocalPath.FullName);
        }

        internal void InitDatabase(String overridePath)
        {
            CurrentDatabase = new FileInfo(overridePath);
            if (CurrentDatabase.Exists)
            {
                var builder = new SQLiteConnectionStringBuilder { DataSource = CurrentDatabase.FullName };
                Connection = new SQLiteConnection(builder.ToString());
                Connection.Open();
                Connection.EnableExtensions(true);
                Connection.LoadExtension("SQLite.Interop.dll", "sqlite3_json_init");
            }
        }

        internal void Close()
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }

        private void ThrowIfConnectionUnavailable()
        {
            if (Connection == null)
            {
                throw new Exception("Database connection isn't open or configured");
            }
            else if (Connection != null && Connection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Database connection isn't open and hasn't been initialized");
            }
        }


        public async Task<Dictionary<long, String>> GetTableDump(string TableName)
        {
            return await await Task.Factory.StartNew(async () =>
            {
                ThrowIfConnectionUnavailable();
                var dataSet = new Dictionary<long, String>();
                var query = 
$@"SELECT json_tree.value,json_extract({TableName}.json, '$')
FROM {TableName}, json_tree({TableName}.json, '$')
WHERE json_tree.key = 'hash'";
                using (var command = new SQLiteCommand(query, Connection))
                {
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var body = reader.GetString(1);
                        var hash = reader.GetInt64(0);
                        dataSet.Add(hash, body);
                    }
                }
                return dataSet;
            }).ConfigureAwait(false);
        }

        public async Task<Dictionary<long, T>> GetTableDump<T>(string TableName)
        {
            return await await Task.Factory.StartNew(async () =>
            {
                ThrowIfConnectionUnavailable();
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
                ThrowIfConnectionUnavailable();
                var dataSet = new Dictionary<long, T>();
                var query = 
$@"SELECT json_tree.value,json_extract({TableName}.json, '$')
FROM {TableName}, json_tree({TableName}.json, '$')
WHERE json_tree.key = 'hash' and json_tree.value in ({GetArrayWhereBlock(itemHashes)})";
                using (var command = new SQLiteCommand(query, Connection))
                {
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

        public String GetArrayWhereBlock<T>(IEnumerable<T> Array)
        {
            var builder = new StringBuilder();

            foreach (var item in Array)
            {
                builder.Append($"{item},");
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}
