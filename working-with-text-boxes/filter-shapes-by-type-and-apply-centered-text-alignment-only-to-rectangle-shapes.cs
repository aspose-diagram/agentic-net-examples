using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only rectangle shapes identified by master name
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Ensure the shape contains at least one paragraph for text alignment
                        if (shape.Paras != null && shape.Paras.Count > 0)
                        {
                            // Apply centered horizontal alignment to the first paragraph
                            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                        }
                    }
                }
            }

            // Path for the modified Visio file
            string outputPath = "output.vsdx";
            // Save the updated diagram using the Vsdx format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}