using System;

namespace PaystackSdk.Scripts.Exceptions
{
    public class PrefsInvalidException : Exception
    {
        public PrefsInvalidException() : base("Prefs file is invalid or corrupted") { }
    }
}
