namespace ConsoleApp
{
    internal class Student
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public Test Test { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName} {Test.Subject} {Test.Mark} {Test.DateTime.ToShortDateString()}";
        }
    }
}
