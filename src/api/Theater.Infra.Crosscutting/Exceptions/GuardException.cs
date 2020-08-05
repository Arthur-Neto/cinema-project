using System;

namespace Theater.Infra.Crosscutting.Exceptions
{
    public sealed class GuardException : Exception
    {
        public GuardException(string message)
            : base(message)
        { }
    }
}
