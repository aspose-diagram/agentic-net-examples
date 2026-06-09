using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input VSDX path as first argument and output path as second argument.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <input.vsdx> <output.vsdx> [hexColor]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        // Optional solid fill color (hex string like "#FFFFFF"). Default to white.
        string fillColor = args.Length >= 3 ? args[2] : "#FFFFFF";

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages and replace background with a solid color rectangle.
        foreach (Page page in diagram.Pages)
        {
            // Retrieve page dimensions (in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center coordinates for the rectangle shape.
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;

            // Add a rectangle shape that spans the entire page.
            // Master name "Rectangle" is a built‑in Visio master.
            long rectId = page.AddShape(centerX, centerY, pageWidth, pageHeight, "Rectangle");

            // Retrieve the newly added shape.
            Shape rect = page.Shapes.GetShape(rectId);

            // Set solid fill pattern.
            rect.Fill.FillPattern.Value = 1; // 1 = solid fill.

            // Apply the desired foreground color.
            rect.Fill.FillForegnd.Value = fillColor;

            // Remove any outline.
            rect.Line.LinePattern.Value = 0; // 0 = no line.

            // Send the rectangle to the back so other content appears above it.
            rect.SendToBack();

            // Make the background shape non‑selectable.
            rect.Protection.LockSelect.Value = BOOL.True;
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
