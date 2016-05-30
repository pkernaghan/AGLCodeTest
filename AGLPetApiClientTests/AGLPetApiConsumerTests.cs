using AGLPetApiConsumer;
using AGLPetApiConsumer.AglApiService;
using FakeItEasy;
using NUnit.Framework;

namespace AGLPetApiConsumerTests
{
    [TestFixture]
    public class AGLPetApiConsumerTests
    {
        [Test]
        [Ignore("To be implemented")]
        public void PrintFormattedPetNamesWhereAnimalType_When_Expects()
        {
            //Arrange
            var printService = A.Fake<IPrintService>();
            var aglApiService = A.Fake<IAglApiService>();

            var testConsumer = new AGLPetApiConsumer.AGLPetApiConsumer(aglApiService, printService);

            ////Act
            testConsumer.PrintFormattedPetNamesWhereAnimalType();

            //Assert - todo 
        }
    }
}
