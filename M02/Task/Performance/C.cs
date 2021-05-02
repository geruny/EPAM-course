using System;

namespace Performance
{
    internal class C : IComparable<C>, IMemoryChecker
    {
        public int I;

        int IMemoryChecker.I { get => I; set => I = value; }

        public int CompareTo(C c)
        {
            if (c != null)
                return this.I.CompareTo(c.I);
            else
                throw new NullReferenceException("Cant compare object");
        }
    }
}
