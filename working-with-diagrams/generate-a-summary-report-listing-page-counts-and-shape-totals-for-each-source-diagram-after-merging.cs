using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to source diagrams
            string[] sourceFiles = { "source1.vsdx", "source2.vsdx", "source3.vsdx" };

            // Store report data for each source diagram
            var reportData = new List<(string FileName, int PageCount, int ShapeCount)>();

            // Master diagram that will hold the combined result
            Diagram masterDiagram = null;

            foreach (string filePath in sourceFiles)
            {
                // Load each diagram using the provided constructor (load rule)
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Count pages
                    int pageCount = diagram.Pages.Count;

                    // Count shapes across all pages
                    int shapeCount = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        shapeCount += page.Shapes.Count;
                    }

                    // Save counts for the summary report
                    reportData.Add((filePath, pageCount, shapeCount));

                    // Combine diagrams: first diagram becomes the master, others are merged into it
                    if (masterDiagram == null)
                    {
                        // Create a new master diagram from the first file (using load rule)
                        masterDiagram = new Diagram(filePath);
                    }
                    else
                    {
                        // Use the Combine method (combine rule) to merge the current diagram into the master
                        masterDiagram.Combine(diagram);
                    }
                }
            }

            // Output the summary report
            Console.WriteLine("Summary Report:");
            foreach (var entry in reportData)
            {
                Console.WriteLine($"File: {entry.FileName}");
                Console.WriteLine($"  Pages : {entry.PageCount}");
                Console.WriteLine($"  Shapes: {entry.ShapeCount}");
            }

            // Optionally save the merged diagram using the provided Save method (save rule)
            if (masterDiagram != null)
            {
                masterDiagram.Save("merged.vsdx", SaveFileFormat.Vsdx);
                masterDiagram.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
