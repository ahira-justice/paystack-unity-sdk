using System;

namespace PaystackSdk.Scripts.Exceptions
{
    public class PrefsDoesNotExistException : Exception
    {
        public PrefsDoesNotExistException() : base("Prefs file does not exist") { }
    }
}
