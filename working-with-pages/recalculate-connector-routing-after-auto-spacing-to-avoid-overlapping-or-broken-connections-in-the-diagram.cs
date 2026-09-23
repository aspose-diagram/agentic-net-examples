using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options
            AutoSpaceOptions autoSpaceOpts = new AutoSpaceOptions();
            autoSpaceOpts.DistanceInHorizontal = 1.0; // inches
            autoSpaceOpts.DistanceInVertical = 1.0;   // inches

            // Apply auto‑spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, autoSpaceOpts);

            // Re‑calculate routing for all connector shapes
            foreach (Shape shape in page.Shapes)
            {
                // 1‑D shapes are connectors
                if (shape.OneD)
                {
                    // Set routing style to right‑angle (or StraightLines/CurvedLines as needed)
                    shape.SetConnectorsType(ConnectorsTypeValue.RightAngle);
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
