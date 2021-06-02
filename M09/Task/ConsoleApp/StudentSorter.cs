using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class StudentSorter
    {
        private List<Student> _students;
        public IReader<Student> StudentsReader { get; set; }
        public ISearchHandler<Student> SearchHandler { get; set; }

        public StudentSorter(IReader<Student> studentsReader, ISearchHandler<Student> searchHandler)
        {
            StudentsReader = studentsReader;
            SearchHandler = searchHandler;
        }

        public IEnumerable<Student> GetStudents()
        {
            _students = StudentsReader.Read();

            var resQuery = SearchHandler.SearchQuery(_students);
            resQuery = SortQuery(resQuery);

            return resQuery;
        }

        private IEnumerable<Student> SortQuery(IEnumerable<Student> resQuery)
        {
            var sortValue = SearchHandler.GetInputValues("-sort");
            var sortMethod = SearchHandler.GetSortMethod(sortValue);

            return sortMethod switch
            {
                "asc" => sortValue switch
                {
                    "name" => resQuery.OrderBy(n => n.LastName),
                    "mark" => resQuery.OrderBy(n => n.Test.Mark),
                    "date" => resQuery.OrderBy(n => n.Test.DateTime),
                    "test" => resQuery.OrderBy(n => n.Test.Subject),
                    _ => throw new ArgumentException("Unknown sort value")
                },
                "desc" => sortValue switch
                {
                    "name" => resQuery.OrderByDescending(n => n.LastName),
                    "mark" => resQuery.OrderByDescending(n => n.Test.Mark),
                    "date" => resQuery.OrderByDescending(n => n.Test.DateTime),
                    "test" => resQuery.OrderByDescending(n => n.Test.Subject),
                    _ => throw new ArgumentException("Unknown sort value")
                },
                _ => throw new ArgumentException("Unknown sort method")
            };
        }
    }
}
