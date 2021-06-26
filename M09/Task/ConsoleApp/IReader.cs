using System.Collections.Generic;

namespace ConsoleApp
{
    public interface IReader<T>
    {
        public List<T> Read();
    }
}
