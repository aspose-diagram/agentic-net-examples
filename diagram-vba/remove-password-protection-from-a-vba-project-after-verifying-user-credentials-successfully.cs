using System.IO;
using System;
using Aspose.Diagram;

class RemoveVbaPassword
{
    // Simple user credential validation (replace with real authentication as needed)
    private static bool ValidateUser(string userName, string password)
    {
        // Example: hard‑coded credentials for demonstration
        const string validUser = "admin";
        const string validPass = "secret";

        return string.Equals(userName, validUser, StringComparison.OrdinalIgnoreCase) &&
               password == validPass;
    }

    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = @"C:\Diagrams\ProtectedDiagram.vsdx";
            string outputPath = @"C:\Diagrams\UnprotectedDiagram.vsdx";

            // Load the diagram (lifecycle rule: load)
            Diagram diagram = new Diagram(inputPath);

            // Prompt for user credentials
            Console.Write("Enter user name: ");
            string userName = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = ReadPassword();

            // Verify credentials before proceeding
            if (!ValidateUser(userName, password))
            {
                Console.WriteLine("Invalid credentials. Operation aborted.");
                return;
            }

            // Remove VBA macro (which also removes any VBA project password protection)
            diagram.RemoveMacro();

            // Optionally clear the raw VBA project data
            diagram.VbProjectData = null;

            // Save the modified diagram (lifecycle rule: save)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Password protection removed and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
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
