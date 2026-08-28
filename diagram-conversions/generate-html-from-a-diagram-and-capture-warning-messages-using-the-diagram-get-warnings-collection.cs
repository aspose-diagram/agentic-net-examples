using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class WarningCollector : IWarningCallback
{
    // Stores warning descriptions captured during save.
    public List<string> Warnings { get; } = new List<string>();

    // Called by Aspose.Diagram for each warning.
    public void Warning(WarningInfo warning)
    {
        if (warning != null && !string.IsNullOrEmpty(warning.Description))
        {
            Warnings.Add(warning.Description);
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file.
            string inputPath = "input.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML export options and attach the warning collector.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            WarningCollector collector = new WarningCollector();
            htmlOptions.WarningCallback = collector;

            // Export to HTML.
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            // Output captured warnings.
            if (collector.Warnings.Count > 0)
            {
                Console.WriteLine("Warnings captured during HTML export:");
                foreach (string msg in collector.Warnings)
                {
                    Console.WriteLine("- " + msg);
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