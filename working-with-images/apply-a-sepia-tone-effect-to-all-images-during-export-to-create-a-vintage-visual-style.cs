using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image (foreign) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Apply a sepia‑like effect by adjusting image properties
                            // Increase brightness slightly
                            shape.Image.Brightness.Value = 0.2;
                            // Reduce contrast to soften the image
                            shape.Image.Contrast.Value = 0.5;
                            // Adjust gamma to give a warm tone
                            shape.Image.Gamma.Value = 0.8;
                        }
                    }
                }

                // Configure image export options (PNG format)
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Optional: set resolution if needed
                saveOptions.Resolution = 300f;

                // Export the diagram with the applied sepia effect
                string outputPath = "output.png";
                diagram.Save(outputPath, saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }