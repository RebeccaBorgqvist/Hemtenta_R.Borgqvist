namespace dtp15_todolist
{    public class Todo
    {        public static List<TodoItem> list = new List<TodoItem>();

        public const int Active = 1;
        public const int Waiting = 2;
        public const int Completed = 3;


        public static string StatusToString(int status)
        {
            switch (status)
            {
                case Active: return "active";
                case Waiting: return "waiting";
                case Completed: return "completed";
                default: return "(error)";
            }
        }        public static void ChangeStatus(string name, int num)        {            foreach (TodoItem item in list)            {                if (item.task == name)                {                    item.status = num;                    return;                }            }        }


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

        // 16.
        public static void NewTask()        {            // 16.1.            string name, description, priority, total;            Console.WriteLine("What's the task: ");            name = Console.ReadLine();            Console.WriteLine("Priority: ");            priority = Console.ReadLine();            Console.WriteLine("Description: ");            description = Console.ReadLine();            total = "1|" + priority + '|' + name + '|' + description;            Todo.list.Add(new TodoItem(total));        }        public static void TaskActive()        {            Console.Write("Which task do you want to change to active? ");            string task = Console.ReadLine();            Todo.ChangeStatus(task, 1);        }        public static void TaskWaiting()        {            Console.Write("Which task do you want to change to waiting? ");            string task = Console.ReadLine();            Todo.ChangeStatus(task, 2);        }        public static void TaskComplete()        {            Console.Write("Which task do you want to change to completed? ");            string task = Console.ReadLine();            Todo.ChangeStatus(task, 3);        }        // 15.        public static void SaveToFile()        {            string todoFileName = "todo.lis";            Console.WriteLine($"Saving changes to file {todoFileName} ...");            using (StreamWriter sw = new StreamWriter("todo.lis"))            foreach (TodoItem item in Todo.list)            {                sw.WriteLine($"{item.status}|{item.priority}|{item.task}|{item.taskDescription}");            }            Thread.Sleep(1000);            Console.WriteLine("The list is now saved ");            Thread.Sleep(500);        }        // 14.        //public static void WriteListToFile(string task)        //{        //    using (StreamWriter sw = File.AppendText("todo.lis"))        //    {        //        sw.WriteLine(task);        //    }        //}


        public static void ReadListFromFile()
        {
            string todoFileName = "todo.lis";
            Console.Write($"Reading from file {todoFileName} ... ");
            Thread.Sleep(1000);
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
        }        // 13.         public static void PrintTodoList(bool verbose = true, bool active = false, bool waiting = false, bool completed = false)
        {
            PrintHead(verbose);

            foreach (TodoItem item in list)
            {
                if (active)                {                    if (item.status == Active) item.Print(verbose);                }                else if (waiting)                {                    if (item.status == Waiting) item.Print(verbose);                }                else if (completed)                {                    if (item.status == Completed) item.Print(verbose);                }                    
                else                {                    item.Print(verbose);                }                    
            }

            PrintFoot(verbose);
        }


        // 12. & 1.
        public static void PrintHelpAndCommandList()
        {
            //Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\nCommands \n");
            Console.WriteLine("New ------------------ Make a new task");                          // Delvis klar, behöver säkras från ev. buggar
            Console.WriteLine("Save ----------------- Save changes");                             // Färdig
            Console.WriteLine("Load ----------------- Load todo.lis");                            // Färdig            Console.WriteLine("List ----------------- Lists the active tasks in to-do-list");
            Console.WriteLine("List all ------------- Lists the whole to-do-list");               // Färdig
            Console.WriteLine("Describe ------------- Show the description of active tasks");     // Färdig
            Console.WriteLine("Describe all --------- Show the description of all tasks");        // Färdig
            Console.WriteLine("Active /task/ -------- Change status on task to Active");          // ej färdig funktion
            Console.WriteLine("Completed /task/ ----- Change status on task to Done");            // ej färdig funktion
            Console.WriteLine("Waiting /task/ ------- Change status on task to Waiting");         // ej färdig funktion
            Console.WriteLine("Clear ---------------- Clear console");
            Console.WriteLine("Help ----------------- Shows this list");
            Console.WriteLine("Quit ----------------- Save list and end program");                // Färdig
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
    }    class MainClass    {        public static void Main(string[] args)        {            Console.Title = "To do list";            //Console.ForegroundColor = ConsoleColor.DarkMagenta;            Console.WriteLine("\n\nHello there!");            Console.WriteLine("Let's see what you have on the todo list today");            Todo.PrintHelpAndCommandList();            string command;            do            {                // 1. & 12.                command = MyIO.ReadCommand("> ");                if (MyIO.Equals(command, "help"))                {                    Todo.PrintHelpAndCommandList();                }                // 2.                else if (MyIO.Equals(command, "quit"))                {                                       Console.WriteLine("\nGoodbye!");                    Todo.SaveToFile();                    break;                }                // 3.                else if (MyIO.Equals(command, "list"))                {                    // 3.1.                    if (MyIO.HasArgument(command, "all"))                        Todo.PrintTodoList(active: false, verbose: false);                    else if (MyIO.HasArgument(command, "waiting"))                        Todo.PrintTodoList(waiting: true, verbose: false);                    else if (MyIO.HasArgument(command, "completed"))                        Todo.PrintTodoList(completed: true, verbose: false);                    else                        Todo.PrintTodoList(active: true, verbose: false);                }                // 4.                else if (MyIO.Equals(command, "new"))                {                    Todo.NewTask();                }                // 5.                else if (MyIO.Equals(command, "describe"))                {                    if (MyIO.HasArgument(command, "all"))                        Todo.PrintTodoList(verbose: true, active: false);                    else                        Todo.PrintTodoList(verbose: true, active: true);                }                // 6.                else if (MyIO.Equals(command, "save"))                {
                    Todo.SaveToFile();                }

                // 7.
                else if (MyIO.Equals(command, "load"))                {                    Todo.ReadListFromFile();                }                // 8.                else if (MyIO.Equals(command, "active"))                {
                    Todo.TaskActive();                }

                // 9.
                else if (MyIO.Equals(command, "waiting"))                {
                    Todo.TaskWaiting();                }

                // 10.
                else if (MyIO.Equals(command, "completed"))                {
                    Todo.TaskComplete();                }                // 11.                else if(MyIO.Equals(command, "clear"))                {                    Console.Clear();                    Todo.PrintHelpAndCommandList();                }                else                {                    Console.ForegroundColor = ConsoleColor.DarkRed;                    Console.WriteLine($"Unkown command: {command}");                    Console.ResetColor();                }            }            while (true);
        }
    }
}
