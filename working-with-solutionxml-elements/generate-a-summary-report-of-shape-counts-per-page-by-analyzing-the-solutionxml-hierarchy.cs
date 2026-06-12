using System;
using System.IO;
using Aspose.Diagram;

class ShapeCountReport
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file
            string inputPath = "input.vsdx";

            // Load the diagram using Aspose.Diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a StringWriter to build the report
            StringWriter reportWriter = new StringWriter();

            reportWriter.WriteLine("Shape Count Summary per Page");
            reportWriter.WriteLine(new string('=', 30));

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Count the shapes on the current page
                int shapeCount = page.Shapes.Count;

                // Write the page information and shape count to the report
                reportWriter.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, Shapes: {shapeCount}");
            }

            // Output the report to the console
            Console.WriteLine(reportWriter.ToString());

            // Optionally, save the report to a text file
            string outputPath = "ShapeCountReport.txt";
            File.WriteAllText(outputPath, reportWriter.ToString());

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
