using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the input diagram path
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputReportPath]");
            return;
        }

        string inputPath = args[0];
        string reportPath = args.Length >= 2 ? args[1] : null;

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // StringBuilder to accumulate the report
        StringBuilder reportBuilder = new StringBuilder();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page with explicit type
                foreach (Page page in diagram.Pages)
                {
                    // Capture original dimensions (in inches)
                    double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define a scaling factor (e.g., increase size by 20%)
                    double scaleFactor = 1.2;

                    // Compute new dimensions
                    double newWidth = originalWidth * scaleFactor;
                    double newHeight = originalHeight * scaleFactor;

                    // Apply new dimensions to the page
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;

                    // Append information for this page to the report
                    reportBuilder.AppendLine($"Page ID: {page.ID}, Name: {page.Name}");
                    reportBuilder.AppendLine($"  Original Size: {originalWidth:F2} in x {originalHeight:F2} in");
                    reportBuilder.AppendLine($"  New Size: {newWidth:F2} in x {newHeight:F2} in");
                }
            }

            // Output the report to the console
            Console.WriteLine("Page Size Modification Report:");
            Console.WriteLine(reportBuilder.ToString());

            // If an output path is provided, write the report to a file
            if (!string.IsNullOrEmpty(reportPath))
            {
                // Ensure the directory exists
                string directory = Path.GetDirectoryName(reportPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(reportPath, reportBuilder.ToString());
                Console.WriteLine($"Report saved to: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to standard error
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}