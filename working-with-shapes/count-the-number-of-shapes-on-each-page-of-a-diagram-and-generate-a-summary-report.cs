using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class ShapeCountReport
{
    static void Main(string[] args)
    {
        try
        {

            // Input diagram file (replace with actual path)
            string inputPath = @"C:\Diagrams\sample.vsdx";

            // Output report file (replace with desired path)
            string outputPath = @"C:\Diagrams\ShapeCountReport.txt";

            // Load the diagram using Aspose.Diagram (load rule)
            Diagram diagram = new Diagram(inputPath);

            // List to hold each page's summary line
            List<string> reportLines = new List<string>();
            reportLines.Add("Shape Count Summary Report");
            reportLines.Add("==========================");
            reportLines.Add(string.Empty);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Count the shapes on the current page
                int shapeCount = page.Shapes.Count;

                // Build a summary line for this page
                string line = $"Page {page.ID} (Name: {page.Name}): {shapeCount} shape{(shapeCount == 1 ? "" : "s")}";
                reportLines.Add(line);
            }

            // Write the summary report to a text file (free‑form code, no specific rule needed)
            File.WriteAllLines(outputPath, reportLines);

            // Optionally, display a confirmation message
            Console.WriteLine($"Shape count report generated at: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
