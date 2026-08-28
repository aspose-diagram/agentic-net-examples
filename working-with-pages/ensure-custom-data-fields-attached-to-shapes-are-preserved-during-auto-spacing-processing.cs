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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page; adjust as needed
            Page page = diagram.Pages[0];

            // Store custom data fields (Data1, Data2, Data3) for each shape keyed by shape ID
            var customData = new Dictionary<long, (string Data1, string Data2, string Data3)>();
            foreach (Shape shape in page.Shapes)
            {
                customData[shape.ID] = (shape.Data1, shape.Data2, shape.Data3);
            }

            // Configure autospace options (example distances in inches)
            var options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // horizontal spacing
                DistanceInVertical = 0.5    // vertical spacing
            };

            // Perform auto‑spacing on all shapes of the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Re‑apply the stored custom data fields after auto‑spacing
            foreach (Shape shape in page.Shapes)
            {
                if (customData.TryGetValue(shape.ID, out var data))
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
