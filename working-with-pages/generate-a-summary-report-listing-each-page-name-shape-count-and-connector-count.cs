using System.IO;
using System;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Prepare a StringBuilder to collect the summary report
            StringBuilder report = new StringBuilder();
            report.AppendLine("Page Name\tShape Count\tConnector Count");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Page name (fallback to empty string if null)
                string pageName = page.Name ?? string.Empty;

                // Count of shapes on the page
                int shapeCount = page.Shapes.Count;

                // Count of connectors on the page (Connects collection holds connections)
                int connectorCount = page.Connects.Count;

                // Append the information for the current page
                report.AppendLine($"{pageName}\t{shapeCount}\t{connectorCount}");
            }

            // Output the report to the console
            Console.WriteLine(report.ToString());

            // Optionally, save the report to a text file
            // System.IO.File.WriteAllText("SummaryReport.txt", report.ToString());

            // Dispose the diagram object
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
