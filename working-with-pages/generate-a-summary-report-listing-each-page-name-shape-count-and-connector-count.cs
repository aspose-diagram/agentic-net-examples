using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be processed
            string diagramPath = "input.vsdx";

            // Load the diagram using the Aspose.Diagram constructor (lifecycle rule)
            Diagram diagram = new Diagram(diagramPath);

            // Prepare a StringBuilder to collect the report lines
            StringBuilder reportBuilder = new StringBuilder();

            reportBuilder.AppendLine("Page Summary Report");
            reportBuilder.AppendLine("-------------------");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Count shapes on the current page
                int shapeCount = page.Shapes.Count;

                // Count connectors (connections) on the current page
                int connectorCount = page.Connects.Count;

                // Append the information for this page to the report
                reportBuilder.AppendLine(
                    $"Page Name: {page.Name}, Shapes: {shapeCount}, Connectors: {connectorCount}");
            }

            // Output the report to the console
            Console.WriteLine(reportBuilder.ToString());

            // Optionally, save the report to a text file
            File.WriteAllText("PageSummaryReport.txt", reportBuilder.ToString());

            // Dispose the diagram object (lifecycle rule)
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
