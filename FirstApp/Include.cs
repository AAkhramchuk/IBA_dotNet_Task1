using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FirstApp
{
    class Include
    {
        public string Name = "";    // User name
        public string Surname = ""; // User surname
        public DateTime Born;       // Date when the user was born
        public DateTime Birth;      // Date when the user will have a birthday
        public int Age;             // User age
        public int CursorPosX = 0;  // Cursor position left
        public int CursorPosY = 0;  // Cursor position top
        public bool ActiveTimer = false; // Is Countdown process active or not?

        public ConsoleKeyInfo InputKey;
        private string InputLine;
        public const string OutputText = "Следующий день рождения через:";

        public Include()
        {
            do
            {
                InputLine = MyReadLine(); // String line input
                if (InputKey.Key == ConsoleKey.Escape)
                    break; // Quit if Escape button was pressed
            }
            while (!HasLineParsed()); // Line parsing
        }

        // String line input
        private string MyReadLine()
        {
            int index = 0;
            StringBuilder input = new(40);

            //Input process
            Console.WriteLine("Введите имя, фамилию и год рождения через разделитель (Escape для выхода):");
            while (index == 0 // expect for the first symbol, ignore Enter button
                   || !(InputKey.Key == ConsoleKey.Enter // Determing Enter to stop input
                        || index > 39)) // not more then 40 symbols
            {
                InputKey = Console.ReadKey();
                // Close application
                if (InputKey.Key == ConsoleKey.Escape)
                    break;
                // Imitate behavior after pressing "BackSpace" button
                else if (index > 0 && InputKey.Key == ConsoleKey.Backspace)
                {
                    input.Remove(--index, 1);
                    Console.Write(" ");
                    Console.CursorLeft--;
                }
                // Store characters, ignore control keys
                else if (InputKey.KeyChar != 0 && !char.IsControl(InputKey.KeyChar))
                {
                    input.Append(InputKey.KeyChar);
                    index++;
                }
            }
            Console.CursorTop++;
            
            return input.ToString();
        }

        // Parse input line
        private bool HasLineParsed()
        {
            string parseFormat = @"(\w*)";
            string[] tryParseFormats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy"
                                       , "dd/MM/yy"  , "dd/M/yy"  , "d/M/yy"  , "d/MM/yy"
                                       , "dd/MM/y"   , "dd/M/y"   , "d/M/y"   , "d/MM/y" };

            bool result = true;

            Regex regex = new Regex(parseFormat);
            MatchCollection matchs = regex.Matches(InputLine);
            // Determine correct input or not
            if (matchs.Count < 9
                || matchs[0].Value == ""
                || matchs[2].Value == ""
                || !DateTime.TryParseExact(matchs[4].Value 
                                           + "/" + matchs[6].Value
                                           + "/" + matchs[8].Value
                                           , tryParseFormats
                                           , CultureInfo.InvariantCulture
                                           , DateTimeStyles.None
                                           , out Born))
            {
                Console.WriteLine("Не удалось определить имя, фамилию и дату рождения.");
                result = false;
            }
            else
            {
                Name = matchs[0].Value;
                Surname = matchs[2].Value;
            }

            return result;
        }

        // Calculate time left until Birthday
        public TimeSpan TimeLeft()
        {
            DateTime today = DateTime.Now;
            TimeSpan result = TimeSpan.Zero;

            if (Birth.Date < today.Date)
            {
                Birth = Birth.AddYears(1);
            }
            if (Birth.Date != today.Date)
            {
                result = Birth.Subtract(today);
            }

            return result;
        }

        // New output lines for User data
        public override string ToString()
        {
            return string.Join(Environment.NewLine
                               , $"Имя: {Name}"
                               , $"Фамилия: {Surname}"
                               , $"Родился: {Born.ToString("dddd dd MMMM yyyy", new CultureInfo("en"))}"
                               , $"Количество полных лет: {Age}"
                               , $"{OutputText}");
        }
    }
}