using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Simple credential verification (replace with real authentication as needed)
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = ReadPassword();

        // Hardcoded credentials for demonstration
        const string validUser = "admin";
        const string validPass = "password";

        if (username != validUser || password != validPass)
        {
            Console.Error.WriteLine("Invalid credentials. Operation aborted.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Check if the diagram contains a VBA project
            if (diagram.VbaProject != null)
            {
                // Output VBA project information
                Console.WriteLine($"VBA Project Name: {diagram.VbaProject.Name}");
                Console.WriteLine($"VBA Project IsSigned: {diagram.VbaProject.IsSigned}");

                // Remove VBA project data (clears macros and any protection)
                // This is the closest available operation to removing password protection.
                diagram.VbProjectData = null;

                Console.WriteLine("VBA project data has been removed.");
            }
            else
            {
                Console.WriteLine("No VBA project found in the diagram.");
            }

            // Save the modified diagram (use a macro-enabled format if you intend to keep macros,
            // but since we removed them, a standard VSDX is sufficient)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to read password without echoing characters
    private static string ReadPassword()
    {
        string password = string.Empty;
        ConsoleKeyInfo info;
        do
        {
            info = Console.ReadKey(intercept: true);
            if (info.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(info.KeyChar))
            {
                password += info.KeyChar;
                Console.Write("*");
            }
        } while (info.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }
}