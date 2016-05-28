using System;
using System.Collections.Generic;
using System.Linq;
using AGLPetApiClient.Common;
using AGLPetApiClient.Models;
using Serilog;
using System.Threading.Tasks;

namespace AGLPetApiClient
{
    //Note: Api Client to store assoicated functionality and avoid static methods anti-pattern
    public class AGLPetApiClient
    {
        #region Properties

        public AglApiService.AglApiService ApiService { get; private set; }

        #endregion

        #region Constructor

        static AGLPetApiClient()
        {
            //Note: To get SEQ logging working, please follow the instructions at the following URL: http://docs.getseq.net/ 
            //Note 2: If SEQ is not installed then nothing bad will happen. You just won't get any logging.
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Log.Information("AGLPetApiClient Starting...");
        }

        //Note: optional parameter for testing purposes i.e. injecting dependencies - and reuse
        public AGLPetApiClient(Uri apiUri = null)
        {
            if (apiUri == null)
            {
                //Note: apiUrl extracted into config for configurability/ maintianability
                var apiUrl = ConfigManager.GetConfigSetting(@"baseApiUrl");
                apiUri = new Uri(apiUrl);
            }

            ApiService = new AglApiService.AglApiService(apiUri);
        }

        #endregion

        #region Public Methods

        public void PrintFormattedPetNamesWhereAnimalTypeAsync(string petOwnersApiPath, string animalType = @"Cat")
        {
            try
            {
                if (string.IsNullOrEmpty(petOwnersApiPath))
                {
                    petOwnersApiPath = ConfigManager.GetConfigSetting(@"petOwnersApiPath");
                }

                var petOwnerList = ApiService.GetPetOwnerList(petOwnersApiPath);

                PrintPetNameWhereAnimalType(petOwnerList, animalType);
            }
            catch (Exception ex)
            {
                //SEQ Logging
                Log.Logger.Error(ex, @"The AGLPetApiClient method 'PrintFormattedPetNamesWhereAnimalType' threw an exception for Base Api Uri: {ApiUri} and Pet Owners Api Path : {petOwnersApiPath}.", ApiService.BaseApiUrl, petOwnersApiPath);

                //Note: deliberate rethrow to hide inner workings/ call stack info from being exposed to caller (security consideration/ option if being called bu external untrusted parties)
                throw new AglException();
            }
        }

        public void PrintFormattedPetNamesWhereAnimalType(string petOwnersApiPath, string animalType = @"Cat")
        {
            try
            {
                if (string.IsNullOrEmpty(petOwnersApiPath))
                {
                    petOwnersApiPath = ConfigManager.GetConfigSetting(@"petOwnersApiPath");
                }

                var petOwnerList = ApiService.GetPetOwnerList(petOwnersApiPath);

                PrintPetNameWhereAnimalType(petOwnerList, animalType);
            }
            catch (Exception ex)
            {
                //SEQ Logging
                Log.Logger.Error(ex, @"The AGLPetApiClient method 'PrintFormattedPetNamesWhereAnimalType' threw an exception for Base Api Uri: {ApiUri} and Pet Owners Api Path : {petOwnersApiPath}.", ApiService.BaseApiUrl, petOwnersApiPath);

                //Note: deliberate rethrow to hide inner workings/ call stack info from being exposed to caller (security consideration/ option if being called bu external untrusted parties)
                throw new AglException();
            }
        }

        #endregion

        #region Private Methods

        private void PrintPetNameWhereAnimalType(List<PetOwner> petOwners, string animalType)
        {
            FilterOnCriteriaAndPrintFormattedResults(petOwners, animalType, @"Male");
            FilterOnCriteriaAndPrintFormattedResults(petOwners, animalType, @"Female");
        }

        private void FilterOnCriteriaAndPrintFormattedResults(List<PetOwner> petOwners, string animalType,
            string petOwnerGender)
        {
            var petNames = GetPetNametByCriteria(petOwners, animalType, petOwnerGender);

            PrintList(petOwnerGender, petNames);
        }

        private List<string> GetPetNametByCriteria(List<PetOwner> petOwners, string animalTypeFilter,
            string petOwnerGenderFilter)
        {
            var petNames = new List<string>();

            var correctGender = petOwners.Where(po => po.gender.ToLower().Equals(petOwnerGenderFilter.ToLower())).ToList();

            // Note: Null check on pets because pets is nullable property
            var correctGenderAndHasPets = correctGender.Where(x => x.pets != null).SelectMany(x => x.pets).ToList();

            var correctGenderAndHasPetsOfCorrectType =
                correctGenderAndHasPets.Where(p => p.type.ToLower().Equals(animalTypeFilter.ToLower())).ToList();

            petNames = correctGenderAndHasPetsOfCorrectType.OrderBy(x => x.name).Select(p => p.name).ToList();

            return petNames;
        }

        private void PrintList(string title, List<string> list)
        {
            Console.WriteLine("\n\n" + title + "\n\n");

            list.ForEach(item => Console.WriteLine(item + "\n"));
        }

        #endregion
    }
}