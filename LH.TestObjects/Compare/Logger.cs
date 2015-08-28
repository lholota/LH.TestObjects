namespace LH.TestObjects.Compare
{
    using System;

    internal class Logger : ILoggerConfiguration
    {
        public void ToConsoleOutput()
        {
            throw new NotImplementedException();
        }

        public void Callback(Action<IComparisonContext> callback)
        {
            throw new NotImplementedException();
        }
    }
}
