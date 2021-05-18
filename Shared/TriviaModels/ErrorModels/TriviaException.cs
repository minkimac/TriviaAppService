using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaApi.Models
{
    public class TriviaException : Exception
    {
        public TriviaException() : base() { }

        public TriviaException(string message) : base(message) { }
    }
}
