using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Paths – adjust as needed
            string inputVisioPath = "input.vsdx";
            string outputPngPath = "output.png";
            string logoImagePath = "logo.png";

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputVisioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify placeholder shapes by checking their text content
                    string shapeText = shape.Text.Value.Text;
                    if (string.IsNullOrWhiteSpace(shapeText))
                        continue;

                    if (shapeText.IndexOf("PLACEHOLDER", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Preserve position and size of the placeholder
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Hide the placeholder shape
                        shape.Del = BOOL.True;

                        // Insert the logo image at the same location
                        try
                        {
                            using (FileStream imgStream = new FileStream(logoImagePath, FileMode.Open, FileAccess.Read))
                            {
                                // AddShape overload that accepts an image stream creates a foreign (image) shape
                                long logoShapeId = page.AddShape(pinX, pinY, width, height, imgStream);
                                // Optional: retrieve the newly added shape if further adjustments are needed
                                // Shape logoShape = page.Shapes.GetShape(logoShapeId);
                            }
                        }
                        catch (Exception imgEx)
                        {
                            Console.WriteLine($"Failed to insert logo for shape ID {shape.ID}: {imgEx.Message}");
                        }
                    }
                }
            }

            // Configure high‑resolution PNG export
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // 300 DPI is a common high‑resolution setting
                Resolution = 300f,
                // Export all pages; set to a specific page index if needed
                // PageIndex = 0
            };

            // Save the modified diagram as PNG
            try
            {
                diagram.Save(outputPngPath, pngOptions);
                Console.WriteLine($"Diagram saved successfully to '{outputPngPath}'.");
            }
            catch (Exception saveEx)
            {
                Console.WriteLine($"Failed to save diagram: {saveEx.Message}");
            }
        }
    }