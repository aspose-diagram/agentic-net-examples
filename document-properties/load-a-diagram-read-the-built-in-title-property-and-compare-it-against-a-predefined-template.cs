using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <expectedTitle>");
            return;
        }

        string diagramPath = args[0];
        string expectedTitle = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Read the built‑in Title property from the document properties.
                string actualTitle = diagram.DocumentProps.Title ?? string.Empty;

                // Compare the actual title with the predefined template.
                if (actualTitle == expectedTitle)
                {
                    Console.WriteLine("Title matches the template.");
                }
                else
                {
                    Console.WriteLine($"Title mismatch. Expected: \"{expectedTitle}\", Actual: \"{actualTitle}\"");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}