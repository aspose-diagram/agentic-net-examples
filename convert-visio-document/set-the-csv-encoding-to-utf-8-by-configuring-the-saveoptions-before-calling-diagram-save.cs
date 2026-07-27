using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load an existing Visio diagram from a file.
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists.
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        Diagram diagram;
        try
        {
            // Attempt to load the diagram; catch any loading errors.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Define the output CSV file path.
        string outputPath = "output.csv";

        try
        {
            // Save the diagram as CSV using the appropriate SaveFileFormat.
            diagram.Save(outputPath, SaveFileFormat.Csv);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during the save operation.
            Console.Error.WriteLine($"Error saving CSV: {ex.Message}");
        }
    }
}