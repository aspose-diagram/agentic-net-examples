using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string visioPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Output PDF file path for the specific shape
        string pdfOutputPath = "shape_output.pdf";

        // Identifier of the shape to export (change as needed)
        long targetShapeId = 5; // example shape ID

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
                throw new Exception("The diagram contains no pages.");

            // Use the first page (or adjust to the required page index)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
                throw new Exception($"Shape with ID {targetShapeId} was not found on page '{page.Name}'.");

            // Create PDF save options (high‑resolution rasterization not supported via property)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Create a temporary diagram containing only the target shape
            Diagram tempDiagram = new Diagram();

            // Add a new page to the temporary diagram
            Page tempPage = new Page();
            tempDiagram.Pages.Add(tempPage);

            // Ensure the original shape has a master before cloning
            if (shape.Master == null)
                throw new Exception("The target shape does not have an associated master.");

            // Clone the shape geometry into the temporary page
            long clonedShapeId = tempPage.AddShape(
                shape.XForm.PinX.Value,
                shape.XForm.PinY.Value,
                shape.XForm.Width.Value,
                shape.XForm.Height.Value,
                shape.Master.Name);

            // Retrieve the cloned shape instance
            Shape clonedShape = tempPage.Shapes.GetShape(clonedShapeId);
            if (clonedShape == null)
                throw new Exception("Failed to retrieve the cloned shape.");

            // Copy all properties from the original shape to the cloned shape
            clonedShape.Copy(shape);

            // Save the temporary diagram (which contains only the target shape) as PDF
            tempDiagram.Save(pdfOutputPath, pdfOptions);

            Console.WriteLine($"Shape ID {targetShapeId} exported successfully to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}