using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve and display the automatically embedded API version information.
            Console.WriteLine($"Diagram API Version: {diagram.Version}");

            // Additional document properties that may be useful for audit purposes.
            Console.WriteLine($"Build Number Created: {diagram.DocumentProps.BuildNumberCreated}");
            Console.WriteLine($"Build Number Edited: {diagram.DocumentProps.BuildNumberEdited}");
            Console.WriteLine($"Time Created: {diagram.DocumentProps.TimeCreated}");
            Console.WriteLine($"Time Edited: {diagram.DocumentProps.TimeEdited}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or property access.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}