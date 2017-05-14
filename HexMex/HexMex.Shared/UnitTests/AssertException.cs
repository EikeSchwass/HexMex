using System;

namespace HexMex.UnitTests
{
    public class AssertException : Exception
    {
        public AssertException(string message) : base(message)
        {
        }
    }
}