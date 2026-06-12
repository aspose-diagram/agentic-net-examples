using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Use the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Identify a source shape (first non‑connector, non‑deleted shape)
                Shape sourceShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.OneD) continue;                 // Skip connectors
                    if (shp.Del == BOOL.True) continue;     // Skip deleted shapes
                    sourceShape = shp;
                    break;
                }

                if (sourceShape == null)
                {
                    Console.WriteLine("No suitable source shape found.");
                    return;
                }

                // Find the nearest gluing‑enabled shape (excluding the source shape)
                Shape nearestShape = null;
                double minDistance = double.MaxValue;

                foreach (Shape candidate in page.Shapes)
                {
                    if (candidate.ID == sourceShape.ID) continue; // Skip the source itself
                    if (candidate.Del == BOOL.True) continue;    // Skip deleted shapes

                    // Check if the shape allows dynamic glue
                    // GlueTypeValue.AllowDynamicGlue indicates gluing is enabled
                    if (candidate.Misc.GlueType.Value != GlueTypeValue.AllowDynamicGlue)
                        continue;

                    // Compute Euclidean distance between shape centers (PinX, PinY)
                    double dx = sourceShape.XForm.PinX.Value - candidate.XForm.PinX.Value;
                    double dy = sourceShape.XForm.PinY.Value - candidate.XForm.PinY.Value;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestShape = candidate;
                    }
                }

                if (nearestShape == null)
                {
                    Console.WriteLine("No gluing‑enabled shape found to connect.");
                    return;
                }

                // Add a dynamic connector shape to the page
                // Master name for a connector is typically "Dynamic connector"
                long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Connect source shape to the nearest shape using the connector
                // Choose connection points (e.g., Bottom of source, Top of target)
                page.ConnectShapesViaConnector(
                    sourceShape.ID,
                    ConnectionPointPlace.Bottom,
                    nearestShape.ID,
                    ConnectionPointPlace.Top,
                    connectorShape.ID);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Connector added between shape {sourceShape.ID} and shape {nearestShape.ID}.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }