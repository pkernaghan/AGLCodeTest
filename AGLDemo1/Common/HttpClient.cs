using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Serilog;

namespace AGLPetApiClient.Common
{
    public static class HttpClient
    {
        public static WebResponse ExecuteGetWebRequest(Uri url)
        {
            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            var webRequest = (HttpWebRequest) WebRequest.Create(url);

            //Note: Performance related - set proxy to null to increase performance
            //Note: Performance related settings in app.config i.e. increase ServicePointManager.DefaultConnectionLimit from default 2 to 48 etc
            webRequest.Proxy = null;

            webRequest.Method = @"GET";

            WebResponse webResponse = webRequest.GetResponse();

            return webResponse;
        }

        public static U ParseJsonWebResponse<U>(WebResponse webResponse)
        {
            U result;

            try
            {
                using (var responseStream = webResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    var responseAsString = reader.ReadToEnd();

                    result = JsonConvert.DeserializeObject<U>(responseAsString);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex,
                    @"AGLPetApiClientError HttpClient error. Unable to parse http response where response is {response}.  Please ensure apiUrl is correctly configured and endpoint is available.",
                    webResponse);

                throw;
            }

            return result;
        }
    }
}