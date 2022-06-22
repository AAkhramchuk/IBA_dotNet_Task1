using SysTimer = System.Timers.Timer;

namespace FirstApp
{
    class CountDown
    {
        public SysTimer Timer { get; }
        public List<Include> Users { get; }
        public CountDown()
        {
            Timer = new(interval: 1000); // Timer to countdown seconds left
            Timer.Elapsed += (sender, e) => Processing();
            
            Users = new();
        }

        //Countdown processing
        private void Processing()
        {
            const string format = "' 'ddd' days 'hh' hours 'mm' minutes 'ss' seconds'";

            int oldPosX, oldPosY;

            foreach(Include user in Users)
            {
                // Set position for countdown line
                (oldPosX, oldPosY) = Console.GetCursorPosition();
                Console.SetCursorPosition(Include.OutputText.Length, user.CursorPosY);
                // Output of the time left until Birthday
                Console.WriteLine(user.TimeLeft().ToString(format));
                Console.SetCursorPosition(oldPosX, oldPosY);

                // If Timer is active and Birthday comes
                if (user.ActiveTimer && user.TimeLeft() == TimeSpan.Zero)
                {
                    user.ActiveTimer = false;
                    Console.WriteLine($"{user.Name} {user.Surname} Поздравляем вам исполнилось {++user.Age}");
                }
            }
            // Stop Timer
            if (Timer != null
                && Users.All(User => !User.ActiveTimer))
            {
                Timer.Stop();
            }
        }
    }
}