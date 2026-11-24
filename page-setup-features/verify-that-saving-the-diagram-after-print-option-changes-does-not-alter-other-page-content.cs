using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (allow command‑line overrides)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Store original shape IDs for each page to verify later
        List<List<long>> originalPageShapeIds = null;

        // ---------- Load diagram, capture shape IDs, modify print options, and save ----------
        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Capture shape IDs for every page
                originalPageShapeIds = new List<List<long>>();
                foreach (Page page in diagram.Pages)
                {
                    List<long> ids = new List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        ids.Add(shape.ID);
                    }
                    originalPageShapeIds.Add(ids);
                }

                // ----- Modify print options on the first page (if any) -----
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];

                    // Set orientation to Landscape
                    firstPage.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Set uniform margins (in inches)
                    firstPage.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                    firstPage.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
                    firstPage.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                    firstPage.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing or saving diagram: {ex.Message}");
            return;
        }

        // ---------- Verify that page content (shape IDs) is unchanged after saving ----------
        try
        {
            // Load the saved diagram
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                // Ensure the page count matches the original
                if (savedDiagram.Pages.Count != originalPageShapeIds.Count)
                {
                    throw new Exception("Page count mismatch after saving.");
                }

                // Compare shape IDs page by page
                for (int i = 0; i < savedDiagram.Pages.Count; i++)
                {
                    Page savedPage = savedDiagram.Pages[i];
                    List<long> originalIds = originalPageShapeIds[i];

                    // Collect shape IDs from the saved page
                    List<long> savedIds = new List<long>();
                    foreach (Shape shape in savedPage.Shapes)
                    {
                        savedIds.Add(shape.ID);
                    }

                    // Compare counts
                    if (savedIds.Count != originalIds.Count)
                    {
                        throw new Exception($"Shape count mismatch on page {i + 1} after saving.");
                    }

                    // Compare sets of IDs (order is not guaranteed)
                    var originalSet = new HashSet<long>(originalIds);
                    var savedSet = new HashSet<long>(savedIds);
                    if (!originalSet.SetEquals(savedSet))
                    {
                        throw new Exception($"Shape IDs differ on page {i + 1} after saving.");
                    }
                }

                Console.WriteLine("Verification succeeded: page content unchanged after modifying print options and saving.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }
}