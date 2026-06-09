using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply a simple drop shadow to every shape in every page
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Enable simple shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";

                        // Shadow transparency (30% transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;

                        // Shadow offset (0.1 inch right and down)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }

                // Export each page as a PNG image with the applied shadows
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for PNG
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Render only the current page
                        PageIndex = i
                    };

                    // Output file name per page
                    string outputPath = $"Page_{i + 1}.png";

                    // Save the diagram (only the specified page) as PNG
                    diagram.Save(outputPath, saveOptions);
                }

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Export completed with drop shadows applied.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }