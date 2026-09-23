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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output XPS file path
            string outputPath = "output.xps";

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply auto-space to each page
                foreach (Page page in diagram.Pages)
                {
                    AutoSpaceOptions autoSpace = new AutoSpaceOptions();
                    autoSpace.DistanceInHorizontal = 2; // horizontal spacing
                    autoSpace.DistanceInVertical = 2;   // vertical spacing

                    // Auto-space the shapes on the page
                    page.AutoSpaceShapes(page.Shapes, autoSpace);
                }

                // Configure XPS save options
                XPSSaveOptions xpsOptions = new XPSSaveOptions();
                xpsOptions.ExportHiddenPage = false;

                // Save the diagram as XPS
                diagram.Save(outputPath, xpsOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
