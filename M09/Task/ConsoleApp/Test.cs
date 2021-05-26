using System;

namespace ConsoleApp
{
    internal class Test
    {
        public string Subject { get; set; }
        public int Mark { get; set; }
        public DateTime DateTime;
        public int[] Date
        {
            get => new int[] { };
            set => DateTime = new DateTime(value[0], value[1], value[2]);
        }
    }
}
