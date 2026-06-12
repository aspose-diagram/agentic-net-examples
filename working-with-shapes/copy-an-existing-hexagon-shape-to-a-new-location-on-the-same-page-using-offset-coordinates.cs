using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Offset values (in inches) for the new shape location
                double offsetX = 2.0;
                double offsetY = 1.0;

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Access the first page (adjust index if needed)
                    Page page = diagram.Pages[0];

                    // Locate the first hexagon shape on the page
                    Shape hexShape = null;
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == "Hexagon")
                        {
                            hexShape = shape;
                            break;
                        }
                    }

                    if (hexShape == null)
                    {
                        Console.WriteLine("Hexagon shape not found on the page.");
                        return;
                    }

                    // Calculate new position using the offset
                    double newPinX = hexShape.XForm.PinX.Value + offsetX;
                    double newPinY = hexShape.XForm.PinY.Value + offsetY;

                    // Add a new shape with the same master (hexagon) at the new location
                    long newShapeId = page.AddShape(newPinX, newPinY, hexShape.Master.Name);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy text content from the original shape to the new shape
                    newShape.Text.Value.Clear();
                    foreach (var item in hexShape.Text.Value)
                    {
                        if (item is Txt txt)
                        {
                            newShape.Text.Value.Add(new Txt(txt.Text));
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Hexagon shape copied to new location and saved as '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }