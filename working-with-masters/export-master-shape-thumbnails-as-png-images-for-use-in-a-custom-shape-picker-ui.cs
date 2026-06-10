using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioFilePath = "input.vsdx";

                // Output folder for PNG thumbnails
                string outputFolder = "MasterThumbnails";

                // Ensure output directory exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(visioFilePath))
                {
                    // Iterate through each master in the diagram
                    foreach (Master master in diagram.Masters)
                    {
                        // Create a temporary page to render the master shape
                        Page tempPage = new Page(diagram.Pages.Count + 1);
                        diagram.Pages.Add(tempPage);

                        // Add the master shape to the temporary page at position (0,0)
                        long shapeId = tempPage.AddShape(0.0, 0.0, master.Name);
                        Shape shape = tempPage.Shapes.GetShape(shapeId);

                        // Prepare a safe file name for the PNG
                        string safeMasterName = string.Concat(master.Name.Split(Path.GetInvalidFileNameChars()));
                        string outputPath = Path.Combine(outputFolder, $"{safeMasterName}.png");

                        // Export the shape as a PNG image
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        shape.ToImage(outputPath, pngOptions);

                        // Remove the temporary page to keep the diagram clean
                        diagram.Pages.Remove(tempPage);

                        Console.WriteLine($"Exported master '{master.Name}' to '{outputPath}'.");
                    }
                }

                Console.WriteLine("All master thumbnails have been exported.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }