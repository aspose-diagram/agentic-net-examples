using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path and shape identifier.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeId>");
            return;
        }

        string visioPath = args[0];
        string shapeIdArg = args[1];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – '{visioPath}'.");
            return;
        }

        // Parse the shape identifier (must be a long integer).
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape identifier '{shapeIdArg}'. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            Shape foundShape = null;
            Page containingPage = null;

            // Iterate through all pages to locate the shape with the specified ID.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through shapes on the current page.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Compare the shape's structural ID.
                    if (shape.ID == targetShapeId)
                    {
                        foundShape = shape;
                        containingPage = page;
                        break;
                    }
                }

                if (foundShape != null)
                    break;
            }

            if (foundShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} was not found in the diagram.");
                return;
            }

            // Output basic information about the located shape.
            Console.WriteLine($"Shape ID: {foundShape.ID}");
            Console.WriteLine($"Shape NameU: {foundShape.NameU}");
            Console.WriteLine($"Shape Type: {foundShape.Type}");
            Console.WriteLine($"Page Name: {containingPage.Name}");
            Console.WriteLine($"Master Name: {(foundShape.Master != null ? foundShape.Master.Name : "None")}");

            // Example processing: toggle visibility by setting width/height to zero if currently visible,
            // otherwise restore to a default size (1x1). This demonstrates a simple modification.
            if (foundShape.Del == BOOL.False)
            {
                // Hide the shape by collapsing its dimensions.
                foundShape.XForm.Width.Value = 0;
                foundShape.XForm.Height.Value = 0;
                Console.WriteLine("Shape has been hidden (dimensions set to zero).");
            }
            else
            {
                // Show the shape with a minimal size.
                foundShape.XForm.Width.Value = 1;
                foundShape.XForm.Height.Value = 1;
                Console.WriteLine("Shape was previously hidden; restored to minimal size.");
            }

            // Save the modified diagram to a new file (optional but demonstrates saving).
            string outputPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", 
                                            Path.GetFileNameWithoutExtension(visioPath) + "_modified.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}