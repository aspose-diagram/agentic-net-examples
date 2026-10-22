using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one window; create one if necessary.
            if (diagram.Windows.Count == 0)
            {
                Window newWindow = new Window();
                newWindow.WindowType = WindowTypeValue.Drawing;
                newWindow.WindowState = WindowStateValue.Maximized;
                newWindow.WindowWidth = 1100;
                newWindow.WindowHeight = 700;
                diagram.Windows.Add(newWindow);
            }

            // Make guides visible on the page via the first window.
            diagram.Windows[0].ShowGuides = BOOL.True;

            // -----------------------------------------------------------------
            // Define guide positions (in inches). These will be used to draw
            // visual guide lines and to align shapes precisely.
            // -----------------------------------------------------------------
            double guideX = 5.0; // Vertical guide at X = 5 inches
            double guideY = 3.0; // Horizontal guide at Y = 3 inches

            // Access the first page.
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches) for full‑length guide lines.
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // -----------------------------------------------------------------
            // Add a vertical guide line (thin gray line) at guideX.
            // DrawPolyline expects a flat double array: x1, y1, x2, y2, ...
            // -----------------------------------------------------------------
            long verticalGuideId = page.DrawPolyline(new double[] { guideX, 0.0, guideX, pageHeight });
            Shape verticalGuide = page.Shapes.GetShape(verticalGuideId);
            // Style the guide line: light gray, thin weight.
            verticalGuide.Line.LineColor.Value = "#C0C0C0";
            verticalGuide.Line.LineWeight.Value = 0.01; // thin line

            // -----------------------------------------------------------------
            // Add a horizontal guide line at guideY.
            // -----------------------------------------------------------------
            long horizontalGuideId = page.DrawPolyline(new double[] { 0.0, guideY, pageWidth, guideY });
            Shape horizontalGuide = page.Shapes.GetShape(horizontalGuideId);
            horizontalGuide.Line.LineColor.Value = "#C0C0C0";
            horizontalGuide.Line.LineWeight.Value = 0.01; // thin line

            // -----------------------------------------------------------------
            // Add a rectangle shape that will be aligned to the intersection
            // of the two guide lines.
            // The AddShape overload: (pinX, pinY, masterName, isCalculate)
            // -----------------------------------------------------------------
            long rectShapeId = diagram.ActivePage.AddShape(guideX, guideY, "Rectangle", false);
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Align the rectangle precisely to the guide positions.
            rectShape.XForm.PinX.Value = guideX;
            rectShape.XForm.PinY.Value = guideY;

            // Optionally set the rectangle size.
            rectShape.XForm.Width.Value = 2.0;   // 2 inches width
            rectShape.XForm.Height.Value = 1.0;  // 1 inch height

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}