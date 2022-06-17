class Program
{
    static void Main(string[] argsCdown
    {
        ConsoleKeyInfo KeyInfo;

        FirstApp.CountDown Cdown = FirstApp.CountDown.GetInstance();

        do
        {

            if (Cdown.Timer == null)
            {
                Console.WriteLine("Не удалось организовать работу таймера.");
                return;
            }

            FirstApp.Include Incl = new FirstApp.Include();

            Incl.DataProc();

            Incl.Output();
            (Cdown.CursorPosX, Cdown.CursorPosY) = Console.GetCursorPosition();
            Cdown.CursorPosY--;

            Cdown.UserBirth  = Incl.UserBirth;
            Cdown.UserAge    = Incl.UserAge;
            Cdown.CursorPosX = Incl.CursorPosX;

            Cdown.Timer.Start();

            //Keypress processing
            do
            {
                KeyInfo = Console.ReadKey();
            }
            while (KeyInfo.Key != ConsoleKey.Escape
                   && Cdown.Timer.Enabled);
        } while (KeyInfo.Key != ConsoleKey.Escape);

        if (Cdown.Timer != null)
        {
            Cdown.Timer.Dispose();
        }
    }
}
