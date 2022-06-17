using SysTimers = System.Timers;

namespace FirstApp
{
    internal class CountDown
    {
        private static readonly CountDown instance = new CountDown();
        public SysTimers.Timer Timer { get; private set; }
        private CountDown()
        {
            Timer = new(interval: 1000); // Timer to countdown seconds left
            Timer.Elapsed += (sender, e) => Processing();
        }
        public static CountDown GetInstance() => instance;

        public int CursorPosX = 0; // Cursor position left
        public int CursorPosY = 0; // Cursor position top

        public DateTime UserBirth; // Date when the user will have a birthday
        public int UserAge;        // User age

        //Countdown processing
        public void Processing()
        {
            const string Format = "' 'ddd' days 'hh' hours 'mm' minutes 'ss' seconds'";

            CountDown Config = CountDown.GetInstance();

            DateTime Today = DateTime.Now;
            TimeSpan TimeLeft;

            if (UserBirth.Date < Today.Date)
            {
                UserBirth = UserBirth.AddYears(1);
            }
            if (UserBirth.Date == Today.Date)
            {
                TimeLeft = TimeSpan.Zero;
            }
            else
            {
                TimeLeft = UserBirth.Subtract(Today);
            }
            Console.SetCursorPosition(Config.CursorPosX, Config.CursorPosY);
            Console.WriteLine(TimeLeft.ToString(Format));

            if (UserBirth.Date == Today.Date)
            {
                if (Config.Timer != null)
                {
                    Config.Timer.Stop();
                }
                Console.WriteLine("Поздравляем вам исполнилось " + ++UserAge + "          ");
            }
            Console.Write("Для выхода нажмите клавишу Escape");
        }
    }
}
