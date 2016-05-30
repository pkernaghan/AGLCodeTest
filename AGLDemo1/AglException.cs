using System;

namespace AGLPetApiConsumer
{
    public class AglException : Exception
    {
        public AglException() :
            base(
            @"Error whilst executing Agl Pet Api Client. Please contact AGL Support Desk if you require further details"
            )
        {
        }

        public AglException(string message)
            : base(message)
        {
        }
    }
}