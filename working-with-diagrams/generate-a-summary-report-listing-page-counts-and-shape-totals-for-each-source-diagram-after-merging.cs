using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to source diagram files
            string[] sourceFiles = { "diagram1.vsdx", "diagram2.vsdx", "diagram3.vsdx" };

            // Load diagrams and collect statistics
            List<Diagram> diagrams = new List<Diagram>();
            List<string> reportLines = new List<string>();

            foreach (string file in sourceFiles)
            {
                Diagram diagram = new Diagram(file); // load diagram
                diagrams.Add(diagram);

                int pageCount = diagram.Pages.Count;
                int shapeCount = 0;

                foreach (Page page in diagram.Pages)
                {
                    shapeCount += page.Shapes.Count;
                }

                reportLines.Add($"Source: {file} | Pages: {pageCount} | Shapes: {shapeCount}");
            }

            // Merge diagrams using Combine
            Diagram mergedDiagram = diagrams[0];
            for (int i = 1; i < diagrams.Count; i++)
            {
                mergedDiagram.Combine(diagrams[i]);
            }

            // Save merged diagram
            mergedDiagram.Save("mergedDiagram.vsdx", SaveFileFormat.Vsdx);

            // Output summary report
            Console.WriteLine("Merge Summary Report:");
            foreach (string line in reportLines)
            {
                Console.WriteLine(line);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
