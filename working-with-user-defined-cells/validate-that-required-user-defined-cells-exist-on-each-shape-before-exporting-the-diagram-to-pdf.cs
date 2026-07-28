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

            // Load the Visio diagram from a file.
            Diagram diagram = new Diagram("input.vsdx");

            // List of required user‑defined cell names (without the "User." prefix).
            List<string> requiredCells = new List<string> { "MyCell1", "MyCell2" };

            // Validate every shape on every page.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    ValidateUserCells(shape, requiredCells);
                }
            }

            // Export the diagram to PDF after successful validation.
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                ExportGuideShapes = true   // keep default behavior; can be changed if needed
            };
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Checks that all required user‑defined cells exist on the given shape.
    static void ValidateUserCells(Shape shape, List<string> requiredCells)
    {
        // Collect the names of all user‑defined cells present on the shape.
        HashSet<string> existingNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (User userCell in shape.Users)
        {
            if (!string.IsNullOrEmpty(userCell.Name))
                existingNames.Add(userCell.Name);
        }

        // Verify each required cell is present; throw if any are missing.
        foreach (string cellName in requiredCells)
        {
            if (!existingNames.Contains(cellName))
            {
                string pageName = shape.Page?.Name ?? "unknown";
                throw new InvalidOperationException(
                    $"Shape ID {shape.ID} on page '{pageName}' is missing required user-defined cell '{cellName}'.");
            }
        }
    }
}
