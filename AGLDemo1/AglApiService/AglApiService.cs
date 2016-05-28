using System;
using System.Collections.Generic;
using AGLPetApiClient.Common;
using AGLPetApiClient.Models;

namespace AGLPetApiClient.AglApiService
{
    public class AglApiService
    {
        #region Properties

        public Uri BaseApiUrl { get; private set; }

        #endregion

        #region Constructor

        public AglApiService(Uri apiUri)
        {
            BaseApiUrl = apiUri;
        }

        #endregion

        #region Public Methods
        
        public List<PetOwner> GetPetOwnerList(string apiPath)
        {
            if (String.IsNullOrEmpty(apiPath))
            {
                throw new ArgumentNullException(@"apiPath cannot be null");
            }

            var fullApiUrl = new Uri(BaseApiUrl, apiPath);

            var webRespone = HttpClient.ExecuteGetWebRequest(fullApiUrl);

            var petOwnersList = HttpClient.ParseJsonWebResponse<List<PetOwner>>(webRespone);

            return petOwnersList;
        }

        #endregion
    }
}