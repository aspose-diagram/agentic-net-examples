using System;
using System.Collections.Generic;
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

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram;
                using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    diagram = new Diagram(fs);
                }

                // Assume processing on the first page
                Page page = diagram.Pages[0];

                // Store custom data fields (Data1, Data2, Data3) for each shape
                var shapeDataMap = new Dictionary<long, (string Data1, string Data2, string Data3)>();
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    shapeDataMap[shape.ID] = (shape.Data1, shape.Data2, shape.Data3);
                }

                // Configure auto‑spacing options
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 2.0, // inches
                    DistanceInVertical = 2.0    // inches
                };

                // Perform auto‑spacing on all shapes of the page
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Re‑apply the stored custom data fields after spacing
                foreach (var kvp in shapeDataMap)
                {
                    long shapeId = kvp.Key;
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        shape.Data1 = kvp.Value.Data1;
                        shape.Data2 = kvp.Value.Data2;
                        shape.Data3 = kvp.Value.Data3;
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }