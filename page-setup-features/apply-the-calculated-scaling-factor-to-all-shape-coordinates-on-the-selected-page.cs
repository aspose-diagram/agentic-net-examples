using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the scaled output file
            string outputPath = "output_scaled.vsdx";

            // Define the scaling factor to apply to shape coordinates
            double scalingFactor = 1.5; // Example: increase size by 150%

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Select the page to process (e.g., the first page)
                // You can also retrieve by name: diagram.Pages.GetPage("Page-1");
                Page page = diagram.Pages[0];

                // Iterate over all shapes on the selected page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Scale position (PinX, PinY)
                    shape.XForm.PinX.Value *= scalingFactor;
                    shape.XForm.PinY.Value *= scalingFactor;

                    // Scale size (Width, Height)
                    shape.XForm.Width.Value *= scalingFactor;
                    shape.XForm.Height.Value *= scalingFactor;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Scaling applied and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
