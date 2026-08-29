using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class ShapeCountReport
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file using the provided constructor (load rule)
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a StringBuilder to collect the summary lines
                StringBuilder reportBuilder = new StringBuilder();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Count the shapes on the current page
                    int shapeCount = page.Shapes.Count;

                    // Append a formatted line to the report
                    reportBuilder.AppendLine($"Page \"{page.Name}\" (ID: {page.ID}) contains {shapeCount} shape(s).");
                }

                // Write the summary report to a text file
                string outputPath = "ShapeCountSummary.txt";
                File.WriteAllText(outputPath, reportBuilder.ToString());

                // Optionally, display the report path
                Console.WriteLine($"Shape count summary saved to: {outputPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
