using System;

namespace Performance
{
    class C : IComparable<C>
    {
        public int I;

        public int CompareTo(C c)
        {
            if (c != null)
                return this.I.CompareTo(c.I);
            else
                throw new NullReferenceException("Cant compare object");
        }
    }
}
