using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (as an example)
                Shape originalShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    originalShape = shp;
                    break;
                }

                if (originalShape == null)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Get original position
                double originalPinX = originalShape.XForm.PinX.Value;
                double originalPinY = originalShape.XForm.PinY.Value;

                // Calculate new position (move 10 units on the X axis)
                double newPinX = originalPinX + 10.0;
                double newPinY = originalPinY;

                // Duplicate the shape by adding a new shape with the same master
                // The AddShape method returns a unique shape ID
                long newShapeId = page.AddShape(newPinX, newPinY, originalShape.Master.Name);

                // Retrieve the newly created shape
                Shape duplicatedShape = page.Shapes.GetShape(newShapeId);

                // Optional: copy the text from the original shape to the duplicated one
                if (!string.IsNullOrWhiteSpace(originalShape.Text.Value.Text))
                {
                    duplicatedShape.Text.Value.Clear();
                    duplicatedShape.Text.Value.Add(new Txt(originalShape.Text.Value.Text));
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine($"Original shape ID: {originalShape.ID}");
                Console.WriteLine($"Duplicated shape ID: {duplicatedShape.ID}");
                Console.WriteLine("Duplication complete. Diagram saved as output.vsdx.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }