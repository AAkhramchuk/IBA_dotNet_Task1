using SysTimers = System.Timers;

namespace FirstApp
{
    internal class CountDown
    {
        private static readonly CountDown Instance = new ();
        public SysTimers.Timer Timer { get; private set; }
        private CountDown()
        {
            Timer = new(interval: 1000); // Timer to countdown seconds left
            Timer.Elapsed += (sender, e) => Processing();
        }
        public static CountDown GetInstance() => Instance;

        public List<Include> Users = new ();

        //Countdown processing
        private void Processing()
        {
            const string Format = "' 'ddd' days 'hh' hours 'mm' minutes 'ss' seconds'";

            int OldPosX, OldPosY;

            //CountDown Config = CountDown.GetInstance();

            DateTime Today = DateTime.Now;
            TimeSpan TimeLeft;

            foreach(Include User in Users)
            {
                // Time calculation for countdown line
                if (User.Birth.Date < Today.Date)
                {
                    User.Birth = User.Birth.AddYears(1);
                }
                if (User.Birth.Date == Today.Date)
                {
                    TimeLeft = TimeSpan.Zero;
                }
                else
                {
                    TimeLeft = User.Birth.Subtract(Today);
                }

                // Set position for countdown line
                (OldPosX, OldPosY) = Console.GetCursorPosition();
                Console.SetCursorPosition(Include.OutputText.Length, User.CursorPosY);
                Console.WriteLine(TimeLeft.ToString(Format));
                Console.SetCursorPosition(OldPosX, OldPosY);

                // If birthday begins and Timer is active
                if (User.ActiveTimer && User.Birth.Date == Today.Date)
                {
                    User.ActiveTimer = false;
                    Console.WriteLine($"{User.Name} {User.Surname} Поздравляем вам исполнилось {++User.Age}");
                }
            }
            // Stop Timer
            if (Instance.Timer != null
                && Users.All(User => !User.ActiveTimer))
            {
                Instance.Timer.Stop();
            }
        }
    }
}