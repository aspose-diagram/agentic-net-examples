using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: program <originalDiagramPath>");
            return;
        }

        // Path to the source Visio file.
        string originalPath = args[0];
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        try
        {
            // Load the original diagram from the file system.
            Diagram originalDiagram = new Diagram(originalPath);

            // Clone the diagram by saving to a temporary file and re‑loading.
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");
            originalDiagram.Save(tempPath, SaveFileFormat.Vsdx); // Save a copy.
            Diagram clonedDiagram = new Diagram(tempPath);      // Load the copy.
            File.Delete(tempPath);                             // Clean up the temp file.

            bool anyMismatch = false;

            // Iterate over each page (by index) in both diagrams.
            for (int pageIndex = 0; pageIndex < originalDiagram.Pages.Count; pageIndex++)
            {
                // Retrieve corresponding pages from original and clone.
                Page originalPage = originalDiagram.Pages[pageIndex];
                Page clonedPage = clonedDiagram.Pages[pageIndex];

                // Iterate over every shape on the original page.
                foreach (Shape originalShape in originalPage.Shapes)
                {
                    // Locate the shape with the same ID in the cloned page.
                    Shape clonedShape = clonedPage.Shapes.GetShape(originalShape.ID);
                    if (clonedShape == null)
                    {
                        Console.Error.WriteLine($"Shape ID {originalShape.ID} missing in cloned diagram (Page {pageIndex}).");
                        anyMismatch = true;
                        continue;
                    }

                    // Access the inherited fill information for both shapes.
                    var origInherit = originalShape.InheritFill;
                    var cloneInherit = clonedShape.InheritFill;

                    // Compare each relevant fill cell value.
                    if (origInherit.FillForegnd.Value != cloneInherit.FillForegnd.Value)
                    {
                        Console.Error.WriteLine($"FillForegnd mismatch on Shape ID {originalShape.ID} (Page {pageIndex}).");
                        anyMismatch = true;
                    }
                    if (origInherit.FillBkgnd.Value != cloneInherit.FillBkgnd.Value)
                    {
                        Console.Error.WriteLine($"FillBkgnd mismatch on Shape ID {originalShape.ID} (Page {pageIndex}).");
                        anyMismatch = true;
                    }
                    if (origInherit.FillPattern.Value != cloneInherit.FillPattern.Value)
                    {
                        Console.Error.WriteLine($"FillPattern mismatch on Shape ID {originalShape.ID} (Page {pageIndex}).");
                        anyMismatch = true;
                    }
                    if (origInherit.ShdwForegnd.Value != cloneInherit.ShdwForegnd.Value)
                    {
                        Console.Error.WriteLine($"ShdwForegnd mismatch on Shape ID {originalShape.ID} (Page {pageIndex}).");
                        anyMismatch = true;
                    }
                    if (origInherit.ShdwPattern.Value != cloneInherit.ShdwPattern.Value)
                    {
                        Console.Error.WriteLine($"ShdwPattern mismatch on Shape ID {originalShape.ID} (Page {pageIndex}).");
                        anyMismatch = true;
                    }
                    if (origInherit.ShapeShdwType.Value != cloneInherit.ShapeShdwType.Value)
                    {
                        Console.Error.WriteLine($"ShapeShdwType mismatch on Shape ID {originalShape.ID} (Page {pageIndex}).");
                        anyMismatch = true;
                    }
                }
            }

            // Report final result.
            if (anyMismatch)
            {
                Console.Error.WriteLine("Fill inheritance values differ between original and cloned diagrams.");
            }
            else
            {
                Console.WriteLine("All fill inheritance values are consistent after cloning.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose‑Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}