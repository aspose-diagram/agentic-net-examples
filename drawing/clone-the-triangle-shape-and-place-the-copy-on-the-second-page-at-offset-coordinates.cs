using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure at least one page exists
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page (source)
            Page sourcePage = diagram.Pages[0];

            // Get or create the second page (target)
            Page targetPage;
            if (diagram.Pages.Count > 1)
            {
                targetPage = diagram.Pages[1];
            }
            else
            {
                // Create a new page with a unique ID
                int maxId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxId) maxId = p.ID;
                }
                targetPage = new Page(maxId + 1);
                diagram.Pages.Add(targetPage);
            }

            // Locate the triangle shape on the source page by its master name
            Shape triangleShape = null;
            foreach (Shape shape in sourcePage.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Ensure the shape has a master and compare its name
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                Console.Error.WriteLine("Triangle shape not found on the first page.");
                return;
            }

            // Original position of the triangle
            double originalPinX = triangleShape.XForm.PinX.Value;
            double originalPinY = triangleShape.XForm.PinY.Value;

            // Define offset (in inches) for the cloned shape
            double offsetX = 2.0;
            double offsetY = 2.0;

            double newPinX = originalPinX + offsetX;
            double newPinY = originalPinY + offsetY;

            // Add a copy of the triangle to the target page using the same master name
            // The fourth parameter (isCalculate) must be a bool, not BOOL
            long newShapeId = targetPage.AddShape(newPinX, newPinY, triangleShape.Master.Name, false);

            // Retrieve the newly added shape
            Shape newShape = targetPage.Shapes.GetShape(newShapeId);
            if (newShape != null)
            {
                // Copy line color
                newShape.Line.LineColor.Value = triangleShape.Line.LineColor.Value;

                // Copy fill foreground color and pattern
                newShape.Fill.FillForegnd.Value = triangleShape.Fill.FillForegnd.Value;
                newShape.Fill.FillPattern.Value = triangleShape.Fill.FillPattern.Value;

                // Copy text content
                if (triangleShape.Text != null && triangleShape.Text.Value != null)
                {
                    newShape.Text.Value.Clear();
                    foreach (var txt in triangleShape.Text.Value)
                    {
                        // Preserve each text run or character property
                        newShape.Text.Value.Add(txt);
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
