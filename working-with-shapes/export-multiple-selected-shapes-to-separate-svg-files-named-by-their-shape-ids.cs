using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportSelectedShapesToSvg
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsd");

            // List of shape IDs that should be exported
            List<long> selectedShapeIds = new List<long> { 1, 5, 9 }; // replace with actual IDs

            // Optional: configure SVG save options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                IsSavingImageSeparately = true // example option, adjust as needed
            };

            // Iterate through each page and each shape on the page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Export only the shapes whose IDs are in the selected list
                    if (selectedShapeIds.Contains(shape.ID))
                    {
                        // Create a file name based on the shape ID
                        string fileName = $"shape_{shape.ID}.svg";

                        // Save the shape as an SVG file
                        shape.ToSvg(fileName, svgOptions);
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
