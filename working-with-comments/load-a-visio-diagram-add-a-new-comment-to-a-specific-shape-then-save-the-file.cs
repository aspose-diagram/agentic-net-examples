using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input Visio file, shape name, comment text, output Visio file
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeName> <commentText> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeName = args[1];
        string commentText = args[2];
        string outputPath = args[3];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (adjust as needed for other pages)
                Page page = diagram.Pages[0];

                // Locate the shape by its name on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a name and is not deleted
                    // Fixed: Shape.Name is a string, not a Str2Value; compare directly.
                    // Fixed: Shape.Del is a BOOL enum; compare to BOOL.False.
                    if (shape.Name != null && shape.Name == shapeName && shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape named '{shapeName}' was not found on the first page.");
                    return;
                }

                // Add a comment associated with the found shape
                // This uses the Page.AddComment(Shape, string) overload
                page.AddComment(targetShape, commentText);

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
