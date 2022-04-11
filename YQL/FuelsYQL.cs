using System.Text;
using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class FuelsYQL
    {        
        public FuelsYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;

         public Task<IResponse> CreateFuel(ulong id, string name, string fullName){
            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $fuel_id AS Uint64;
                    DECLARE $name AS Utf8;
                    DECLARE $fullname AS Utf8;

                    UPSERT INTO Fuels (fuel_id, name, fullname) VALUES
                    ($fuel_id, $name, $fullname);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$fuel_id", YdbValue.MakeUint64(id) },
                    { "$name", YdbValue.MakeUtf8(name) },
                    { "$fullname", YdbValue.MakeUtf8(fullName) },
                }
            );
            });
            response.Wait();
            return response;
        }
        public string GetFuel(ulong id){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT fuel_id,name,fullname FROM Fuels WHERE fuel_id == {id}
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$id", YdbValue.MakeUint64(id) }
                }
            );
            });
            response.Wait();
            ExecuteDataQueryResponse resp = (ExecuteDataQueryResponse)response.Result;
            var x = resp.Result.ResultSets[0].Rows;
            Fuels fuels = new Fuels(
                x[0][0].GetOptional().GetUint64(),
                x[0][1].GetOptionalUtf8().ToString(),
                x[0][2].GetOptional().GetUtf8().ToString());                

            return JsonSerializer.Serialize(fuels);
        }     
        public string GetFuels(){
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT fuel_id,name,fullname FROM Fuels
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                }
            );
            });
            response.Wait();
            ExecuteDataQueryResponse resp = (ExecuteDataQueryResponse)response.Result;
            var x = resp.Result.ResultSets[0].Rows;
            string s = "";
            for(int z = 0;z < x.Count();z++){
            Fuels fuels = new Fuels(
                x[z][0].GetOptional().GetUint64(),
                x[z][1].GetOptionalUtf8().ToString(),
                x[z][2].GetOptional().GetUtf8().ToString());  
                s += JsonSerializer.Serialize(fuels) + ", \n";               
            }
            return s;
        }        
    }
}