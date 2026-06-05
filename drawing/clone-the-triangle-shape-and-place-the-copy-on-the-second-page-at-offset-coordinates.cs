using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least a second page; create one if necessary
                if (diagram.Pages.Count < 2)
                {
                    // Determine a new unique page ID
                    int maxId = 0;
                    foreach (Page p in diagram.Pages)
                    {
                        if (p.ID > maxId) maxId = p.ID;
                    }

                    Page newPage = new Page();
                    newPage.ID = maxId + 1;
                    newPage.Name = "Page-2";
                    diagram.Pages.Add(newPage);
                }

                // Reference to the first (source) page and second (target) page
                Page sourcePage = diagram.Pages[0];
                Page targetPage = diagram.Pages[1];

                // Locate the triangle shape on the source page
                Shape triangleShape = null;
                foreach (Shape shape in sourcePage.Shapes)
                {
                    // The master name for a triangle shape is typically "Triangle"
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangleShape = shape;
                        break;
                    }
                }

                if (triangleShape == null)
                {
                    Console.WriteLine("Triangle shape not found on the first page.");
                    return;
                }

                // Retrieve geometry of the original triangle
                double originalPinX = triangleShape.XForm.PinX.Value;
                double originalPinY = triangleShape.XForm.PinY.Value;
                double originalWidth = triangleShape.XForm.Width.Value;
                double originalHeight = triangleShape.XForm.Height.Value;

                // Define offset (in inches) for the cloned shape on the second page
                double offsetX = 2.0; // move 2 inches to the right
                double offsetY = 2.0; // move 2 inches up

                double newPinX = originalPinX + offsetX;
                double newPinY = originalPinY + offsetY;

                // Add a new triangle shape on the second page using the same master
                string masterName = triangleShape.Master.Name; // should be "Triangle"
                long newShapeId = targetPage.AddShape(newPinX, newPinY, originalWidth, originalHeight, masterName);

                // Optionally retrieve the newly added shape to copy additional properties
                Shape newShape = targetPage.Shapes.GetShape((int)newShapeId);
                // Example: copy fill color if needed
                newShape.Fill.FillForegnd.Value = triangleShape.Fill.FillForegnd.Value;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Triangle shape cloned and placed on the second page.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }