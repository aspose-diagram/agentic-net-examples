using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "output.vsdx";

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

            // Flag to indicate whether a shape was modified
            bool shapeModified = false;

            // Iterate through pages and shapes to find a non-deleted shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Example: rotate the shape 45 degrees (optional)
                    shape.XForm.Angle.Value = 45.0;

                    // Set KeepTextFlat to true to keep text horizontal despite rotation
                    shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

                    shapeModified = true;
                    // Break after first shape modification; remove if you want to modify all shapes
                    break;
                }

                if (shapeModified)
                    break;
            }

            if (!shapeModified)
            {
                Console.Error.WriteLine("No suitable shape found to modify.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}