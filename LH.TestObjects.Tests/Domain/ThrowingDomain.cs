using System;

namespace LH.TestObjects.Tests.Domain
{
    public class ThrowingDomain
    {
        public string DummyProp
        {
            get { throw new Exception("The property should not be accessed."); }
        }
    }
}