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

            // Select the page to modify (e.g., the first page)
            Page page = diagram.Pages[0];

            // Define the scaling factor (example: 1.5 means 150% scaling)
            double scalingFactor = 1.5;

            // Apply scaling to each shape on the selected page
            foreach (Shape shape in page.Shapes)
            {
                // Scale position
                shape.XForm.PinX.Value *= scalingFactor;
                shape.XForm.PinY.Value *= scalingFactor;

                // Scale size
                shape.XForm.Width.Value *= scalingFactor;
                shape.XForm.Height.Value *= scalingFactor;
            }

            // Save the modified diagram
            string outputPath = "output_scaled.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
