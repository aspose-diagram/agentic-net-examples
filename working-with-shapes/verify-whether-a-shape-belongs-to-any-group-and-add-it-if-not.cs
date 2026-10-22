using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (same folder, named output.vsdx)
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output.vsdx");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that are not in any group
                    var shapesToGroup = new System.Collections.Generic.List<Shape>();

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Verify whether the shape belongs to any group
                        bool inGroup = shape.IsInGroup();

                        // If the shape is not in a group, add it to the list
                        if (!inGroup)
                        {
                            shapesToGroup.Add(shape);
                        }
                    }

                    // Create a group for each shape that is not already grouped
                    foreach (Shape shape in shapesToGroup)
                    {
                        // Group the single shape (creates a new group containing this shape)
                        Shape groupShape = page.Shapes.Group(new Shape[] { shape });

                        // Optionally set a name for the new group
                        if (groupShape != null)
                        {
                            // NameU is a plain string property, assign directly
                            groupShape.NameU = $"Group_{shape.ID}";
                        }
                    }
                }

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}