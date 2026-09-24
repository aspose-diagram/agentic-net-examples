using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Height threshold (in inches) and the new height to apply
            double heightThreshold = 2.0;
            double newHeight = 3.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.False)
                    {
                        double currentHeight = shape.XForm.Height.Value;

                        // Apply new height only if current height is less than the threshold
                        if (currentHeight < heightThreshold)
                        {
                            shape.XForm.Height.Value = newHeight;
                            Console.WriteLine($"Shape ID {shape.ID} height changed from {currentHeight} to {newHeight}");
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
