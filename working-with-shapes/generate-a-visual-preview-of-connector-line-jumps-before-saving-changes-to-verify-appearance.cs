using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Example: set the connector to use an Arc jump style
                            shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                            // Example: set the jump code to "Always" (jump always appears)
                            shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Always;
                        }
                    }
                }

                // Export a visual preview of the diagram (including the updated connector jumps) to PNG
                string previewPath = "preview.png";
                ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(previewPath, previewOptions);
                Console.WriteLine($"Preview image saved to: {previewPath}");

                // After verifying the preview, save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }