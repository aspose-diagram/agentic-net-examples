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
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

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
                // Collect shapes that need to be removed to avoid modifying the collection during iteration
                List<Shape> shapesToRemove = new List<Shape>();

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape is not already deleted
                    if (shape.Del != BOOL.False)
                        continue;

                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData == null)
                        continue;

                    // Verify the foreign object is an embedded OLE object
                    if (shape.ForeignData.ObjectType != ObjectType.EmbeddedObject)
                        continue;

                    // Get the source name of the embedded object (e.g., file name with extension)
                    string sourceName = shape.ForeignData.ObjectSourceFullName;
                    if (string.IsNullOrEmpty(sourceName))
                        continue;

                    // Determine if the embedded object is an Excel worksheet based on file extension
                    if (sourceName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                        sourceName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                        sourceName.EndsWith(".xlsm", StringComparison.OrdinalIgnoreCase))
                    {
                        // Mark this shape for removal
                        shapesToRemove.Add(shape);
                    }
                }

                // Remove the identified OLE shapes from the page
                foreach (Shape shape in shapesToRemove)
                {
                    page.Shapes.Remove(shape);
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}