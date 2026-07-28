using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Ensure there is at least one page to copy from
                    if (diagram.Pages.Count == 0)
                    {
                        Console.WriteLine("The diagram contains no pages.");
                        return;
                    }

                    // Source page (first page)
                    Page sourcePage = diagram.Pages[0];

                    // Find the first shape on the source page to clone
                    Shape sourceShape = null;
                    foreach (Shape shp in sourcePage.Shapes)
                    {
                        sourceShape = shp;
                        break;
                    }

                    if (sourceShape == null)
                    {
                        Console.WriteLine("No shape found on the source page.");
                        return;
                    }

                    // Determine the master name of the source shape (used for creating a similar shape)
                    string masterName = sourceShape.Master != null ? sourceShape.Master.Name : "Rectangle";

                    // Get or create the target page (second page)
                    Page targetPage;
                    if (diagram.Pages.Count > 1)
                    {
                        targetPage = diagram.Pages[1];
                    }
                    else
                    {
                        // Create a new page with a unique ID
                        int maxPageId = diagram.Pages.Max(p => p.ID);
                        targetPage = new Page
                        {
                            ID = maxPageId + 1,
                            Name = "ClonedPage"
                        };
                        diagram.Pages.Add(targetPage);
                    }

                    // Add a new shape on the target page using the same master.
                    // Position it slightly offset from the original shape.
                    double offsetX = 2.0; // inches offset on X axis
                    double newPinX = sourceShape.XForm.PinX.Value + offsetX;
                    double newPinY = sourceShape.XForm.PinY.Value; // same Y position

                    long newShapeId = targetPage.AddShape(newPinX, newPinY, masterName);
                    Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                    // Apply a different preset theme to the cloned shape
                    clonedShape.PresetTheme = PresetThemeValue.Bubble;
                    clonedShape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                    clonedShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

                    // Save the modified diagram
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Shape cloned and themed. Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }