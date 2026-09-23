using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) from the diagram
            Page page = diagram.Pages[0];

            // Determine page dimensions (in inches) to size the background shape
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that spans the entire page.
            // The AddShape method returns the shape ID (long).
            long bgShapeId = page.AddShape(
                pinX: pageWidth / 2,   // center X
                pinY: pageHeight / 2,  // center Y
                width: pageWidth,
                height: pageHeight,
                masterName: "Rectangle",
                isCalculate: false);

            // Retrieve the newly added shape using its ID
            Shape bgShape = page.Shapes.GetShape(bgShapeId);

            // Set the shape's fill to a solid color (light blue)
            bgShape.Fill.FillPattern.Value = 1;          // 1 = solid fill
            bgShape.Fill.FillForegnd.Value = "#ADD8E6"; // hexadecimal color

            // Remove the border by setting line pattern to 0 (no line)
            bgShape.Line.LinePattern.Value = 0;

            // Send the shape to the back so it appears behind all other content
            bgShape.SendToBack();

            // Optionally lock the shape to prevent accidental selection/editing
            bgShape.Protection.LockSelect.Value = BOOL.True;

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}