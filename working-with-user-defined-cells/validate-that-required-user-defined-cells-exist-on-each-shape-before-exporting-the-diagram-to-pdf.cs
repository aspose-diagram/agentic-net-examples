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

            // List of required user‑defined cell names (adjust as needed)
            var requiredUserCells = new List<string> { "MyCell1", "MyCell2" };

            // Load the Visio diagram (replace with your file path)
            var diagram = new Diagram("input.vsdx");

            // Validate each shape on every page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    ValidateShapeUserCells(shape, requiredUserCells);
                }
            }

            // Set PDF export options
            var pdfOptions = new PdfSaveOptions
            {
                // Example: do not export guide shapes
                ExportGuideShapes = false
            };

            // Export the diagram to PDF (replace with your output path)
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Checks that all required user‑defined cells exist on the given shape
    static void ValidateShapeUserCells(Shape shape, List<string> requiredCells)
    {
        // Users collection may be empty if the shape has no user‑defined cells
        var users = shape.Users;

        foreach (string cellName in requiredCells)
        {
            bool found = false;

            foreach (User user in users)
            {
                if (string.Equals(user.Name, cellName, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                // Throw an exception or handle the missing cell as required
                throw new InvalidOperationException(
                    $"Shape ID {shape.ID} ('{shape.Name}') is missing required user-defined cell '{cellName}'.");
            }
        }
    }
}
