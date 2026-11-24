using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Collect group shapes on the current page.
                    System.Collections.Generic.List<Shape> groupShapes = new System.Collections.Generic.List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify a group shape by its TypeValue.
                        if (shape.Type == TypeValue.Group && shape.Del == BOOL.False)
                        {
                            groupShapes.Add(shape);
                        }
                    }

                    // Flatten each identified group.
                    foreach (Shape groupShape in groupShapes)
                    {
                        // Move each child shape out of the group onto the page.
                        foreach (Shape child in groupShape.Shapes)
                        {
                            // Add the child shape directly to the page's shape collection.
                            page.Shapes.Add(child);
                        }

                        // Mark the original group shape as deleted so it will not be saved.
                        groupShape.Del = BOOL.True;
                    }
                }

                // Save the optimized diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram optimized and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}