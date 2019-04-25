using System;
using System.Collections.Generic;
using System.Text;

namespace Photobook.Models
{
    public class IncorrectEmailException : Exception
    {
        public IncorrectEmailException(string msg)
        {
            Message = msg;
        }
        public override string Message { get; }
    }

    public class Host
    {
        public Host()
        {

        }

        public void Validate()
        {
            if (!Email.Contains("@"))
            {
                throw new IncorrectEmailException("Incorrect email entered");
            }
        }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        

    }


}
