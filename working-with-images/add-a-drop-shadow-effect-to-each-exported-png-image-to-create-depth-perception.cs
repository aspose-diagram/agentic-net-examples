using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Apply a simple drop shadow to every non-deleted shape in the diagram
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Enable simple shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";

                        // Shadow transparency (30% transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;

                        // Shadow offset (adjust as needed)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1; // inches
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1; // inches
                    }
                }

                // Export each page as a PNG image with the applied shadows
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    string outputPath = $"Page_{i + 1}.png";

                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        PageIndex = i // Export the specific page
                    };

                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Exported page {i + 1} to {outputPath}");
                }

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }