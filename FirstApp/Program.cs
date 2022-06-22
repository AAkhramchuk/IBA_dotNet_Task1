class Program
{
    public static void Main()
    {
        DateTime today = DateTime.Now;
        FirstApp.Include lastUser;

        //FirstApp.CountDown Countdown = FirstApp.CountDown.GetInstance();
        FirstApp.CountDown Countdown = new ();
        if (Countdown.Timer == null)
        {
            Console.WriteLine("Не удалось организовать работу таймера.");
            return;
        }

        try
        {
            do
            {
                // Add User data
                Countdown.Users.Add(new FirstApp.Include());
                lastUser = Countdown.Users.Last();

                // Finalize execution by pressed Escape button
                if (lastUser.InputKey.Key == ConsoleKey.Escape)
                    break;

                // User's Birth date 
                lastUser.Birth = new DateTime(today.Year
                                     , lastUser.Born.Month
                                     , lastUser.Born.Day);

                // User's age calculation
                lastUser.Age = today.Year - lastUser.Born.Year;
                if (lastUser.Birth > today)
                {
                    lastUser.Age--;
                }

                // Output lines
                Console.WriteLine(lastUser);

                // Save cursor position for the countdown line
                (lastUser.CursorPosX, lastUser.CursorPosY) = Console.GetCursorPosition();
                lastUser.CursorPosY--;
                
                //Timer start
                Countdown.Timer.Start();
                lastUser.ActiveTimer = true;

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