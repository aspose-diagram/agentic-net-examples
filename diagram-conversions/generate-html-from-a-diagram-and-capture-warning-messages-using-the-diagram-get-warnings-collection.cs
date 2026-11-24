using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output HTML file path
        string outputPath = "output.html";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a warning collector implementing IWarningCallback
                WarningCollector collector = new WarningCollector();

                // Configure HTML save options and assign the warning callback
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.WarningCallback = collector;

                // Save the diagram to HTML using the configured options
                diagram.Save(outputPath, htmlOptions);

                // Output captured warnings, if any
                if (collector.Warnings.Count > 0)
                {
                    Console.WriteLine("Warnings captured during save:");
                    foreach (string warning in collector.Warnings)
                    {
                        Console.WriteLine(warning);
                    }
                }
                else
                {
                    Console.WriteLine("No warnings captured during save.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Implementation of IWarningCallback to collect warnings
class WarningCollector : IWarningCallback
{
    public List<string> Warnings { get; } = new List<string>();

    // This method is called by Aspose.Diagram for each warning
    public void Warning(WarningInfo warning)
    {
        if (warning != null)
        {
            // Capture the warning description
            Warnings.Add(warning.Description);
        }
    }
}