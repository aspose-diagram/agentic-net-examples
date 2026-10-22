using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_flattened.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of group shapes to be flattened
                List<long> groupShapeIds = new List<long>();

                // Scan all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify group shapes that are not already marked as deleted
                    if (shape.Type == TypeValue.Group && shape.Del == BOOL.False)
                    {
                        // Add the group's ID to the list for later processing
                        groupShapeIds.Add(shape.ID);
                    }
                }

                // Flatten each identified group
                foreach (long groupId in groupShapeIds)
                {
                    // Retrieve the group shape
                    Shape groupShape = page.Shapes.GetShape(groupId);

                    // Iterate over child shapes within the group
                    foreach (Shape child in groupShape.Shapes)
                    {
                        // Ensure child shape is not marked as deleted
                        if (child.Del == BOOL.False)
                        {
                            // Optionally, you could adjust child properties here
                            // For this example we simply ensure the child remains visible
                            child.Del = BOOL.False;
                        }
                    }

                    // Mark the group shape itself as deleted to flatten it
                    groupShape.Del = BOOL.True;
                }
            }

            // Save the optimized diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram optimized and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}