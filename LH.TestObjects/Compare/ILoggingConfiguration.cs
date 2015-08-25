namespace LH.TestObjects.Compare
{
    using System;

    public interface ILoggingConfiguration
    {
        void ToConsoleOutput();
        void Callback(Action<object> callback);
    }
}