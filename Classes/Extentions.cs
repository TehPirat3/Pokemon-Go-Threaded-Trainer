/*using POGOProtos.Networking.Requests;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon_Go_Threaded_Trainer.Classes
{
    public static class Extentions
    {
        private static readonly HttpClient _httpClient;
        private Request.Types.UnknownAuth _unknownAuth;

        public static async Task SetServer(this Client client)
        {
            var serverRequest = RequestBuilder.GetInitialRequest(AccessToken, client.AuthType, client.CurrentLatitude, client.CurrentLongitude, client.CurrentAltitude,
    RequestType.GET_PLAYER, RequestType.GET_HATCHED_OBJECTS, RequestType.GET_INVENTORY,
    RequestType.CHECK_AWARDED_BADGES, RequestType.DOWNLOAD_SETTINGS);
            var serverResponse = await _httpClient.PostProto(Resources.RpcUrl, serverRequest);

            if (serverResponse.Auth == null)
                throw new AccessTokenExpiredException();

            _unknownAuth = new Request.Types.UnknownAuth
            {
                Unknown71 = serverResponse.Auth.Unknown71,
                Timestamp = serverResponse.Auth.Timestamp,
                Unknown73 = serverResponse.Auth.Unknown73
            };

            _apiUrl = serverResponse.ApiUrl;
        }
    }
}
*/