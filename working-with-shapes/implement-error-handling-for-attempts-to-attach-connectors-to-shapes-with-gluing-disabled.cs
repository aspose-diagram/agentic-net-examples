using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to a stencil that contains the required masters (e.g., "Rectangle" and "Dynamic connector").
                // Replace with an actual .vss file path when running the code.
                string stencilPath = "basic.vss";

                // Load the stencil as a diagram (the stencil itself acts as a diagram containing masters).
                Diagram diagram = new Diagram(stencilPath);

                // Use the first page (created automatically) for shape operations.
                Page page = diagram.Pages[0];

                // Add a rectangle shape.
                long rectShapeId = page.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", false);
                Shape rectShape = page.Shapes.GetShape(rectShapeId);

                // Add a second rectangle shape.
                long rectShape2Id = page.AddShape(6.0, 2.0, 2.0, 1.0, "Rectangle", false);
                Shape rectShape2 = page.Shapes.GetShape(rectShape2Id);

                // Add a dynamic connector shape (1‑D connector).
                long connectorId = page.AddShape(0.0, 0.0, 0.0, 0.0, "Dynamic connector", false);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Verify that both endpoint shapes allow dynamic gluing.
                // GlueTypeValue.AllowDynamicGlue means gluing is enabled.
                // GlueTypeValue.NoAllowDynamicGlue means gluing is disabled.
                bool rectCanGlue = rectShape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;
                bool rect2CanGlue = rectShape2.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;

                if (!rectCanGlue || !rect2CanGlue)
                {
                    // Provide detailed error information.
                    if (!rectCanGlue)
                    {
                        Console.WriteLine($"Error: Shape ID {rectShapeId} ('{rectShape.Master?.Name}') has gluing disabled.");
                    }
                    if (!rect2CanGlue)
                    {
                        Console.WriteLine($"Error: Shape ID {rectShape2Id} ('{rectShape2.Master?.Name}') has gluing disabled.");
                    }
                    Console.WriteLine("Connector attachment aborted due to disabled gluing on one or more shapes.");
                    // Optionally, you could throw an exception to halt execution.
                    // throw new InvalidOperationException("Cannot attach connector because gluing is disabled.");
                }
                else
                {
                    // Both shapes allow gluing – proceed with connector attachment.
                    // Connect the first rectangle's right side to the second rectangle's left side.
                    page.ConnectShapesViaConnector(
                        rectShapeId,
                        ConnectionPointPlace.Right,
                        rectShape2Id,
                        ConnectionPointPlace.Left,
                        connectorId);

                    Console.WriteLine("Connector successfully attached between the two rectangles.");
                }

                // Save the diagram to a VSDX file.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }