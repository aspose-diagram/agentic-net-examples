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

            // Uniform gray color in hex format
            string grayHex = "#808080";

            // Iterate over all pages and shapes to locate gradient fills
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape uses a gradient fill and that the gradient is enabled
                    if (shape.Fill != null && shape.Fill.FillPattern != null &&
                        shape.Fill.FillPattern.Value == 25 && // 25 = gradient fill pattern
                        shape.Fill.GradientFill != null &&
                        shape.Fill.GradientFill.GradientEnabled != null &&
                        shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // Update each gradient stop to the uniform gray color while keeping its position
                        foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                        {
                            // Assign the hex string directly to the Color cell's Value
                            stop.Color.Value = grayHex;
                        }
                    }
                }
            }

            // Path for the modified Visio file
            string outputPath = "output.vsdx";
            // Save the updated diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}