using System.Text;
using PasswordGenerator.CustomClasses;

namespace PasswordGenerator;

internal static class Program
{
    private static Task Main()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            HashSet<ConsoleColor> colors = new() { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkYellow, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan, ConsoleColor.Gray };

            var passwordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numbers = "0123456789";
            var symbols = "!@#$%^&*()_+";
        
            CustomRandom random = new(DateTime.Now.Millisecond * 2);
            CustomList<string> passwords = new();
        
            var restartProgram = true;

            while (restartProgram)
            {
                restartProgram = false;
                
                var enableColors = EnableRandomTextColors();
                if (enableColors)
                {
                    // Enable random text colors
                    Console.ForegroundColor = colors.ElementAt(random.Next(colors.Count));
                    Console.WriteLine("Enabled random text colors. This will change each time you run or restart this script.");
                }
                else
                {
                    // Disable random text colors
                    Console.ResetColor();
                    Console.WriteLine("Not enabling random text colors.");
                }


                var addNumbers = AddNumbersToPasswords();
                var addSymbols = AddSymbolsToPasswords();

                if (addNumbers)
                {
                    passwordCharacters += numbers;
                    Console.WriteLine("Added numbers.");
                }
                else
                {
                    Console.WriteLine("No numbers added.");
                }

                if (addSymbols)
                {
                    passwordCharacters += symbols;
                    Console.WriteLine("Added symbols.");
                }
                else
                {
                    Console.WriteLine("No symbols added.");
                }


                var length = GetPasswordLength();

                var startingCharacter = GetStartingCharacter();

                var numPasswords = GetNumPasswords();

            

                Console.ForegroundColor = colors.ElementAt(random.Next(colors.Count));

                GeneratePasswords(passwordCharacters, length, startingCharacter!, numPasswords, passwords);

                SavePasswordsToFile(passwords);


                restartProgram = RestartProgram(random, passwords, out var restart);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }

        return Task.CompletedTask;
    }

    private static bool RestartProgram(CustomRandom random, CustomList<string> passwords, out string? restart)
    {
        bool restartProgram;
        Console.WriteLine("\nScript ended. Enter 'y' to restart: ");
        restart = Console.ReadLine();
        if (restart == null || restart.ToLower() != "y")
        {
            restartProgram = false;
        }
        else
        {
            random.Seed = DateTime.Now.Millisecond * 2;
            passwords.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            restartProgram = true;
        }

        return restartProgram;
    }

    private static bool EnableRandomTextColors()
    {
        Console.WriteLine("\nEnter 'y' to enable random text colors, or 'n' to skip: ");
        var enableColors = Console.ReadLine();
        return enableColors?.ToLower() == "y";
    }


    private static bool AddNumbersToPasswords()
    {
        Console.WriteLine("\nEnter 'y' to add numbers to each password, or 'n' to skip: ");
        var addNumbers = Console.ReadLine();
        return addNumbers?.ToLower() == "y";
    }

    private static bool AddSymbolsToPasswords()
    {
        Console.WriteLine("\nEnter 'y' to add symbols to each password, or 'n' to skip: ");
        var addSymbols = Console.ReadLine();
        return addSymbols?.ToLower() == "y";
    }


    private static int GetPasswordLength()
    {
        Console.WriteLine("\nEnter password length: ");
        if (!int.TryParse(Console.ReadLine(), out var length) || length <= 0)
        {
            length = 16; // Default password length
        }
        return length;
    }


    private static string? GetStartingCharacter()
    {
        Console.WriteLine("\nWhat should the passwords always start with? (e.g., H = Hy90utr6): ");
        var startingCharacter = Console.ReadLine();
        return string.IsNullOrEmpty(startingCharacter) ? null : startingCharacter;
    }


    private static int GetNumPasswords()
    {
        Console.WriteLine("\nEnter the number of passwords to generate: ");
        if (!int.TryParse(Console.ReadLine(), out var numPasswords) || numPasswords <= 0)
        {
            numPasswords = 10; // Default number of passwords
        }
        return numPasswords;
    }


    private static void GeneratePasswords(string passwordCharacters, int length, string startingCharacter, int numPasswords, CustomList<string> passwords)
    {
        var characters = passwordCharacters;
        Parallel.For(0, numPasswords, _ =>
        {
            StringBuilder passwordBuilder = new();
            if (!string.IsNullOrEmpty(startingCharacter))
            {
                passwordBuilder.Append(startingCharacter);
            }

            for (var j = 0; j < length; j++)
            {
                var randomCharacter = characters.MinBy(_ => Guid.NewGuid());
                passwordBuilder.Append(randomCharacter);
            }

            var password = passwordBuilder.ToString();
            passwords.Add(password);
            Console.WriteLine(password);
        });
    }


    private static void SavePasswordsToFile(CustomList<string> passwords)
    {
        Console.WriteLine("\nEnter 'y' to save the passwords to a file, or 'n' to skip (Note that the passwords may not be the same): ");
        var saveToFile = Console.ReadLine();
        if (saveToFile?.ToLower() != "y")
        {
            Console.WriteLine("Skipping saving passwords to a file.");
            return;
        }

        Console.WriteLine("\nEnter a file name (without extension): ");
        var fileName = Console.ReadLine();

        var directoryPath = Path.Combine(Environment.CurrentDirectory, "Passwords");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, $"{fileName}.txt");
        using StreamWriter file = new(filePath);

        file.WriteLine("--------------GENERATED PASSWORDS--------------" + Environment.NewLine); // delete this line if you don't want the header
        file.WriteLine(string.Join(Environment.NewLine, passwords));
        Console.WriteLine("Passwords saved to file.");
    }

}