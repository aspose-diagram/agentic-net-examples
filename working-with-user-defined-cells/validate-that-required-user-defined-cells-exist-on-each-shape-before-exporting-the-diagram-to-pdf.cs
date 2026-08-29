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

            // List of required user-defined cell names (without the "User." prefix)
            var requiredCells = new List<string> { "MyCell1", "MyCell2" };

            // Load the Visio diagram
            using (var diagram = new Diagram("input.vsdx"))
            {
                // Validate each shape on every page
                foreach (var page in diagram.Pages)
                {
                    foreach (var shape in page.Shapes)
                    {
                        ValidateShapeUserCells(shape, requiredCells);
                    }
                }

                // Export the diagram to PDF
                var pdfOptions = new PdfSaveOptions
                {
                    ExportGuideShapes = true // keep default behavior
                };
                diagram.Save("output.pdf", pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ValidateShapeUserCells(Shape shape, List<string> requiredCells)
    {
        // Collect existing user-defined cell names for the shape
        var existingNames = new HashSet<string>();
        foreach (var user in shape.Users)
        {
            existingNames.Add(user.Name);
        }

        // Check each required cell
        foreach (var cellName in requiredCells)
        {
            if (!existingNames.Contains(cellName))
            {
                throw new InvalidOperationException(
                    $"Shape ID {shape.ID} on page '{shape.Page?.Name}' is missing required user-defined cell '{cellName}'.");
            }
        }
    }
}
