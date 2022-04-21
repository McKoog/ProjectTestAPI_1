using System.Text.Json;
using ProjectTestAPI_1.Services;
using Ydb.Sdk.Client;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace ProjectTestAPI_1.YQL
{
    public class UserYQL
    {
        public UserYQL(TableClient client)
        {
            this.client = client;
        }
        private TableClient client = MyYDBService.Client;
        public string RegisterUser(ulong userId, string name, string email, string password, string phone, string token){

            var response =  client.SessionExec(async session =>
                {
                    var query = @"
                    DECLARE $user_id AS Uint64;
                    DECLARE $name AS Utf8;
                    DECLARE $email AS Utf8;
                    DECLARE $password AS Utf8;
                    DECLARE $phone AS Utf8;
                    DECLARE $token as Utf8;

                    UPSERT INTO Users (user_id, name, email, password, phone, token) VALUES
                    ($user_id, $name, $email, $password, $phone, $token);
                ";

            return await session.ExecuteDataQuery(
                query: query,
                txControl: TxControl.BeginSerializableRW().Commit(),
                parameters: new Dictionary<string, YdbValue>
                {
                    { "$user_id", YdbValue.MakeUint64(userId) },
                    { "$name", YdbValue.MakeUtf8(name)},
                    { "$email", YdbValue.MakeUtf8(email) },
                    { "$password", YdbValue.MakeUtf8(password) },
                    { "$phone", YdbValue.MakeUtf8(phone) },
                    { "$token", YdbValue.MakeUtf8(token) },
                }
            );
            });
            return token;
        }
        public string LoginUser(string email, string password)
        {
            var response =  client.SessionExec(async session =>
                {
                    var query = @$"
                    SELECT token FROM Users WHERE email = '{email}' AND password = '{password}'
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
            if(resp.Result.ResultSets[0].Rows.Count<ResultSet.Row>() != 0){
            return x[0][0].GetOptionalUtf8().ToString();
            }
            else return "";
        }
        
    }
}