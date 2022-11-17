namespace dtp15_todolist
{    // TODO : G Nivå    //    // Lägg till:    //     NY                    - skapa en ny uppgift    //     BESKRIV               - lista alla Active uppgifter, status, prio, namn och >Beskrivning<    //     SPARA                 - spara uppgifterna    //     LADDA                 - ladda listan todo.lis    //     AKTIVERA /uppgift/    - sätt status till Active    //     KLAR /uppgift/        - sätt status på uppgift till Ready    //     VÄNTA /uppgift/       - sätt status på uppgift till Waiting    //    // Ändra:    //     LISTA                 - lista alla Active uppgifter, status, prio och namn på uppgiften    //     LISTA ALLT            - lista alla uppgifter oavsett status    //     SLUTA                 - spara senast laddade filen och avsluta programmet    // TODO : VG Nivå    //    // Lägg till:    //      ny /uppgift/         - skapa en ny uppgift med namnet /uppgift/    //      redigera /uppgift/   - redigera en ppgift med namnet /uppgift/    //      kopiera /uppgift/    - redigera en uppgift med namnet /uppgift/    //                             till namnet /uppgift 2/, kopian har samma prio men vara aktiv    //      beskriv allt         - lista alla uppgifter oavsett status    //      lista väntande       - lista alla väntande uppgifter    //      lista klara          - lista alla klara uppgifter    //      spara /fil/          - spara uppgifterna på filen /fil/    //      ladda /fil/          - ladda filen fil    //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            public class Todo
    {        public static List<TodoItem> list = new List<TodoItem>();

        public const int Active = 1;
        public const int Waiting = 2;
        public const int Ready = 3;


        public static string StatusToString(int status)
        {
            switch (status)
            {
                case Active: return "aktiv";
                case Waiting: return "väntande";
                case Ready: return "avklarad";
                default: return "(felaktig)";
            }
        }


        public class TodoItem
        {
            public int status;
            public int priority;
            public string task;
            public string taskDescription;


            public TodoItem(int priority, string task)
            {
                this.status = Active;
                this.priority = priority;
                this.task = task;
                this.taskDescription = "";
            }


            public TodoItem(string todoLine)
            {
                string[] field = todoLine.Split('|');
                status = Int32.Parse(field[0]);
                priority = Int32.Parse(field[1]);
                task = field[2];
                taskDescription = field[3];
            }


            public void Print(bool verbose = false)
            {
                string statusString = StatusToString(status);
                Console.Write($"|{statusString,-12}|{priority,-6}|{task,-20}|");
                if (verbose)
                    Console.WriteLine($"{taskDescription,-40}|");
                else
                    Console.WriteLine();
            }
        }


        // TODO - gör en funktion för att göra en ny uppgift
        public static void NewTask()        {            string name, description, priority, total;            Console.WriteLine("Vad ska göras: ");            name = Console.ReadLine();            Console.WriteLine("Prioritet: ");            priority = Console.ReadLine();            Console.WriteLine("Beskrivning: ");            description = Console.ReadLine();            total = "1|" + priority + '|' + name + '|' + description;            WriteListToFile(total);            ReadListFromFile();        }        // TODO - gör en StreamWriter för att spara nya tasks på todo.lis        public static void WriteListToFile(string task)        {            using (StreamWriter sw = File.AppendText("todo.lis"))            {                sw.WriteLine(task);            }
        }


        public static void ReadListFromFile()
        {
            string todoFileName = "todo.lis";
            Console.Write($"Läser från fil {todoFileName} ... ");
            StreamReader sr = new StreamReader(todoFileName);
            int numRead = 0;
            list.Clear();

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                TodoItem item = new TodoItem(line);
                list.Add(item);
                numRead++;
            }
            sr.Close();
            Console.WriteLine($"Läste {numRead} rader.");
        }


        private static void PrintHeadOrFoot(bool head, bool verbose)
        {
            if (head)
            {
                Console.Write("|status      |prio  |namn                |");
                if (verbose) Console.WriteLine("beskrivning                             |");
                else Console.WriteLine();
            }
            Console.Write("|------------|------|--------------------|");
            if (verbose) Console.WriteLine("----------------------------------------|");
            else Console.WriteLine();
        }


        private static void PrintHead(bool verbose)
        {
            PrintHeadOrFoot(head: true, verbose);
        }


        private static void PrintFoot(bool verbose)
        {
            PrintHeadOrFoot(head: false, verbose);
        }


        // TODO - skapa en funktion som listar endast Active
        public static void PrintTodoList(bool verbose = false)
        {
            PrintHead(verbose);
            foreach (TodoItem item in list)
            {
                item.Print(verbose);
            }
            PrintFoot(verbose);
        }

        // TODO - lägg till: ny, lista allt, beskriv
        public static void PrintHelp()
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("hjälp                    lista denna hjälp");
            Console.WriteLine("lista                    lista att-göra-listan");
            Console.WriteLine("lista allt               lista även väntande och avslutade uppgifter");  // ej färdig funktion, visar även beskrivning just nu
            Console.WriteLine("beskriv                  visar beskrivningen av varje uppgift");         // ej färdig funktion
            Console.WriteLine("ny                       lägg till en ny uppgift");                      // Delvis klar, behöver säkras från ev. buggar
            Console.WriteLine("spara                    spara ändringar");                              // ej färdig funktion
            Console.WriteLine("ladda                    ladda listan todo.lis");                        // ej färdig funktion
            Console.WriteLine("aktiv /uppgift/          sätt status på uppgift till aktiv");            // ej färdig funktion
            Console.WriteLine("klar /uppgift/           sätt status på uppgift till klar");             // ej färdig funktion
            Console.WriteLine("vänta /uppgift/          sätt status på uppgift till väntande");         // ej färdig funktion
            Console.WriteLine("sluta                    spara att-göra-listan och sluta");              // ej färdig funktion, sparar inte än
        }
    }    class MyIO
    {
        static public string ReadCommand(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }


        static public bool Equals(string rawCommand, string expected)
        {
            string command = rawCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cwords = command.Split(' ');
                if (cwords[0] == expected) return true;
            }
            return false;
        }


        // TODO - lägg till funktion som listar endast Active
        // listar allt oavsett status
        static public bool HasArgument(string rawCommand, string expected)        {
            string command = rawCommand.Trim();
            if (command == "") return false;
            else
            {
                string[] cwords = command.Split(' ');
                if (cwords.Length < 2) return false;
                if (cwords[1] == expected) return true;
            }
            return false;
        }
    }    class MainClass    {        public static void Main(string[] args)        {            Console.Title = "Att göra lista";            Console.ForegroundColor = ConsoleColor.DarkMagenta;            Console.WriteLine("Välkommen till att-göra-listan!");            Todo.ReadListFromFile();            Todo.PrintHelp();            string command;            do            {                command = MyIO.ReadCommand("> ");                if (MyIO.Equals(command, "hjälp"))                {                    Todo.PrintHelp();                }                // TODO - Lägg till sparfunktion                else if (MyIO.Equals(command, "sluta"))                {                    Console.WriteLine("Hej då!");                    break;                }                // TODO - Listar allt just nu, ändra så endast aktiva listas                // för VG - Lista alla väntande och/eller klara                else if (MyIO.Equals(command, "lista"))                {                    // TODO - Här listas allt plus beskrivningen, ändra till beskriv                    if (MyIO.HasArgument(command, "allt"))                        Todo.PrintTodoList(verbose: true);                    else                        Todo.PrintTodoList(verbose: false);                }                // TODO - Lägg till ny task                // för VG - ny /uppgift/, /redigera/, /kopiera/                else if (MyIO.Equals(command, "ny"))                {                    Todo.NewTask();                }                // TODO - Lägg till beskrivning, endast aktiva                // för VG - beskriv ALLT                else if (MyIO.Equals(command, "beskriv"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till spara                // för VG - spara /fil/                else if (MyIO.Equals(command, "spara"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till ladda                // för VG - ladda /fil/                else if (MyIO.Equals(command, "ladda"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till aktiv                else if (MyIO.Equals(command, "aktiv"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till klar                else if (MyIO.Equals(command, "klar"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till vänta                else if (MyIO.Equals(command, "vänta"))                {                    Console.WriteLine("inte skapad än");                }                else                {                    Console.WriteLine($"Okänt kommando: {command}");                }            }            while (true);        }
    }
}
