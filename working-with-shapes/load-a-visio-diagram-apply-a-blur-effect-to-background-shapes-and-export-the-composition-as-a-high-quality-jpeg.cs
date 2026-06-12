using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output JPEG file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioBlurExport <inputVisioPath> <outputJpegPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Process only background pages
                    if (page.Background == BOOL.True)
                    {
                        // Iterate through all shapes on the background page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply blur only to foreign (image) shapes that have an Image object
                            if (shape.Type == TypeValue.Foreign && shape.Image != null)
                            {
                                // Set blur amount (value between 0.0 and 1.0, where 0 = no blur)
                                shape.Image.Blur.Value = 0.5; // Adjust as needed for desired effect

                                // Ensure the shape stays behind other content
                                shape.SendToBack();
                            }
                        }
                    }
                }

                // Configure high‑quality JPEG export options
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    // 300 DPI is a common high‑resolution setting
                    Resolution = 300,
                    // Do not export hidden pages unless explicitly needed
                    ExportHiddenPage = false
                };

                // Save the modified diagram as a JPEG image
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }