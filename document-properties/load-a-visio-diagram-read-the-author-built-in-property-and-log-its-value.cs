using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (replace with your actual file path)
        string inputPath = "example.vsdx";

        // Guard to ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Read the built‑in Author property (mapped to Creator in Aspose.Diagram)
            string author = diagram.DocumentProps.Creator;

            // Log the Author value to the console
            Console.WriteLine($"Author: {author}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or property access
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}