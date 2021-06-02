using System.Collections.Generic;

namespace ConsoleApp
{
    public interface ISearchHandler<T>
    {
        public IEnumerable<T> SearchQuery(List<T> collection);
        public string GetInputValues(string flag);
        public string GetSortMethod(string sortValue);
    }
}
