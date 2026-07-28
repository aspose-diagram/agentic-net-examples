using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output file path (second argument)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Work with the first page (index 0)
        if (diagram.Pages.Count == 0)
        {
            Console.WriteLine("The diagram contains no pages.");
            return;
        }

        Page page = diagram.Pages[0];

        // Retrieve page width (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Define spacing between shapes (in inches)
        double horizontalSpacing = 0.5;
        double verticalSpacing = 0.5;

        // Determine a reference shape size (use the first shape as a template)
        if (page.Shapes.Count == 0)
        {
            Console.WriteLine("The page contains no shapes to arrange.");
            return;
        }

        // Get the first shape to obtain its width and height
        Shape firstShape = page.Shapes.GetShape(page.Shapes[0].ID);
        double shapeWidth = firstShape.XForm.Width.Value;
        double shapeHeight = firstShape.XForm.Height.Value;

        // Compute how many columns can fit within the page width
        int columns = (int)Math.Floor((pageWidth + horizontalSpacing) / (shapeWidth + horizontalSpacing));
        if (columns < 1) columns = 1; // Ensure at least one column

        // Starting positions (center of first shape)
        double startX = shapeWidth / 2.0 + horizontalSpacing;
        double startY = shapeHeight / 2.0 + verticalSpacing;

        // Reposition each shape in a grid layout
        int index = 0;
        foreach (Shape shape in page.Shapes)
        {
            int col = index % columns;
            int row = index / columns;

            double pinX = startX + col * (shapeWidth + horizontalSpacing);
            double pinY = startY + row * (shapeHeight + verticalSpacing);

            // Ensure shapes stay within page bounds
            if (pinX + shapeWidth / 2.0 > pageWidth)
                pinX = pageWidth - shapeWidth / 2.0;
            if (pinY + shapeHeight / 2.0 > pageHeight)
                pinY = pageHeight - shapeHeight / 2.0;

            shape.XForm.PinX.Value = pinX;
            shape.XForm.PinY.Value = pinY;

            index++;
        }

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to '{outputPath}' with {columns} columns per row.");
    }
}
