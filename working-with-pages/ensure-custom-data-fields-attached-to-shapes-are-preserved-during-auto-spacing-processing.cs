using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class PreserveCustomDataDuringAutoSpace
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output Visio file paths
            string inputPath = @"C:\Diagrams\input.vsdx";
            string outputPath = @"C:\Diagrams\output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Store custom data fields (Data1, Data2, Data3) for each shape by its ID
            var customDataMap = new Dictionary<long, (string Data1, string Data2, string Data3)>();
            foreach (Shape shape in page.Shapes)
            {
                // Only store for shapes that have custom data (non‑null)
                customDataMap[shape.ID] = (shape.Data1, shape.Data2, shape.Data3);
            }

            // Prepare auto‑spacing options (example distances)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // inches
                DistanceInVertical = 0.5    // inches
            };

            // Perform auto‑spacing on all shapes of the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Re‑apply the previously stored custom data fields
            foreach (Shape shape in page.Shapes)
            {
                if (customDataMap.TryGetValue(shape.ID, out var data))
                {
                    shape.Data1 = data.Data1;
                    shape.Data2 = data.Data2;
                    shape.Data3 = data.Data3;
                }

                // Refresh shape data to ensure position changes are reflected
                shape.RefreshData();
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
