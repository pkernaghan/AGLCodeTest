using System;

namespace AGLPetApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome to the AGL Pet Api Client Demo");
            
            Console.WriteLine(@"Please wait whilst the results are being retreived....");

            var aglPetApiClient = new AGLPetApiClient();

            aglPetApiClient.PrintFormattedPetNamesWhereAnimalType(@"");

            Console.ReadLine();
        }
    }
}