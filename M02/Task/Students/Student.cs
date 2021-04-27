using System.Text;

namespace Students
{
    class Student
    {
        private string _fullName;
        private string _email;

        public Student(string email)
        {
            _email = email;
            _fullName = email.Substring(0, email.IndexOf('@'));

            string[] words = _fullName.Split('.');
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
                sb.Append(char.ToUpper(word[0]) + word.Substring(1, word.Length - 1) + " ");

            _fullName = sb.ToString().Remove(sb.Length - 1);
        }

        public Student(string name, string surname)
        {
            _fullName = name + " " + surname;
            _email = (name + "." + surname + "@epam.com").ToLower();
        }

        public override string ToString() => _fullName;

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            return this.ToString() == obj.ToString();
        }

        public override int GetHashCode() => this.ToString().GetHashCode();
    }
}
