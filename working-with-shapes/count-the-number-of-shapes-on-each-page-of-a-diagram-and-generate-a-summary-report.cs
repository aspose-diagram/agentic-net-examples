using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Output summary report file path
        string reportPath = "summary.txt";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare to write the summary report
                using (StreamWriter writer = new StreamWriter(reportPath, false))
                {
                    // Write header
                    writer.WriteLine("Shape Count Summary Report");
                    writer.WriteLine($"Diagram: {Path.GetFileName(inputPath)}");
                    writer.WriteLine($"Generated on: {DateTime.Now}");
                    writer.WriteLine(new string('-', 40));

                    // Console header
                    Console.WriteLine("Shape Count Summary Report");
                    Console.WriteLine($"Diagram: {Path.GetFileName(inputPath)}");
                    Console.WriteLine($"Generated on: {DateTime.Now}");
                    Console.WriteLine(new string('-', 40));

                    // Manual page index because Page does not expose an Index property
                    int pageIndex = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        pageIndex++;

                        // Count shapes on the current page
                        int shapeCount = page.Shapes.Count;

                        // Prepare the line for both console and file
                        string line = $"Page {pageIndex}: {shapeCount} shape(s)";

                        // Output to console
                        Console.WriteLine(line);

                        // Write to report file
                        writer.WriteLine(line);
                    }

                    // Footer
                    writer.WriteLine(new string('-', 40));
                    writer.WriteLine("End of Report");
                }

                Console.WriteLine($"Summary report saved to: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}