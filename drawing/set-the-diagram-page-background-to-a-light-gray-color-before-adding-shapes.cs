using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a drawing window (required for a usable diagram)
            Window window = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized,
                WindowWidth = 1100,
                WindowHeight = 700
            };
            diagram.Windows.Add(window);

            // Create the foreground page
            Page foregroundPage = new Page
            {
                ID = 1,
                Name = "Page-1"
            };
            diagram.Pages.Add(foregroundPage);

            // Create a background page that will hold the background color shape
            Page backgroundPage = new Page
            {
                ID = 2,
                Name = "Background-1",
                Background = BOOL.True // Mark as background page
            };
            diagram.Pages.Add(backgroundPage);

            // Link the background page to the foreground page
            foregroundPage.BackPage = backgroundPage;

            // Retrieve page dimensions to size the background rectangle
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that spans the entire page on the background page
            // DrawRectangle(pinX, pinY, width, height) where pinX/Y are the center of the shape
            long bgShapeId = backgroundPage.DrawRectangle(pageWidth / 2, pageHeight / 2, pageWidth, pageHeight);
            Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

            // Configure the shape to have a solid fill pattern
            bgShape.Fill.FillPattern.Value = 1; // Solid fill
            // Set the fill foreground color to light gray (#D3D3D3)
            bgShape.Fill.FillForegnd.Value = "#D3D3D3";

            // Remove the shape's line to keep the background clean
            bgShape.Line.LinePattern.Value = 0; // No line

            // Add a sample ellipse shape to the foreground page after setting the background
            // DrawEllipse(pinX, pinY, width, height)
            long sampleShapeId = foregroundPage.DrawEllipse(pageWidth / 2, pageHeight / 2, 2.0, 1.0);
            Shape sampleShape = foregroundPage.Shapes.GetShape(sampleShapeId);
            sampleShape.Fill.FillPattern.Value = 1; // Solid fill
            sampleShape.Fill.FillForegnd.Value = "#FF0000"; // Red fill for demonstration

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}