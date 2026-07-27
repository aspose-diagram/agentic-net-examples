using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements the warning callback to collect warnings during save operation
    public class WarningCollector : IWarningCallback
    {
        // Stores warning messages
        public List<string> Messages { get; } = new List<string>();

        // Called by Aspose.Diagram when a warning occurs
        public void Warning(WarningInfo warning)
        {
            // Capture the description of the warning
            Messages.Add(warning.Description);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output HTML file path
                string outputPath = "output.html";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Create HTML save options
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                    // Set a default font to avoid missing font issues
                    htmlOptions.DefaultFont = "Arial";

                    // Assign the warning collector
                    WarningCollector collector = new WarningCollector();
                    htmlOptions.WarningCallback = collector;

                    // Save the diagram as HTML
                    diagram.Save(outputPath, htmlOptions);

                    // Output captured warnings
                    if (collector.Messages.Count > 0)
                    {
                        Console.WriteLine("Warnings captured during HTML export:");
                        foreach (string msg in collector.Messages)
                        {
                            Console.WriteLine("- " + msg);
                        }
                    }
                    else
                    {
                        Console.WriteLine("HTML export completed without warnings.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}