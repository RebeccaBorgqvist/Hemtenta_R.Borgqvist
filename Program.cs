namespace dtp15_todolist
{
    public class Todo
    {        // TODO        //        // Lägg till:        //     NY                    - skapa en ny uppgift        //     BESKRIV               - lista alla Active uppgifter, status, prio, namn och >Beskrivning<        //     SPARA                 - spara uppgifterna        //     LADDA                 - ladda listan todo.lis        //     AKTIVERA /uppgift/    - sätt status till Active        //     KLAR /uppgift/        - sätt status på uppgift till Ready        //     VÄNTA /uppgift/       - sätt status på uppgift till Waiting        //        // Ändra:        //     LISTA                 - lista alla Active uppgifter, status, prio och namn på uppgiften        //     LISTA ALLT            - lista alla uppgifter oavsett status        //     SLUTA                 - spara senast laddade filen och avsluta programmet

        public static List<TodoItem> list = new List<TodoItem>();

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


        public static void ReadListFromFile()
        {
            string todoFileName = "todo.lis";
            Console.Write($"Läser från fil {todoFileName} ... ");
            StreamReader sr = new StreamReader(todoFileName);
            int numRead = 0;

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

        // TODO - lägg till: ny, lista allt
        public static void PrintHelp()
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("hjälp            lista denna hjälp");
            Console.WriteLine("lista            lista att-göra-listan");
            Console.WriteLine("lista allt       lista även väntande och avslutade uppgifter");  // done
            Console.WriteLine("ny               lägg till en ny uppgift");  // done
            Console.WriteLine("sluta            spara att-göra-listan och sluta");
        }
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till att-göra-listan!");
            Todo.ReadListFromFile();
            Todo.PrintHelp();
            string command;
            do
            {
                command = MyIO.ReadCommand("> ");

                if (MyIO.Equals(command, "hjälp"))
                {
                    Todo.PrintHelp();
                }

                else if (MyIO.Equals(command, "sluta"))
                {
                    Console.WriteLine("Hej då!");
                    break;
                }

                // TODO - lägg till lista
                else if(MyIO.Equals(command, "lista"))                {                    // TBD                }

                // TODO - gör om till lista allt
                else if (MyIO.Equals(command, "lista allt"))
                {
                    if (MyIO.HasArgument(command, "allt"))
                        Todo.PrintTodoList(verbose: true);
                    else
                        Todo.PrintTodoList(verbose: false);
                }

                // TODO - lägg till NY
                else if(MyIO.Equals(command, "ny uppgift"))                {                    // TBD                }

                else
                {
                    Console.WriteLine($"Okänt kommando: {command}");
                }
            }
            while (true);
        }
    }


    class MyIO
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
        static public bool HasArgument(string rawCommand, string expected) 
        {
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
    }
}
