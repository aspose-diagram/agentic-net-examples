using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Simple container for diagram statistics
    private class DiagramStats
    {
        public string FilePath { get; set; }
        public int PageCount { get; set; }
        public int ShapeCount { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect at least two arguments: output file path and one or more source diagram paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <output.vsdx> <source1.vsdx> [<source2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];
        string outputDirectory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDirectory))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDirectory}");
            return;
        }

        // Collect source file paths
        List<string> sourcePaths = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            sourcePaths.Add(args[i]);
        }

        // Validate each source file exists
        foreach (string path in sourcePaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Source file not found: {path}");
                return;
            }
        }

        // List to hold statistics for each source diagram
        List<DiagramStats> statsList = new List<DiagramStats>();

        // Load the first diagram as the target for merging
        Diagram targetDiagram = null;
        try
        {
            // Load first diagram
            string firstPath = sourcePaths[0];
            using (Diagram firstDiagram = new Diagram(firstPath))
            {
                // Clone the first diagram into targetDiagram (deep copy)
                // Since Diagram implements IDisposable, we create a new instance via the copy constructor pattern
                // Aspose.Diagram does not provide a direct copy constructor, so we save to a memory stream and reload
                using (MemoryStream ms = new MemoryStream())
                {
                    firstDiagram.Save(ms, SaveFileFormat.Vsdx);
                    ms.Position = 0;
                    targetDiagram = new Diagram(ms);
                }

                // Compute statistics for the first diagram
                DiagramStats firstStats = ComputeStats(firstDiagram, firstPath);
                statsList.Add(firstStats);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading first diagram: {ex.Message}");
            return;
        }

        // Process remaining source diagrams
        for (int idx = 1; idx < sourcePaths.Count; idx++)
        {
            string srcPath = sourcePaths[idx];
            try
            {
                using (Diagram srcDiagram = new Diagram(srcPath))
                {
                    // Compute and store statistics for this source diagram
                    DiagramStats srcStats = ComputeStats(srcDiagram, srcPath);
                    statsList.Add(srcStats);

                    // Merge current source diagram into the target diagram
                    // The Combine method merges pages, masters, styles, etc.
                    targetDiagram.Combine(srcDiagram);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing source diagram '{srcPath}': {ex.Message}");
                // Continue processing remaining diagrams
            }
        }

        // Save the merged diagram
        try
        {
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Merged diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving merged diagram: {ex.Message}");
        }
        finally
        {
            // Dispose target diagram if it implements IDisposable
            if (targetDiagram != null)
            {
                targetDiagram.Dispose();
            }
        }

        // Output summary report
        Console.WriteLine("\nSummary Report:");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("{0,-30} {1,10} {2,12}", "Source File", "Pages", "Shapes");
        Console.WriteLine("-------------------------------------------------");
        foreach (DiagramStats ds in statsList)
        {
            Console.WriteLine("{0,-30} {1,10} {2,12}", Path.GetFileName(ds.FilePath), ds.PageCount, ds.ShapeCount);
        }
        Console.WriteLine("-------------------------------------------------");
    }

    // Helper method to compute page and shape counts for a diagram
    private static DiagramStats ComputeStats(Diagram diagram, string filePath)
    {
        int pageCount = diagram.Pages.Count;
        int shapeCount = 0;

        // Iterate over each page and count shapes
        foreach (Page page in diagram.Pages)
        {
            // page.Shapes is a collection; its Count property gives the number of shapes on the page
            shapeCount += page.Shapes.Count;
        }

        return new DiagramStats
        {
            FilePath = filePath,
            PageCount = pageCount,
            ShapeCount = shapeCount
        };
    }
}