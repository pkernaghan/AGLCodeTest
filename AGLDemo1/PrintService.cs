using System;
using System.Collections.Generic;
using Serilog;

namespace AGLPetApiConsumer
{
    public class PrintService : IPrintService
    {
        public void PrintFormattedList(string listHeader, List<string> listContents)
        {
            try
            {
                PrintList(listHeader, listContents);
            }
            catch (Exception ex)
            {
                //SEQ Logging
                Log.Logger.Error(ex, @"The PrintService method 'PrintFormattedPetNamesWhereAnimalType'");

                //Note: deliberate rethrow to hide inner workings/ call stack info from being exposed to caller (security consideration/ option if being called bu external untrusted parties)
                throw new AglException();
            }
        }

        protected void PrintList(string title, List<string> list)
        {
            Console.WriteLine("\n\n" + title + "\n\n");

            if (list != null && list.Count > 0)
            {
                list.ForEach(item => Console.WriteLine(item + "\n"));
            }
        }


    }
}