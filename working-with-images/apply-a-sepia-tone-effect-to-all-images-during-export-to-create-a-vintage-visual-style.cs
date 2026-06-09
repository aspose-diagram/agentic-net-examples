using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image shapes (foreign objects)
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Apply sepia-like adjustments using image properties
                            // Increase brightness slightly
                            shape.Image.Brightness.Value = 0.1;
                            // Increase contrast to enhance tones
                            shape.Image.Contrast.Value = 0.2;
                            // Adjust gamma to give a warm tone
                            shape.Image.Gamma.Value = 0.9;
                            // Optionally reduce transparency (make fully opaque)
                            shape.Image.Transparency.Value = 0;
                        }
                    }
                }

                // Export the modified diagram to a PNG image with the applied effects
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                string outputPath = "output.png";
                diagram.Save(outputPath, saveOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }