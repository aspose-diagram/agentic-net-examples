using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file and output HTML file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.html";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a warning collector implementing IWarningCallback
            WarningCollector warningCollector = new WarningCollector();

            // Configure HTML save options and assign the warning callback
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.WarningCallback = warningCollector;

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);

            // Output any captured warnings
            if (warningCollector.Warnings.Count > 0)
            {
                Console.WriteLine("Warnings captured during HTML export:");
                foreach (string warning in warningCollector.Warnings)
                {
                    Console.WriteLine(warning);
                }
            }
            else
            {
                Console.WriteLine("No warnings were generated during HTML export.");
            }

            // Dispose the diagram to free resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Implementation of IWarningCallback to collect warning messages
    private class WarningCollector : IWarningCallback
    {
        public List<string> Warnings { get; } = new List<string>();

        // This method is called by Aspose.Diagram for each warning
        public void Warning(WarningInfo warning)
        {
            string description = warning?.Description ?? "Unknown warning";
            Warnings.Add(description);
        }
    }
}