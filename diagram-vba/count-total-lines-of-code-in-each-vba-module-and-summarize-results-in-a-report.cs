using System.IO;
using System;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaLineCounter
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Prepare a report builder
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("VBA Module Line Count Report");
            reportBuilder.AppendLine("----------------------------");

            // Iterate through each VBA module in the project
            foreach (VbaModule module in vbaProject.Modules)
            {
                // Retrieve the VBA code as a single string
                string code = module.Codes ?? string.Empty;

                // Count lines by splitting on newline characters
                // Handles both Windows (\r\n) and Unix (\n) line endings
                int lineCount = 0;
                if (code.Length > 0)
                {
                    // Normalize line endings to '\n' then split
                    string normalized = code.Replace("\r\n", "\n").Replace("\r", "\n");
                    lineCount = normalized.Split('\n').Length;
                }

                // Append module information to the report
                reportBuilder.AppendLine($"Module Name: {module.Name}");
                reportBuilder.AppendLine($"Module Type: {module.Type}");
                reportBuilder.AppendLine($"Total Lines: {lineCount}");
                reportBuilder.AppendLine();
            }

            // Output the report to the console
            Console.WriteLine(reportBuilder.ToString());

            // Optionally, write the report to a text file
            // System.IO.File.WriteAllText("VbaLineCountReport.txt", reportBuilder.ToString());

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
