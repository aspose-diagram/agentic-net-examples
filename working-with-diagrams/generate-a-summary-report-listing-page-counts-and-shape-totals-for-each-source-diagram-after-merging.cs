using System;
using Aspose.Diagram;

namespace DiagramMergeReport
{
    // Holds summary information for each source diagram
    class SourceInfo
    {
        public string FilePath { get; set; }
        public int PageCount { get; set; }
        public int ShapeCount { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Paths to source Visio files – replace with actual file locations
                string[] sourceFiles = new string[]
                {
                    "source1.vsdx",
                    "source2.vsdx",
                    "source3.vsdx"
                };

                // Collection to store summary data for each source diagram
                SourceInfo[] summaries = new SourceInfo[sourceFiles.Length];

                Diagram mergedDiagram = null;

                for (int i = 0; i < sourceFiles.Length; i++)
                {
                    string path = sourceFiles[i];

                    // Load the diagram from file
                    Diagram diagram = new Diagram(path);

                    // Count pages
                    int pageCount = diagram.Pages.Count;

                    // Count shapes across all pages
                    int shapeCount = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            shapeCount++;
                        }
                    }

                    // Store the summary information
                    summaries[i] = new SourceInfo
                    {
                        FilePath = path,
                        PageCount = pageCount,
                        ShapeCount = shapeCount
                    };

                    // Build the merged diagram
                    if (mergedDiagram == null)
                    {
                        // First diagram becomes the base of the merge
                        mergedDiagram = diagram;
                    }
                    else
                    {
                        // Merge subsequent diagrams into the base diagram
                        mergedDiagram.Combine(diagram);
                    }
                }

                // Save the merged diagram
                if (mergedDiagram != null)
                {
                    string outputPath = "merged_output.vsdx";
                    mergedDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Merged diagram saved to: {outputPath}");
                }
                else
                {
                    Console.WriteLine("No diagrams were loaded; merged diagram not created.");
                }

                // Output the summary report
                Console.WriteLine("\nSummary Report:");
                Console.WriteLine("-------------------------------------------------");
                foreach (SourceInfo info in summaries)
                {
                    Console.WriteLine($"File: {info.FilePath}");
                    Console.WriteLine($"  Pages: {info.PageCount}");
                    Console.WriteLine($"  Shapes: {info.ShapeCount}");
                    Console.WriteLine("-------------------------------------------------");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}