using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramMerger
{
    public static void MergeAndReport(string[] sourceFiles, string outputFile)
    {
        var reports = new List<(string FileName, int PageCount, int ShapeCount)>();
        Diagram mainDiagram = null;

        foreach (var filePath in sourceFiles)
        {
            // Load diagram from file
            var diagram = new Diagram(filePath);

            // Count pages
            int pageCount = diagram.Pages.Count;

            // Count shapes on all pages
            int shapeCount = 0;
            foreach (Page page in diagram.Pages)
            {
                shapeCount += page.Shapes.Count;
            }

            reports.Add((Path.GetFileName(filePath), pageCount, shapeCount));

            // Merge diagrams
            if (mainDiagram == null)
            {
                mainDiagram = diagram; // first diagram becomes the base
            }
            else
            {
                mainDiagram.Combine(diagram);
                diagram.Dispose(); // dispose merged diagram
            }
        }

        // Save merged diagram
        if (mainDiagram != null)
        {
            mainDiagram.Save(outputFile, SaveFileFormat.Vdx);
        }

        // Print summary report
        Console.WriteLine("Merge Summary Report:");
        foreach (var report in reports)
        {
            Console.WriteLine($"File: {report.FileName}, Pages: {report.PageCount}, Shapes: {report.ShapeCount}");
        }

        // Cleanup
        mainDiagram?.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramMerger.MergeAndReport(null, "");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
