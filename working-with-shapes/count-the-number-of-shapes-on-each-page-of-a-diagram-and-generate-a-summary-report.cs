using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (VSD, VDX, VSDX, etc.)
                string diagramPath = "inputDiagram.vsdx";

                // Load the diagram using the Aspose.Diagram constructor that accepts a file path.
                // This follows the required lifecycle rule for loading a document.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Prepare a StringWriter to collect the summary report.
                    using (StringWriter reportWriter = new StringWriter())
                    {
                        // Iterate through each page in the diagram.
                        foreach (Page page in diagram.Pages)
                        {
                            // Count the shapes on the current page.
                            int shapeCount = page.Shapes.Count;

                            // Write a line for this page into the report.
                            reportWriter.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) contains {shapeCount} shape(s).");
                        }

                        // Get the complete report text.
                        string report = reportWriter.ToString();

                        // Output the report to the console.
                        Console.WriteLine("=== Diagram Shape Count Summary ===");
                        Console.WriteLine(report);

                        // Optionally, save the report to a text file.
                        // This uses standard .NET I/O and does not interfere with Aspose.Diagram's save lifecycle.
                        string reportPath = "ShapeCountReport.txt";
                        File.WriteAllText(reportPath, report);
                        Console.WriteLine($"Report saved to: {Path.GetFullPath(reportPath)}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }