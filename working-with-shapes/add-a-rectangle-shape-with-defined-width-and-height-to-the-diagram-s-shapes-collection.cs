using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments: stencil file and output diagram path
        string stencilPath = args.Length > 0 ? args[0] : "stencil.vss";
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure the diagram has at least one window (required for saving)
            if (diagram.Windows.Count == 0)
            {
                Window win = new Window();
                win.WindowType = WindowTypeValue.Drawing;
                win.WindowState = WindowStateValue.Maximized;
                win.WindowWidth = 1100;
                win.WindowHeight = 700;
                diagram.Windows.Add(win);
            }

            // Ensure there is at least one page; add if missing
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Load the "Rectangle" master from the provided stencil
            // This makes the master available for shape creation
            diagram.AddMaster(stencilPath, "Rectangle");

            // Retrieve the first page to add the shape onto
            Page page = diagram.Pages[0];

            // Define rectangle position (PinX, PinY) and desired size
            double pinX = 2.0;   // horizontal center position in inches
            double pinY = 2.0;   // vertical center position in inches
            double rectWidth = 3.0;   // desired width in inches
            double rectHeight = 2.0;  // desired height in inches

            // Add a rectangle shape using the master name.
            // The fourth argument 'false' indicates that the shape should not be recalculated immediately.
            long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the newly added shape by its ID
            Shape rectShape = page.Shapes.GetShape(shapeId);

            // Set the shape's width and height via the XForm properties
            rectShape.XForm.Width.Value = rectWidth;
            rectShape.XForm.Height.Value = rectHeight;

            // Save the diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}