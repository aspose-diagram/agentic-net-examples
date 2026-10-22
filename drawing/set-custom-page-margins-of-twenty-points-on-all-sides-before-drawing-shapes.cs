using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Output file path for the generated diagram
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram instance
            Diagram diagram = new Diagram();

            // Retrieve the first (default) page of the diagram
            Page page = diagram.Pages[0];

            // Convert 20 points to inches (1 point = 1/72 inch)
            double marginInInches = 20.0 / 72.0;

            // Set all four page margins via the PrintProps collection
            page.PageSheet.PrintProps.PageTopMargin.Value = marginInInches;
            page.PageSheet.PrintProps.PageBottomMargin.Value = marginInInches;
            page.PageSheet.PrintProps.PageLeftMargin.Value = marginInInches;
            page.PageSheet.PrintProps.PageRightMargin.Value = marginInInches;

            // Draw a rectangle shape on the page (pinX, pinY, width, height) – values are in inches
            long shapeId = page.DrawRectangle(1.0, 1.0, 2.0, 1.0);

            // Retrieve the created shape to modify its properties
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text runs and add new text to the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Shape"));

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}