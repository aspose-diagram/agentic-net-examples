using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Ensure there are at least two pages; add a second page if missing
                    if (diagram.Pages.Count < 2)
                    {
                        // Add a new blank page
                        diagram.Pages.Add(new Page());
                    }

                    // Get references to the first (source) and second (target) pages
                    Page sourcePage = diagram.Pages[0];
                    Page targetPage = diagram.Pages[1];

                    // Locate the first triangle shape on the source page
                    Shape triangleShape = null;
                    foreach (Shape shape in sourcePage.Shapes)
                    {
                        // Identify triangle by its master name (Visio uses "Triangle" for the basic triangle shape)
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            triangleShape = shape;
                            break;
                        }
                    }

                    if (triangleShape == null)
                    {
                        Console.WriteLine("No triangle shape found on the first page.");
                        return;
                    }

                    // Retrieve geometry and master information from the original triangle
                    string masterName = triangleShape.Master.Name;
                    double width = triangleShape.XForm.Width.Value;
                    double height = triangleShape.XForm.Height.Value;

                    // Original position
                    double originalPinX = triangleShape.XForm.PinX.Value;
                    double originalPinY = triangleShape.XForm.PinY.Value;

                    // Define offset (e.g., shift 2 inches right and 1 inch up)
                    double offsetX = 2.0;
                    double offsetY = 1.0;

                    double newPinX = originalPinX + offsetX;
                    double newPinY = originalPinY + offsetY;

                    // Add a new shape on the second page using the same master and size, positioned with the offset
                    long newShapeId = targetPage.AddShape(newPinX, newPinY, width, height, masterName);
                    Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                    // Copy visual properties (fill, line, text, etc.) from the original triangle to the new shape
                    // The Copy method copies the shape's formatting and text content
                    newShape.Copy(triangleShape);

                    // Save the modified diagram to a new file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                    Console.WriteLine("Triangle shape cloned and placed on the second page successfully.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }