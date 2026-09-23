using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        // Prompt for user credentials
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        // Simple credential verification (replace with real logic as needed)
        if (username != "admin" || password != "secret")
        {
            Console.WriteLine("Invalid credentials. Operation aborted.");
            return;
        }

        // Paths to the input and output Visio files
        string inputPath = "protected.vsdm";   // Visio file with VBA password protection
        string outputPath = "unprotected.vsdm"; // Destination file without VBA protection

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Remove all VBA project data (clears password protection)
            diagram.VbProjectData = null;

            // Save the diagram in a macro‑enabled format
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

            Console.WriteLine($"VBA password protection removed. File saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
