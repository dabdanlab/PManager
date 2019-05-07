using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace PManager.Services
{
    [Serializable]
    internal class AppException : Exception
    {

        public AppException() : base()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}