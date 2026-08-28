using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the actual path to your diagram file
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // -------------------------------------------------
                    // 1. Locate the first triangle shape on the first page
                    // -------------------------------------------------
                    Page sourcePage = diagram.Pages[0];
                    Shape triangleShape = null;

                    foreach (Shape shape in sourcePage.Shapes)
                    {
                        // Ensure the shape has a master and that the master name is "Triangle"
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

                    // -------------------------------------------------
                    // 2. Retrieve original shape geometry and master name
                    // -------------------------------------------------
                    string masterName = triangleShape.Master.Name;
                    double originalPinX = triangleShape.XForm.PinX.Value;
                    double originalPinY = triangleShape.XForm.PinY.Value;
                    double originalWidth = triangleShape.XForm.Width.Value;
                    double originalHeight = triangleShape.XForm.Height.Value;

                    // -------------------------------------------------
                    // 3. Ensure a second page exists; create if necessary
                    // -------------------------------------------------
                    Page targetPage;
                    if (diagram.Pages.Count >= 2)
                    {
                        targetPage = diagram.Pages[1];
                    }
                    else
                    {
                        // Determine the maximum existing page ID
                        int maxPageId = 0;
                        foreach (Page p in diagram.Pages)
                        {
                            if (p.ID > maxPageId)
                                maxPageId = p.ID;
                        }

                        // Create a new page with a unique ID
                        targetPage = new Page(maxPageId + 1);
                        targetPage.Name = "Page-2";
                        diagram.Pages.Add(targetPage);
                    }

                    // -------------------------------------------------
                    // 4. Define offset coordinates for the cloned shape
                    // -------------------------------------------------
                    const double offsetX = 2.0; // inches
                    const double offsetY = 2.0; // inches
                    double newPinX = originalPinX + offsetX;
                    double newPinY = originalPinY + offsetY;

                    // -------------------------------------------------
                    // 5. Add the cloned triangle shape to the second page
                    // -------------------------------------------------
                    long newShapeId = targetPage.AddShape(newPinX, newPinY, originalWidth, originalHeight, masterName);
                    Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                    // Optional: copy text from the original shape to the cloned shape
                    if (!string.IsNullOrWhiteSpace(triangleShape.Text.Value.ToString()))
                    {
                        clonedShape.Text.Value.Clear();
                        clonedShape.Text.Value.Add(new Txt(triangleShape.Text.Value.ToString()));
                    }

                    // -------------------------------------------------
                    // 6. Save the modified diagram
                    // -------------------------------------------------
                    // Replace "output.vsdx" with the desired output path
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