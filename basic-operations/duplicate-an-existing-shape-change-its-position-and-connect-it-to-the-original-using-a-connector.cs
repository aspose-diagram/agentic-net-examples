using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (replace with actual paths)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page to duplicate
                Shape originalShape = null;
                foreach (Shape s in page.Shapes)
                {
                    originalShape = s;
                    break;
                }

                if (originalShape == null)
                {
                    Console.WriteLine("No shape found on the page to duplicate.");
                    return;
                }

                // Store original shape ID for later connection
                long originalShapeId = originalShape.ID;

                // Ensure the shape has a master (required for duplication)
                if (originalShape.Master == null)
                {
                    Console.WriteLine("The original shape does not have an associated master.");
                    return;
                }

                // Duplicate the shape by adding a new shape with the same master, size, and offset position
                double offsetX = 2.0; // shift 2 inches to the right
                double newPinX = originalShape.XForm.PinX.Value + offsetX;
                double newPinY = originalShape.XForm.PinY.Value; // same vertical position
                double width = originalShape.XForm.Width.Value;
                double height = originalShape.XForm.Height.Value;
                string masterName = originalShape.Master.Name;

                long duplicatedShapeId = page.AddShape(newPinX, newPinY, width, height, masterName, false);
                Shape duplicatedShape = page.Shapes.GetShape(duplicatedShapeId);

                // Copy the text from the original shape to the duplicated shape
                duplicatedShape.Text.Value.Clear();
                foreach (var item in originalShape.Text.Value)
                {
                    if (item is Txt txt)
                    {
                        duplicatedShape.Text.Value.Add(new Txt(txt.Text));
                    }
                }

                // Optionally copy fill and line formatting (example for fill foreground color)
                duplicatedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                duplicatedShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;
                duplicatedShape.Line.LineWeight.Value = originalShape.Line.LineWeight.Value;

                // Add a dynamic connector shape (connector)
                // The master name for a dynamic connector is "Dynamic connector"
                long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // Set connector routing style (e.g., right-angle)
                connector.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;

                // Connect the original shape to the duplicated shape using the connector
                // Connect from the bottom of the original shape to the top of the duplicated shape
                page.ConnectShapesViaConnector(
                    originalShapeId,
                    ConnectionPointPlace.Bottom,
                    duplicatedShapeId,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Shape duplicated, repositioned, and connected successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }