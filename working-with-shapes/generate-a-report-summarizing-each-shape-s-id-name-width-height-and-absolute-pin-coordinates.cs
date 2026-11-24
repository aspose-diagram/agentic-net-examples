using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the diagram file path as a command-line argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Compute absolute PinX and PinY (simple additive approach for nested groups)
                    double absolutePinX = GetAbsolutePinX(shape);
                    double absolutePinY = GetAbsolutePinY(shape);

                    // Retrieve width and height from the shape's XForm
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Output the shape summary
                    Console.WriteLine(
                        $"Shape ID: {shape.ID}, " +
                        $"Name: {shape.Name}, " +
                        $"Width: {width}, " +
                        $"Height: {height}, " +
                        $"AbsPinX: {absolutePinX}, " +
                        $"AbsPinY: {absolutePinY}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to calculate absolute PinX by traversing parent hierarchy
    static double GetAbsolutePinX(Shape shape)
    {
        double pinX = shape.XForm.PinX.Value;
        Shape parent = shape.ParentShape;
        while (parent != null)
        {
            pinX += parent.XForm.PinX.Value;
            parent = parent.ParentShape;
        }
        return pinX;
    }

    // Helper method to calculate absolute PinY by traversing parent hierarchy
    static double GetAbsolutePinY(Shape shape)
    {
        double pinY = shape.XForm.PinY.Value;
        Shape parent = shape.ParentShape;
        while (parent != null)
        {
            pinY += parent.XForm.PinY.Value;
            parent = parent.ParentShape;
        }
        return pinY;
    }
}