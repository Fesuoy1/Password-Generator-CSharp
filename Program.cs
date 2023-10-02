using System.Text;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            HashSet<ConsoleColor> colors = new() { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan, ConsoleColor.Gray };

            string password_characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            string symbols = "!@#$%^&*()_+";

            bool restartProgram = true;

            while (restartProgram)
            {
                Random random = new(DateTime.Now.Millisecond);
                ConsoleColor color = colors.ElementAt(random.Next(colors.Count));
                ConsoleColor color2 = colors.ElementAt(random.Next(colors.Count));

                restartProgram = false;
                // Prompt user to enable random text colors
                Console.WriteLine("\nEnter 'y' to enable random text colors, or 'n' to skip: ");
                string? enable_colors = Console.ReadLine();
                if (enable_colors != null && enable_colors.ToLower() == "y")
                {

                    Console.ForegroundColor = color;
                    Console.WriteLine("Enabled random text colors. This will change each time you run or restart this script.");
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine("Not enabling random text colors.");
                }

                // Prompt user to add numbers to each password
                Console.WriteLine("\nEnter 'y' to add numbers to each password, or 'n' to skip: ");
                string? add_numbers = Console.ReadLine();
                if (add_numbers != null && add_numbers.ToLower() == "y")
                {
                    password_characters += numbers;
                    Console.WriteLine("Added numbers.");
                }
                else
                {
                    Console.WriteLine("No numbers added.");
                }

                // Prompt user to add symbols to each password
                Console.WriteLine("\nEnter 'y' to add symbols to each password, or 'n' to skip: ");
                string? add_symbols = Console.ReadLine();
                if (add_symbols != null && add_symbols.ToLower() == "y")
                {
                    password_characters += symbols;
                    Console.WriteLine("Added symbols.");
                }
                else
                {
                    Console.WriteLine("No symbols added.");
                }

                int length;
                Console.WriteLine("\nEnter password length: ");
                if (!int.TryParse(Console.ReadLine(), out length))
                {
                    length = 16;
                }

                Console.WriteLine("\nWhat should the passwords always start with? (e.g., H = Hy90utr6): ");
                string? starting_character = Console.ReadLine();

                int num_passwords;
                Console.WriteLine("\nEnter the number of passwords to generate: ");
                if (!int.TryParse(Console.ReadLine(), out num_passwords))
                {
                    num_passwords = 10;
                }

                List<string> passwords = new();

                Console.ForegroundColor = color2;

                await Task.Run(() =>
                    {
                        Parallel.For(0, num_passwords, i =>
                        {
                            StringBuilder passwordBuilder = new();
                            passwordBuilder.Append(starting_character);

                            for (int j = 0; j < length; j++)
                            {
                                char randomCharacter = password_characters.OrderBy(x => Guid.NewGuid()).First();
                                passwordBuilder.Append(randomCharacter);
                            }

                            string password = passwordBuilder.ToString();
                            passwords.Add(password);
                            Console.WriteLine(password);
                        });
                    });

                // Prompt user to save passwords to a file
                Console.WriteLine("\nEnter 'y' to save the passwords to a file, or 'n' to skip (Note that the passwords may not be the same): ");
                string? save_to_file = Console.ReadLine();
                if (save_to_file != null && save_to_file.ToLower() == "y")
                {
                    Console.WriteLine("\nEnter a file name (without extension): ");
                    string? file_name = Console.ReadLine();

                    string directoryPath = Path.Combine(Environment.CurrentDirectory, "Passwords");
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);

                    string filePath = Path.Combine(directoryPath, $"{file_name}.txt");
                    using StreamWriter file = new(filePath);

                    file.WriteLine("--------------GENERATED PASSWORDS--------------" + Environment.NewLine); // delete this line if you don't want the header
                    file.WriteLine(string.Join(Environment.NewLine, passwords));
                    Console.WriteLine("Passwords saved to file.");
                }

                Console.WriteLine("\nScript ended. Enter 'y' to restart: ");
                string? restart = Console.ReadLine();
                if (restart != null && restart.ToLower() == "y")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    restartProgram = true;
                }
            }
        }
    }
