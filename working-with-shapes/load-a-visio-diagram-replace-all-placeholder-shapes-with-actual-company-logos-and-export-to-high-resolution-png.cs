using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string inputVisioPath = "input.vsdx";
                string outputPngPath = "output.png";
                string logoImagePath = "logo.png";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputVisioPath);

                // Iterate through all pages and shapes to find placeholders
                foreach (Page page in diagram.Pages)
                {
                    // Collect placeholder shape IDs first to avoid modifying collection during iteration
                    var placeholderIds = new System.Collections.Generic.List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Get plain text of the shape
                        string shapeText = shape.Text.Value.Text;

                        // Identify placeholder shapes (e.g., containing "[Logo]")
                        if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("[Logo]"))
                        {
                            placeholderIds.Add(shape.ID);
                        }
                    }

                    // Replace each placeholder with the actual logo image
                    foreach (long placeholderId in placeholderIds)
                    {
                        // Retrieve the placeholder shape
                        Shape placeholder = page.Shapes.GetShape(placeholderId);

                        // Preserve position and size
                        double pinX = placeholder.XForm.PinX.Value;
                        double pinY = placeholder.XForm.PinY.Value;
                        double width = placeholder.XForm.Width.Value;
                        double height = placeholder.XForm.Height.Value;

                        // Hide the placeholder shape
                        placeholder.Del = BOOL.True;

                        // Insert the logo image at the same location and size
                        using (FileStream logoStream = new FileStream(logoImagePath, FileMode.Open, FileAccess.Read))
                        {
                            // AddShape overload that accepts an image stream
                            page.AddShape(pinX, pinY, width, height, logoStream);
                        }
                    }
                }

                // Configure high‑resolution PNG export (e.g., 300 DPI)
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.Resolution = 300f; // DPI

                // Save the modified diagram as a PNG image
                diagram.Save(outputPngPath, pngOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }