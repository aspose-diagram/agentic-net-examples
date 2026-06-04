using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements warning callback to collect warnings during save operation
    public class WarningCollector : IWarningCallback
    {
        // Stores warning descriptions
        public List<string> Warnings { get; } = new List<string>();

        // Called by Aspose.Diagram for each warning
        public void Warning(WarningInfo warning)
        {
            // Add warning description to the list
            Warnings.Add(warning.Description);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output HTML file path
                string outputPath = "output.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare HTML save options and assign warning callback
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.DefaultFont = "Arial";

                WarningCollector warningCollector = new WarningCollector();
                htmlOptions.WarningCallback = warningCollector;

                // Save diagram as HTML
                diagram.Save(outputPath, htmlOptions);

                // Output captured warnings
                if (warningCollector.Warnings.Count > 0)
                {
                    Console.WriteLine("Warnings captured during HTML export:");
                    foreach (string warning in warningCollector.Warnings)
                    {
                        Console.WriteLine("- " + warning);
                    }
                }
                else
                {
                    Console.WriteLine("No warnings were generated during HTML export.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}