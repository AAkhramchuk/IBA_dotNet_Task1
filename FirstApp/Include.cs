using System.Globalization;
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
        public bool ActiveTimer = false; // Is Timer active or not?
        public ConsoleKeyInfo InputKey;

        private string InputLine;

        private readonly string[] TryParseFormats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy"
                                                    , "dd/MM/yy"  , "dd/M/yy"  , "d/M/yy"  , "d/MM/yy"
                                                    , "dd/MM/y"   , "dd/M/y"   , "d/M/y"   , "d/MM/y" };
        private readonly string ParseFormat = @"(\w*)";
        public const string OutputText = "Следующий день рождения через:";

        public Include()
        {
            do
            {
                InputLine = myReadLine(); // Input string line
                if (InputKey.Key == ConsoleKey.Escape)
                    break; // Quit if Escape button was pressed
            }
            while (IsLineParsing()); // Line parsing
        }

        private string myReadLine()
        {
            List<char> Input = new ();
            InputLine = "";
            int i = 0;

            //Input process
            Console.WriteLine("Введите имя, фамилию и год рождения через разделитель (Escape для выхода):");
            while (i == 0 // expect for the first symbol, ignore Enter button
                   || !(InputKey.Key == ConsoleKey.Enter // Determing Enter to stop input
                        || i > 39)) // not more then 40 symbol
            {
                InputKey = Console.ReadKey();
                // Close application
                if (InputKey.Key == ConsoleKey.Escape)
                    break;
                // Imitate behavior after pressing "BackSpace" button
                else if (Input.Count > 0 && InputKey.Key == ConsoleKey.Backspace)
                {
                    Input.RemoveAt(--i);
                    Console.Write(" ");
                    --Console.CursorLeft;
                }
                // Store characters, ignore control keys
                else if (InputKey.KeyChar != 0 && !char.IsControl(InputKey.KeyChar))
                {
                    Input.Add(InputKey.KeyChar);
                    i++;
                }
            }
            Console.WriteLine();

            // Create input line
            foreach (char Symbol in Input)
            {
                InputLine += Symbol;
            }
            return InputLine;
        }

        private bool IsLineParsing()
        {
            Name = "";
            Surname = "";
            string IDay = "";
            string IMonth = "";
            string IYear = "";
            bool Result = false;

            // Parse input line
            Regex Regex = new Regex(ParseFormat);
            MatchCollection Matchs = Regex.Matches(InputLine);
            foreach (Match IWord in Matchs)
            {
                if (IWord.Value == "") continue;
                else if (Name == "") Name = IWord.Value;
                else if (Surname == "") Surname = IWord.Value;
                else if (IDay == "") IDay = IWord.Value;
                else if (IMonth == "") IMonth = IWord.Value;
                else if (IYear == "")
                {
                    IYear = IWord.Value;
                    break;
                }
            }
            // Determine correct input or not
            if (!DateTime.TryParseExact(IDay + "/" + IMonth + "/" + IYear
                                        , TryParseFormats
                                        , CultureInfo.InvariantCulture
                                        , DateTimeStyles.None
                                        , out Born)
                || Name == ""
                || Surname == "")
            {
                Console.WriteLine("Не удалось определить имя, фамилию и год рождения.");
                Result = true;
            }

            return Result;
        }

        // New output lines for User data
        public override string ToString()
        {
            return String.Join(Environment.NewLine
                               , $"Имя: {Name}"
                               , $"Фамилия: {Surname}"
                               , $"Родился: {Born.ToString("dddd dd MMMM yyyy", new CultureInfo("en"))}"
                               , $"Количество полных лет: {Age}"
                               , $"{OutputText}");
        }
    }
}