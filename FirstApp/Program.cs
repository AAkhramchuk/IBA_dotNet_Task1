class Program
{
    public static void Main()
    {
        DateTime Today = DateTime.Now;
        FirstApp.Include LastUser;

        FirstApp.CountDown Countdown = FirstApp.CountDown.GetInstance();
        if (Countdown.Timer == null)
        {
            Console.WriteLine("Не удалось организовать работу таймера.");
            return;
        }

        try
        {
            do
            {
                if (Countdown.Timer == null)
                {
                    Console.WriteLine("Не удалось организовать работу таймера.");
                    return;
                }

                // Add User data
                Countdown.Users.Add(new FirstApp.Include());
                LastUser = Countdown.Users.Last();

                // Finalize execution by pressed Escape button
                if (LastUser.InputKey.Key == ConsoleKey.Escape)
                    break;

                // User's Birth date calculation 
                LastUser.Birth = new DateTime(Today.Year
                                     , LastUser.Born.Month
                                     , LastUser.Born.Day);
                LastUser.Age = Today.Year - LastUser.Born.Year;

                // User's age calculation
                if (LastUser.Birth > Today)
                {
                    LastUser.Age--;
                }

                // Output lines
                Console.WriteLine(LastUser);

                // Save cursor position for the countdown line
                (LastUser.CursorPosX, LastUser.CursorPosY) = Console.GetCursorPosition();
                LastUser.CursorPosY--;
                
                //Timer start
                Countdown.Timer.Start();
                LastUser.ActiveTimer = true;

            } while (Countdown.Timer.Enabled);
        }
        finally
        {
            if (Countdown.Timer != null)
            {
                Countdown.Timer.Dispose();
            }
        }
    }
}