namespace App.Domain.core.Models
{
    public class Homework:BaseModel
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Mark { get; set; }
    }
}
