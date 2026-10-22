using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: first argument is output file, remaining are source diagrams.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <outputPath> <sourceDiagram1> [<sourceDiagram2> ...]");
            return;
        }

        string outputPath = args[0];

        // Collect source file paths.
        List<string> sourcePaths = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            sourcePaths.Add(args[i]);
        }

        // Verify that each source file exists.
        foreach (string path in sourcePaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Source file not found: {path}");
                return;
            }
        }

        // List to hold page and shape counts for each source diagram.
        List<(string Path, int PageCount, int ShapeCount)> reportData = new List<(string, int, int)>();

        // Load the first diagram as the target for merging.
        try
        {
            using (Diagram targetDiagram = new Diagram(sourcePaths[0]))
            {
                // Count pages and shapes of the first (target) diagram.
                int firstPageCount = targetDiagram.Pages.Count;
                int firstShapeCount = 0;
                foreach (Aspose.Diagram.Page page in targetDiagram.Pages)
                {
                    firstShapeCount += page.Shapes.Count;
                }
                reportData.Add((sourcePaths[0], firstPageCount, firstShapeCount));

                // Process remaining source diagrams: count and merge.
                for (int idx = 1; idx < sourcePaths.Count; idx++)
                {
                    string srcPath = sourcePaths[idx];
                    try
                    {
                        using (Diagram srcDiagram = new Diagram(srcPath))
                        {
                            // Count pages and shapes of the current source diagram.
                            int srcPageCount = srcDiagram.Pages.Count;
                            int srcShapeCount = 0;
                            foreach (Aspose.Diagram.Page page in srcDiagram.Pages)
                            {
                                srcShapeCount += page.Shapes.Count;
                            }
                            reportData.Add((srcPath, srcPageCount, srcShapeCount));

                            // Merge the current source diagram into the target diagram.
                            targetDiagram.Combine(srcDiagram);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error processing source diagram '{srcPath}': {ex.Message}");
                        return;
                    }
                }

                // Save the merged diagram to the specified output path.
                try
                {
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error saving merged diagram to '{outputPath}': {ex.Message}");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading target diagram '{sourcePaths[0]}': {ex.Message}");
            return;
        }

        // Output the summary report for each source diagram.
        Console.WriteLine("Summary Report:");
        foreach (var item in reportData)
        {
            Console.WriteLine($"Source: {item.Path}");
            Console.WriteLine($"  Pages : {item.PageCount}");
            Console.WriteLine($"  Shapes: {item.ShapeCount}");
        }

        // Load the merged diagram to report its final page and shape totals.
        try
        {
            using (Diagram mergedDiagram = new Diagram(outputPath))
            {
                int mergedPageCount = mergedDiagram.Pages.Count;
                int mergedShapeCount = 0;
                foreach (Aspose.Diagram.Page page in mergedDiagram.Pages)
                {
                    mergedShapeCount += page.Shapes.Count;
                }

                Console.WriteLine("Merged Diagram:");
                Console.WriteLine($"  Pages : {mergedPageCount}");
                Console.WriteLine($"  Shapes: {mergedShapeCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading merged diagram for final report: {ex.Message}");
        }
    }
}