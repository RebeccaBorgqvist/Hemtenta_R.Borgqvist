﻿namespace dtp15_todolist
{    public class Todo
    {        public static List<TodoItem> list = new List<TodoItem>();

        public const int Active = 1;
        public const int Waiting = 2;
        public const int Ready = 3;


        public static string StatusToString(int status)
        {
            switch (status)
            {
                case Active: return "active";
                case Waiting: return "waiting";
                case Ready: return "done";
                default: return "(error)";
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
        }        // TODO - gör en funktion som sparar till fil        //      - sparar just nu redan inskrivna tasks..        public static void saveToFile()        {            string todoFileName = "todo.lis";            Console.WriteLine($"Saving changes to file {todoFileName} ...");            using (StreamWriter sw = File.AppendText("todo.lis"))            foreach (TodoItem item in Todo.list)            {                sw.WriteLine($"{item.status}|{item.priority}|{item.task}|{item.taskDescription}");            }            Console.WriteLine("The list is now saved ");        }


        // TODO - gör en funktion för att göra en ny uppgift
        public static void NewTask()        {            string name, description, priority, total;            Console.WriteLine("What's the task: ");            name = Console.ReadLine();            Console.WriteLine("Priority: ");            priority = Console.ReadLine();            Console.WriteLine("Description: ");            description = Console.ReadLine();            total = "1|" + priority + '|' + name + '|' + description;            WriteListToFile(total);            ReadListFromFile();        }        // TODO - gör en StreamWriter för att spara nya tasks på todo.lis        public static void WriteListToFile(string task)        {            using (StreamWriter sw = File.AppendText("todo.lis"))            {                sw.WriteLine(task);            }
        }


        public static void ReadListFromFile()
        {
            string todoFileName = "todo.lis";
            Console.Write($"Reading from file {todoFileName} ... ");
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
            Console.WriteLine($"Read {numRead} lines.");
        }


        private static void PrintHeadOrFoot(bool head, bool verbose)
        {
            if (head)
            {
                Console.Write("|status      |prio  |name                |");
                if (verbose) Console.WriteLine("description                             |");
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
        }        // TODO - skapa en funktion som listar endast Active        public static void PrintTodoList(bool verbose = true, bool active = false)
        {
            PrintHead(verbose);

            foreach (TodoItem item in list)
            {
                if (active)                {                    if (item.status == Active) item.Print(verbose);                }                    
                else                {                    item.Print(verbose);                }                    
            }

            PrintFoot(verbose);
        }


        // TODO - lägg till: ny, lista allt, beskriv osv.
        public static void PrintHelpAndCommandList()
        {
            Console.WriteLine("\nCommands:");
            Console.WriteLine("New                      Make a new task");                          // Delvis klar, behöver säkras från ev. buggar
            Console.WriteLine("Save                     Save changes");                             // Ska vara klar
            Console.WriteLine("Load                     Load todo.lis");                            // Delvis klar, sparar dubbletter?            Console.WriteLine("List                     Lists the active tasks in to-do-list");
            Console.WriteLine("List all                 Lists the whole to-do-list");               // Ska vara klar
            Console.WriteLine("Description              Show the description of active tasks");     // Ska vara klar
            Console.WriteLine("Describe all             Show the description of all tasks");        // Ska vara klar
            Console.WriteLine("Active /task/            Change status on task to Active");          // ej färdig funktion
            Console.WriteLine("Done /task/              Change status on task to Done");            // ej färdig funktion
            Console.WriteLine("Waiting /task/           Change status on task to Waiting");         // ej färdig funktion
            Console.WriteLine("Help                     Shows this list");
            Console.WriteLine("Quit                     Save list and end program");                // ej färdig funktion, sparar inte än
        }
    }    class MyIO
    {
        // TODO - tar input, lägg till ToLower så input inte krånglar
        static public string ReadCommand(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().ToLower();
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
    }    class MainClass    {        public static void Main(string[] args)        {            Console.Title = "To do list";            Console.ForegroundColor = ConsoleColor.DarkMagenta;            Console.WriteLine("\nWelcome to the ToDo list!");            //Todo.ReadListFromFile();  //onödigt att visa vid start?            Todo.PrintHelpAndCommandList();            string command;            do            {                command = MyIO.ReadCommand("> ");                if (MyIO.Equals(command, "help"))                {                    Todo.PrintHelpAndCommandList();                }                // TODO - Lägg till sparfunktion                else if (MyIO.Equals(command, "quit"))                {                    Console.WriteLine("Goodbye!");                    break;                }                // TODO - Listar allt just nu, ändra så endast aktiva listas, UTAN BESKRIVNING                // för VG - Lista alla väntande och/eller klara,              UTAN BESKRIVNING                else if (MyIO.Equals(command, "list"))                {                    // TODO - Här listas allt plus beskrivningen, ändra till beskriv                    if (MyIO.HasArgument(command, "all"))                        Todo.PrintTodoList(active: false, verbose: false);                    else                        Todo.PrintTodoList(active: true, verbose: false);                }                // TODO - Lägg till ny task                // för VG - ny /uppgift/, /redigera/, /kopiera/                else if (MyIO.Equals(command, "new"))                {                    Todo.NewTask();                }                // TODO - Lägg till beskrivning, endast aktiva                // för VG - beskriv ALLT                else if (MyIO.Equals(command, "describe"))                {                    if (MyIO.HasArgument(command, "all"))                        Todo.PrintTodoList(verbose: true, active: false);                    else                        Todo.PrintTodoList(verbose: true, active: true);                }                // TODO - Lägg till spara                // för VG - spara /fil/                else if (MyIO.Equals(command, "save"))                {
                    Todo.saveToFile();                }

                // TODO - Lägg till ladda
                // för VG - ladda /fil/
                else if (MyIO.Equals(command, "load"))                {                    Todo.ReadListFromFile();                }                // TODO - Lägg till aktiv                else if (MyIO.Equals(command, "active"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till klar                else if (MyIO.Equals(command, "done"))                {                    Console.WriteLine("inte skapad än");                }                // TODO - Lägg till vänta                else if (MyIO.Equals(command, "waiting"))                {                    Console.WriteLine("inte skapad än");                }                else                {                    Console.WriteLine($"Unkown command: {command}");                }            }            while (true);
        }
    }
}
