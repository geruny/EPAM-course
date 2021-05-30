namespace ObserverApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CountDown.Notify += Observer1.PrintMessage;
            CountDown.Notify += Observer2.PrintMessage;
            CountDown.CountDownMethod();
        }
    }
}
