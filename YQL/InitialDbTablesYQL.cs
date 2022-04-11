using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class InitialDbTablesYQL
    {
        public InitialDbTablesYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        
        public Task<IResponse> CreateDbTables(){
            var response = client.SessionExec(async session =>
                {
                    return await session.ExecuteSchemeQuery(@"
                    CREATE TABLE Stations (
                    station_id Uint64,
                    enable bool,
                    name Utf8,
                    adress Utf8,
                    location Json,
                    PRIMARY KEY (station_id)
                );
                    CREATE TABLE Fuels (
                    fuel_id Uint64,
                    name Utf8,
                    fullname Utf8,
                    PRIMARY KEY (fuel_id)
                );
                ");
            });
            return response;
        }
    }
}