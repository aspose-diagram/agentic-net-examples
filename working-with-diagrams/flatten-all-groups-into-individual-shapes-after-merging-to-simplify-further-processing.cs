using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_flattened.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect group shapes on the current page
                var groups = new System.Collections.Generic.List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Identify group shapes by their Type
                    if (shape.Type == TypeValue.Group)
                    {
                        groups.Add(shape);
                    }
                }

                // Ungroup each group shape
                foreach (Shape groupShape in groups)
                {
                    // Ungroup expands the group into its constituent shapes
                    // and removes the original group shape from the page.
                    groupShape.Ungroup();
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}