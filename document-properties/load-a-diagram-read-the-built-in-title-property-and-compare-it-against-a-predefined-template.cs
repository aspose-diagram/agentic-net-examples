using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Define the path to the Visio diagram file.
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Define the expected title template to compare against.
        string expectedTitle = args.Length > 1 ? args[1] : "My Diagram Title";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Read the built‑in Title property from the document properties.
            string actualTitle = diagram.DocumentProps.Title ?? string.Empty;

            // Compare the actual title with the expected template.
            if (actualTitle == expectedTitle)
            {
                Console.WriteLine("Title matches the template.");
            }
            else
            {
                Console.WriteLine($"Title does not match the template.\nExpected: \"{expectedTitle}\"\nActual:   \"{actualTitle}\"");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}