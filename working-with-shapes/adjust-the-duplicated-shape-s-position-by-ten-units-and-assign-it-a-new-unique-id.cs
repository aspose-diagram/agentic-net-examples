using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Assume we duplicate the first shape on the page
                // Retrieve the original shape
                Shape originalShape = page.Shapes[0];

                // Extract necessary properties from the original shape
                string masterName = originalShape.Master.Name;                     // Master name to reuse
                double width = originalShape.XForm.Width.Value;                    // Width in inches
                double height = originalShape.XForm.Height.Value;                  // Height in inches
                double originalPinX = originalShape.XForm.PinX.Value;              // Original X position
                double originalPinY = originalShape.XForm.PinY.Value;              // Original Y position

                // Calculate new position (offset by 10 units)
                double newPinX = originalPinX + 10.0;
                double newPinY = originalPinY + 10.0;

                // Add a new shape using the same master and dimensions at the new position
                long newShapeId = page.AddShape(newPinX, newPinY, width, height, masterName);

                // Retrieve the newly added shape instance
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Ensure the new shape has a unique ID (greater than any existing ID)
                long maxId = 0;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.ID > maxId)
                        maxId = shp.ID;
                }
                newShape.ID = maxId + 1; // Assign a new unique identifier

                // Optionally, copy other properties from the original shape (e.g., text)
                // Clear any existing text and copy the original text
                newShape.Text.Value.Clear();
                foreach (var txtItem in originalShape.Text.Value)
                {
                    if (txtItem is Txt txt)
                    {
                        newShape.Text.Value.Add(new Txt(txt.Text));
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }