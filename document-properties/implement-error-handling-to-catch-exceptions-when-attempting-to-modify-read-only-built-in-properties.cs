using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx"; // replace with actual file path

        // Guard: ensure the input file exists before loading
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load an existing Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Attempt to modify a read‑only built‑in property: Version
        try
        {
            // diagram.Version is read‑only; this will throw an exception
            diagram.Version = "1.0.0";
        }
        catch (Exception ex)
        {
            // Log the error to standard error stream
            Console.Error.WriteLine("Error modifying read‑only property 'Version': " + ex.Message);
        }

        // Attempt to modify another read‑only built‑in property: BuildNumberCreated
        try
        {
            // DocumentProps.BuildNumberCreated is read‑only and expects a string
            diagram.DocumentProps.BuildNumberCreated = "12345";
        }
        catch (Exception ex)
        {
            // Log the error to standard error stream
            Console.Error.WriteLine("Error modifying read‑only property 'BuildNumberCreated': " + ex.Message);
        }

        // Define output file path
        string outputPath = "output.vsdx"; // replace with desired output path

        // Save the diagram (unchanged) to a new file with proper error handling
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved to " + outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error saving diagram: " + ex.Message);
        }
    }
}