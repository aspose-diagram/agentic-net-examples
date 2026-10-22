using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input diagram path and output report path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputReportPath>");
            return;
        }

        string inputPath = args[0];
        string reportPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list to collect report lines
                List<string> reportLines = new List<string>();
                reportLines.Add("Page Size Modification Report");
                reportLines.Add("----------------------------------------------------");

                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve original dimensions (in inches)
                    double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define new dimensions (e.g., increase size by 20%)
                    double newWidth = originalWidth * 1.2;
                    double newHeight = originalHeight * 1.2;

                    // Apply the new dimensions to the page
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // Add an entry to the report
                    reportLines.Add(
                        $"Page ID {page.ID}: Original (W={originalWidth:F2} in, H={originalHeight:F2} in) -> New (W={newWidth:F2} in, H={newHeight:F2} in)");
                }

                // Write the report to the specified file
                File.WriteAllLines(reportPath, reportLines);
                Console.WriteLine($"Report successfully written to: {reportPath}");

                // Save the modified diagram to a new file (optional but useful)
                string outputDiagramPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}