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
                string inputPath = "input.vsdx"; // replace with actual file path
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure there are at least two pages; add a new page if needed
                    if (diagram.Pages.Count < 2)
                    {
                        // Create a new blank page and add it to the diagram
                        Page newPage = new Page();
                        // Assign a unique ID (max existing ID + 1)
                        int maxId = 0;
                        foreach (Page p in diagram.Pages)
                        {
                            if (p.ID > maxId) maxId = p.ID;
                        }
                        newPage.ID = maxId + 1;
                        diagram.Pages.Add(newPage);
                    }

                    // Source page (first page) and target page (second page)
                    Page sourcePage = diagram.Pages[0];
                    Page targetPage = diagram.Pages[1];

                    // Retrieve the first shape on the source page to clone
                    Shape sourceShape = null;
                    foreach (Shape s in sourcePage.Shapes)
                    {
                        // Skip deleted shapes
                        if (s.Del == BOOL.False)
                        {
                            sourceShape = s;
                            break;
                        }
                    }

                    if (sourceShape == null)
                    {
                        Console.WriteLine("No shape found on the source page to clone.");
                        return;
                    }

                    // Get the master name of the source shape (used to create a similar shape)
                    string masterName = sourceShape.Master?.Name;
                    if (string.IsNullOrEmpty(masterName))
                    {
                        Console.WriteLine("Source shape does not have an associated master.");
                        return;
                    }

                    // Determine position for the cloned shape on the target page
                    double clonePinX = sourceShape.XForm.PinX.Value + 2.0; // offset X by 2 inches
                    double clonePinY = sourceShape.XForm.PinY.Value;     // same Y

                    // Add a new shape on the target page using the same master
                    long newShapeId = targetPage.AddShape(clonePinX, clonePinY, masterName);
                    Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                    // Copy all properties from the source shape to the cloned shape
                    clonedShape.Copy(sourceShape);

                    // Modify the width of the cloned shape
                    double newWidth = 2.0; // desired width in inches
                    clonedShape.XForm.Width.Value = newWidth;

                    // Save the modified diagram
                    string outputPath = "output.vsdx"; // replace with desired output path
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }