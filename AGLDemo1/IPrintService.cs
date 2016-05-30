using System.Collections.Generic;

namespace AGLPetApiConsumer
{
    public interface IPrintService
    {
        void PrintFormattedList(string listHeader, List<string> listContents);
    }
}