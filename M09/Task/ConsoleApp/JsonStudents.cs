using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp
{
    public class JsonStudents : IReader<Student>
    {
        public List<Student> Students { get; set; }
        public string Path { get; set; }

        public JsonStudents(string path)
        {
            Path = path;
        }

        public List<Student> Read()
        {
            string jsonString;
            using (var sr = new StreamReader(Path))
                jsonString = sr.ReadToEnd();

            var studentsObj = JsonSerializer.Deserialize<JsonStudents>(jsonString);
            Students = studentsObj.Students ?? throw new ArgumentNullException();

            return Students;
        }
    }
}
