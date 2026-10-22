using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is output file, remaining are source diagram files.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <outputMaster.vsdx> <source1.vsdx> [<source2.vsdx> ...]");
            return;
        }

        string outputPath = args[0];

        // Guard for output directory existence.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Verify each source file exists.
        for (int i = 1; i < args.Length; i++)
        {
            if (!File.Exists(args[i]))
            {
                Console.Error.WriteLine($"Source file not found: {args[i]}");
                return;
            }
        }

        try
        {
            // Create an empty master diagram.
            using (Diagram masterDiagram = new Diagram())
            {
                // Add a temporary placeholder page (will be removed later).
                masterDiagram.Pages.Add(new Page());

                // Iterate over each source diagram and copy its first page.
                for (int i = 1; i < args.Length; i++)
                {
                    string sourcePath = args[i];

                    using (Diagram sourceDiagram = new Diagram(sourcePath))
                    {
                        // Ensure the source diagram has at least one page.
                        if (sourceDiagram.Pages.Count == 0)
                        {
                            Console.WriteLine($"Source diagram has no pages: {sourcePath}");
                            continue;
                        }

                        // Retrieve the first page from the source.
                        Page sourcePage = sourceDiagram.Pages[0];

                        // Create a new page instance and copy the source page's content.
                        Page copiedPage = new Page();
                        copiedPage.Copy(sourcePage);

                        // Preserve the original page ID.
                        copiedPage.ID = sourcePage.ID;

                        // Add the copied page to the master diagram.
                        masterDiagram.Pages.Add(copiedPage);
                    }
                }

                // Remove the initial placeholder page if it still exists.
                if (masterDiagram.Pages.Count > 0)
                {
                    // The placeholder is the first page added before any copies.
                    // It typically has ID = 0; we remove it to keep only copied pages.
                    masterDiagram.Pages.Remove(masterDiagram.Pages[0]);
                }

                // Save the master diagram containing the copied pages.
                masterDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Master diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}