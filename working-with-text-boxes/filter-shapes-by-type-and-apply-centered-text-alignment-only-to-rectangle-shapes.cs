using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: check input file exists
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
                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Filter: process only rectangle shapes (identified by master name)
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Apply centered text alignment
                        // Vertical centering
                        shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                        // Horizontal centering: Aspose.Diagram does not expose a direct property,
                        // but setting the text block's alignment to center is typically handled
                        // via the shape's style or by default alignment. This placeholder
                        // demonstrates where such logic would be applied if available.
                        // Example (if supported):
                        // shape.TextBlock.HorizontalAlign.Value = HorizontalAlignValue.Center;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}