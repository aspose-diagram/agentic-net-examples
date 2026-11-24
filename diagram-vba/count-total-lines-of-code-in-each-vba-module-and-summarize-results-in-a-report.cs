using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Check whether the diagram contains a VBA project.
            if (diagram.VbaProject == null)
            {
                Console.WriteLine("The diagram does not contain a VBA project.");
                return;
            }

            // Prepare counters for the report.
            long totalLines = 0;
            int moduleIndex = 0;

            // Iterate through each VBA module in the project.
            foreach (VbaModule module in diagram.VbaProject.Modules)
            {
                moduleIndex++;

                // Retrieve the VBA source code.
                string code = module.Codes ?? string.Empty;

                // Count lines – split on both CRLF and LF.
                string[] lines = code.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                long lineCount = lines.Length;

                totalLines += lineCount;

                // Output per‑module statistics.
                Console.WriteLine($"Module {moduleIndex}: \"{module.Name}\" – {lineCount} line(s)");
            }

            // Summarize the overall result.
            Console.WriteLine($"Total lines of VBA code across all modules: {totalLines}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}