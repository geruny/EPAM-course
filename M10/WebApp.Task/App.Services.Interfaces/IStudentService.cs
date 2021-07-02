namespace App.Services.Interfaces
{
    public interface IStudentService
    {
        public void CheckStudentTurnout(int studentId);
        public void CheckStudentAverageScore(int studentId);
    }
}