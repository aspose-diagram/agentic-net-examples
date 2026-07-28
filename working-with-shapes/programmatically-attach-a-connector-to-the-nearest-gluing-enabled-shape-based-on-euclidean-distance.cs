using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find a source shape (first non‑connector shape)
                Shape sourceShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    // Exclude 1‑D connector shapes
                    if (!shp.OneD)
                    {
                        sourceShape = shp;
                        break;
                    }
                }

                if (sourceShape == null)
                {
                    Console.WriteLine("No suitable source shape found.");
                    return;
                }

                // Locate the nearest gluing‑enabled shape (excluding the source)
                Shape nearestShape = null;
                double minDistance = double.MaxValue;

                foreach (Shape candidate in page.Shapes)
                {
                    // Skip the source shape itself
                    if (candidate.ID == sourceShape.ID)
                        continue;

                    // Ensure the shape is not a connector and has gluing enabled
                    if (candidate.OneD)
                        continue;

                    if (candidate.Misc == null || candidate.Misc.GlueType == null)
                        continue;

                    if (candidate.Misc.GlueType.Value != GlueTypeValue.AllowDynamicGlue)
                        continue;

                    // Compute Euclidean distance between shape centers (PinX, PinY)
                    double dx = candidate.XForm.PinX.Value - sourceShape.XForm.PinX.Value;
                    double dy = candidate.XForm.PinY.Value - sourceShape.XForm.PinY.Value;
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

                // Add a dynamic connector shape at the source position
                long connectorId = page.AddShape(
                    sourceShape.XForm.PinX.Value,
                    sourceShape.XForm.PinY.Value,
                    "Dynamic connector");

                // Connect the source shape to the nearest shape using the connector
                // Choose connection points (e.g., Bottom of source, Top of target)
                page.ConnectShapesViaConnector(
                    sourceShape.ID,
                    ConnectionPointPlace.Bottom,
                    nearestShape.ID,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Connector attached between shape {sourceShape.ID} and shape {nearestShape.ID}.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }