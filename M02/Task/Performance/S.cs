using System;

namespace Performance
{
    internal struct S : IComparable<S>, IMemoryChecker
    {
        public int I { get; set; }
        public int CompareTo(S s) => this.I.CompareTo(s.I);
    }
}
