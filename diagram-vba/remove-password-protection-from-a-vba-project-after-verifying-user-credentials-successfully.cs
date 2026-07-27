using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Simple console‑based credential verification
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        if (!ValidateCredentials(username, password))
        {
            Console.WriteLine("Invalid credentials. Operation aborted.");
            return;
        }

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Remove VBA project data (clears password protection)
        diagram.VbProjectData = null;

        // Save the diagram in a macro‑enabled format to preserve the (now unprotected) VBA structure
        diagram.Save(outputPath, SaveFileFormat.Vsdm);

        Console.WriteLine("Password protection removed and file saved successfully.");
    }

    // Replace this with real authentication logic as needed
    static bool ValidateCredentials(string user, string pass)
    {
        // Example hard‑coded check
        return user == "admin" && pass == "secret";
    }
}
