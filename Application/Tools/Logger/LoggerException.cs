using System;
using System.Runtime.Serialization;

namespace Application.Tools.Logger;

[Serializable]
public class LoggerException : Exception
{
    public LoggerException() { }

    public LoggerException(string message) 
        : base(message) {}

    public LoggerException(string message, Exception inner) 
        : base(message, inner) {}

    protected LoggerException(
        SerializationInfo info, 
        StreamingContext context) 
        : base(info, context) {}
}