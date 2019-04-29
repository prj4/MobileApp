using System;

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
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public void Validate()
        {
            if (!Email.Contains("@")) throw new IncorrectEmailException("Incorrect email entered");
        }
    }
}