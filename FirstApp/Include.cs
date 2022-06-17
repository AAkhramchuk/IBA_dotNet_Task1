using System.Globalization;
using System.Text.RegularExpressions;

namespace FirstApp
{
    class Include
    {
        public const string OutputText = "Следующий день рождения через:";
        public const string ParseFormat = @"(\w*)";
        public string[] TryParseFormats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy"
                                        , "d/MM/yyyy", "dd/MM/yy", "dd/M/yy"
                                        , "d/M/yy", "d/MM/yy" };

        public string UserName = "";    // User name
        public string UserSurname = ""; // User surname
        public DateTime UserBorn;       // Date when the user was born
        public DateTime UserBirth;      // Date when the user will have a birthday
        public int UserAge;             // User age

        public int CursorPosX; // Cursor position left

        // Data processing part
        public void DataProc()
        {
            string Input = "";
            Regex Regex;
            MatchCollection Matches;
            string InputDate = "";
            DateTime Today = DateTime.Now;

            do
            {
                Console.Write("Введите имя, фамилию, год рождения через разделитель: ");
                // Input
                Input = "";
                for (ConsoleKeyInfo Input1 = Console.ReadKey()
                     ; Input.Length == 0
                       || !(Input1.Key == ConsoleKey.Enter || Input.Length > 40)
                     ; Input1 = Console.ReadKey())
                { if (Input1.KeyChar != 0) Input = Input + Input1.KeyChar; }

                string str = String.Concat(str.Where(char.IsLetterOrDigit));

                Regex = new Regex(ParseFormat);
                Matches = Regex.Matches(Input);
                if (Matches.Count > 7)
                {
                    UserName = Matches[0].Value; //Name
                    UserSurname = Matches[2].Value; //Surname
                    //Date of birth (DOB) before parse
                    InputDate = Matches[4].Value + '/' + Matches[6].Value + '/' + Matches[8].Value;
                }
                if (Matches.Count < 8
                    || !DateTime.TryParseExact(InputDate
                                               , TryParseFormats
                                               , CultureInfo.InvariantCulture
                                               , DateTimeStyles.None
                                               , out UserBorn)) //DOB after the parse
                {
                    Console.WriteLine("Не удалось определить имя, фамилию и год рождения.");
                }
                else break;
            } while (true);

            UserBirth = new DateTime(Today.Year
                                     , UserBorn.Month
                                     , UserBorn.Day);
            UserAge = Today.Year - UserBorn.Year;
            if (UserBirth > Today)
            {
                UserAge--;
            }
        }

        //Output
        public void Output()
        {
            Console.WriteLine($"Имя: {UserName}");
            Console.WriteLine($"Фамилия: {UserSurname}");
            Console.WriteLine("Родился: " + UserBorn.ToString("dddd dd MMMM yyyy"
                                                              , new CultureInfo("en-EN")));
            Console.WriteLine($"Количество полных лет: {UserAge}");
            Console.WriteLine(OutputText);
            CursorPosX = OutputText.Length;
        }
    }
}
