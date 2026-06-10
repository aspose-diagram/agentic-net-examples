using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class PreserveCustomDataDuringAutoSpace
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Store custom data fields (Data1, Data2, Data3) for each shape by its ID
            var shapeDataMap = new Dictionary<long, (string Data1, string Data2, string Data3)>();
            foreach (Shape shape in page.Shapes)
            {
                shapeDataMap[shape.ID] = (shape.Data1, shape.Data2, shape.Data3);
            }

            // Configure auto‑spacing options (optional: customize distances)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // inches
                DistanceInVertical = 0.5    // inches
            };

            // Perform auto‑spacing on all shapes of the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Refresh shape data after moving them (ensures geometry is updated)
            foreach (Shape shape in page.Shapes)
            {
                shape.RefreshData();
            }

            // Re‑apply stored custom data fields in case they were cleared during spacing
            foreach (Shape shape in page.Shapes)
            {
                if (shapeDataMap.TryGetValue(shape.ID, out var data))
                {
                    shape.Data1 = data.Data1;
                    shape.Data2 = data.Data2;
                    shape.Data3 = data.Data3;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
