using System;
using System.Collections.Generic;
using AGLPetApiConsumer.Models;

namespace AGLPetApiConsumer.AglApiService
{
    public interface IAglApiService
    {
        Uri BaseApiUrl { get; }

        List<PetOwner> GetPetOwnerList(string apiPath);
    }
}