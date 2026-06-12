using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PNG file path
                string outputPath = "output.png";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.False)
                            {
                                // Set fill foreground and background transparency to 20%
                                // (80% opacity = 20% transparency)
                                shape.Fill.FillForegndTrans.Value = 20;
                                shape.Fill.FillBkgndTrans.Value = 20;
                            }
                        }
                    }

                    // Configure PNG export options
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Export the diagram to PNG
                    diagram.Save(outputPath, pngOptions);
                }

                Console.WriteLine("Diagram processed and saved as PNG successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }