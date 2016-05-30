using System.Collections.Generic;

namespace AGLPetApiConsumer.Models
{
    public class PetOwner
    {
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public IList<Pet> pets { get; set; }
    }
}