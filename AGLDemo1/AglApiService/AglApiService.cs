using System;
using System.Collections.Generic;
using AGLPetApiConsumer.Common;
using AGLPetApiConsumer.Models;

namespace AGLPetApiConsumer.AglApiService
{
    public class AglApiService : IAglApiService
    {
        #region Properties

        public Uri BaseApiUrl { get; private set;}

        #endregion

        #region Constructor

        public AglApiService(Uri apiUri)
        {
            if (apiUri == null)
            {
                throw new ArgumentNullException(@"Error in AglApiService. ApiUri cannot be null");
            }

            BaseApiUrl = apiUri;
        }

        #endregion

        #region Public Methods

        public List<PetOwner> GetPetOwnerList(string apiPath)
        {
            if (String.IsNullOrEmpty(apiPath))
            {
                throw new ArgumentNullException(@"Error in AglApiService. ApiPath cannot be null");
            }

            var fullApiUrl = new Uri(BaseApiUrl, apiPath);

            var webRespone = HttpClient.ExecuteGetWebRequest(fullApiUrl);

            var petOwnersList = HttpClient.ParseJsonWebResponse<List<PetOwner>>(webRespone);

            return petOwnersList;
        }

        #endregion
    }
}