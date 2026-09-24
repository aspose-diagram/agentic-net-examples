using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file (VSX, VSDX, etc.)
                string diagramPath = @"C:\Diagrams\SampleDiagram.vsdx";

                // Load the diagram using Aspose.Diagram
                Diagram diagram = new Diagram(diagramPath);

                // Prepare a StringWriter to build the summary report
                using (StringWriter reportWriter = new StringWriter())
                {
                    reportWriter.WriteLine("Shape Count Summary Report");
                    reportWriter.WriteLine("==========================");
                    reportWriter.WriteLine();

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Count the number of shapes on the current page
                        int shapeCount = page.Shapes.Count;

                        // Write the page name and shape count to the report
                        reportWriter.WriteLine($"Page: {page.Name}");
                        reportWriter.WriteLine($"  Shape Count: {shapeCount}");
                        reportWriter.WriteLine();
                    }

                    // Output the report to the console
                    Console.WriteLine(reportWriter.ToString());

                    // Optionally, save the report to a text file
                    string reportPath = @"C:\Diagrams\ShapeCountReport.txt";
                    File.WriteAllText(reportPath, reportWriter.ToString());

                    Console.WriteLine($"Report saved to: {reportPath}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }