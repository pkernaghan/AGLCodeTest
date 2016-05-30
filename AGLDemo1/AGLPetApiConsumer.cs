using System;
using System.Collections.Generic;
using System.Linq;
using AGLPetApiConsumer.Common;
using AGLPetApiConsumer.Models;
using Serilog;
using System.Threading.Tasks;
using AGLPetApiConsumer.AglApiService;

namespace AGLPetApiConsumer
{
    public class AGLPetApiConsumer
    {
        #region Properties

        public IAglApiService AglApiService { get; private set; }
        public IPrintService PrintService { get; private set; }

        #endregion

        #region Constructor

        static AGLPetApiConsumer()
        {
            //Note: To get SEQ logging working, please follow the instructions at the following URL: http://docs.getseq.net/ 
            //Note 2: If SEQ is not installed then nothing bad will happen. You just won't get any logging.
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Log.Information("AGLPetApiConsumer Starting...");
        }

        public AGLPetApiConsumer(Uri baseApiUri = null): this(baseApiUri, new PrintService())
        {
        }

        public AGLPetApiConsumer(Uri baseApiUri, IPrintService printService)
        {
            if (baseApiUri == null && String.IsNullOrEmpty(ConfigManager.GetConfigSetting(@"baseApiUrl")))
            {
                throw new ArgumentNullException("Error: baseApiUri cannot be null. It must either be defined in the Construcotr or via the config file app setting.");
            }
            
            if (printService == null)
            {
                throw new ArgumentNullException("Error: printService cannot be null.");
            }

            if (baseApiUri != null)
            {
                AglApiService = new AglApiService.AglApiService(baseApiUri);
            }
            else
            {
                var configUri = new Uri(ConfigManager.GetConfigSetting(@"baseApiUrl"));
                AglApiService = new AglApiService.AglApiService(configUri);
            }

            PrintService = printService;
        }

        //Note: Constructor for testing purposes and extensiion i.e. injecting dependencies - and reuse
        public AGLPetApiConsumer(IAglApiService aglApiService, IPrintService printService)
        {
            AglApiService = aglApiService;
            PrintService = printService;
        }

        #endregion

        #region Public Methods

        public void PrintFormattedPetNamesWhereAnimalType(string animalType = @"Cat")
        {
            try
            {
                var petOwnersApiPath = ConfigManager.GetConfigSetting(@"petOwnersApiPath");

                var petOwnerList = AglApiService.GetPetOwnerList(petOwnersApiPath);

                PrintPetNameWhereAnimalTypeByGender(petOwnerList, animalType, new string[] {@"Male", @"Female"});
            }
            catch (Exception ex)
            {
                //SEQ Logging
                Log.Logger.Error(ex, @"The AGLPetApiConsumer method 'PrintFormattedPetNamesWhereAnimalType' threw an exception for Base Api Uri: {ApiUri} and Animal Type : {animalType}.", AglApiService.BaseApiUrl, animalType);

                //Note: deliberate rethrow to hide inner workings/ call stack info from being exposed to caller (security consideration/ option if being called bu external untrusted parties)
                throw new AglException();
            }
        }

        #endregion

        #region Private Methods

        private void PrintPetNameWhereAnimalTypeByGender(List<PetOwner> petOwners, string animalType, string[] genderCriteriaInOrder)
        {
            foreach (var gender in genderCriteriaInOrder)
            {
                FilterOnCriteriaAndPrintFormattedResults(petOwners, animalType, gender);
            }
        }

        private void FilterOnCriteriaAndPrintFormattedResults(List<PetOwner> petOwners, string animalType, string petOwnerGender)
        {
            var petNamesByGender = GetPetNametListByAnimalTypeAndOwnerGender(petOwners, animalType, petOwnerGender);

            PrintService.PrintFormattedList(petNamesByGender.Gender, petNamesByGender.PetNameList);
        }

        private PetNamesByGender GetPetNametListByAnimalTypeAndOwnerGender(List<PetOwner> petOwners, string animalType, string petOwnerGender)
        {
            if (string.IsNullOrEmpty(petOwnerGender))
            {
                throw new ArgumentNullException(@"Error: petOwnerGender must be specified.");
            }

            if (string.IsNullOrEmpty(animalType))
            {
                throw new ArgumentNullException(@"Error: animalType must be specified.");
            }

            var result = new PetNamesByGender();
            result.Gender = petOwnerGender;

            if (petOwners != null && petOwners.Count > 0)
            {
                var correctGender = petOwners.Where(po => po.gender.ToLower().Equals(petOwnerGender.ToLower())).ToList();

                // Note: Null check on pets because pets is nullable property
                var correctGenderAndHasPets = correctGender.Where(x => x.pets != null).SelectMany(x => x.pets).ToList();

                var correctGenderAndHasPetsOfCorrectType =
                    correctGenderAndHasPets.Where(p => p.type.ToLower().Equals(animalType.ToLower())).ToList();

                var petNamesList =
                    correctGenderAndHasPetsOfCorrectType.OrderBy(x => x.name).Select(p => p.name).ToList();

                result.PetNameList = petNamesList;
            }

            return result;
        }

        #endregion
    }
}

public class PetNamesByGender
{
    public string Gender { get; set; }
    public List<string> PetNameList { get; set; }

    public PetNamesByGender()
    {
        Gender = String.Empty;
        PetNameList = new List<string>();
    }
}