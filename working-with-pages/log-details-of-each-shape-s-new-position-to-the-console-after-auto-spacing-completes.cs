using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from a file stream
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                Diagram diagram = new Diagram(stream);

                // Iterate through each page in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Configure auto‑spacing options
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    options.DistanceInHorizontal = 2.0; // horizontal spacing in inches
                    options.DistanceInVertical = 2.0;   // vertical spacing in inches

                    // Apply auto‑spacing to all shapes on the current page
                    page.AutoSpaceShapes(page.Shapes, options);

                    // Log the new position of each shape after spacing
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, New Position -> PinX: {pinX}, PinY: {pinY}");
                    }
                }

                // Save the modified diagram (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
