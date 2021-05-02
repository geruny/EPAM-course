using System;
using System.Text;

namespace Students
{
    internal class Student
    {
        private string _fullName;
        private string _email;

        public Student(string email)
        {
            _email = email ?? throw new ArgumentException();
            _fullName = GetFullName(email);
        }

        public Student(string name, string surname)
        {
            if (name == null | surname == null)
                throw new ArgumentException();

            _fullName = name + " " + surname;
            _email = (name + "." + surname + "@epam.com").ToLower();
        }

        public override string ToString() => _fullName;

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj.GetType() != this.GetType())
                    return false;

                return this.ToString().Equals(obj.ToString());
            }
            else
                return false;
        }

        public override int GetHashCode() => this.ToString().GetHashCode();

        private static string GetFullName(string email)
        {
            string[] words = email.Substring(0, email.IndexOf('@')).Split('.');
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
                sb.Append(char.ToUpper(word[0]) + word.Substring(1, word.Length - 1) + " ");

            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}
