using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths
        string inputPath = "input.vsdx";
        string outputPdfPath = "output.pdf";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use the first page as the working page
            Page page = diagram.Pages[0];

            // Retrieve the master that defines the gradient fill (replace with actual master name)
            Master master = diagram.Masters.GetMasterByName("GradientMaster");
            if (master == null)
                throw new Exception("Master 'GradientMaster' not found in the diagram.");

            // The master’s visual definition is stored in its first shape
            Shape masterShape = master.Shapes[0];

            // Add a new shape that references the master
            long shapeId = page.AddShape(2.0, 2.0, master.Name);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Validate that the shape inherits the master’s gradient fill settings
            bool inheritsPattern = shape.Fill.FillPattern.Value == masterShape.Fill.FillPattern.Value;
            bool gradientEnabled = shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True;

            if (!inheritsPattern || !gradientEnabled)
                throw new Exception("Shape does not correctly inherit the master's gradient fill.");

            Console.WriteLine("Gradient fill inheritance validated successfully.");

            // Export the diagram to PDF with a fallback font
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine($"Diagram exported to PDF at: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}