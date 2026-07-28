using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the resulting Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Locate the first hexagon shape on the page
                Shape hexagon = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Hexagon")
                    {
                        hexagon = shape;
                        break;
                    }
                }

                if (hexagon == null)
                {
                    Console.WriteLine("Hexagon shape not found on the page.");
                    return;
                }

                // Define offset values (in inches)
                double offsetX = 2.0; // move 2 inches to the right
                double offsetY = 1.0; // move 1 inch up

                // Calculate new position based on original PinX/PinY
                double newPinX = hexagon.XForm.PinX.Value + offsetX;
                double newPinY = hexagon.XForm.PinY.Value + offsetY;

                // Add a new shape using the same master (hexagon) at the new location
                long newShapeId = page.AddShape(newPinX, newPinY, hexagon.Master.Name);
                Shape newHexagon = page.Shapes.GetShape(newShapeId);

                // Optional: copy the text from the original shape to the new one
                if (hexagon.Text != null && hexagon.Text.Value != null)
                {
                    newHexagon.Text.Value.Clear();
                    foreach (var item in hexagon.Text.Value)
                    {
                        if (item is Txt txt)
                        {
                            newHexagon.Text.Value.Add(new Txt(txt.Text));
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Hexagon shape copied successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }