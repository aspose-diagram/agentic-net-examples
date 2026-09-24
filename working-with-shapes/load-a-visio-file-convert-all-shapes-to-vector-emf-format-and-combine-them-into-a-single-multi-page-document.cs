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

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file that will contain EMF images on separate pages
                string outputPath = "combined.vsdx";

                // Temporary folder to store individual EMF files
                string tempFolder = Path.Combine(Path.GetTempPath(), "ShapeEmfExport");
                Directory.CreateDirectory(tempFolder);

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(inputPath);

                // Create an empty target diagram
                Diagram targetDiagram = new Diagram();

                // Iterate through all pages and shapes in the source diagram
                foreach (Page srcPage in sourceDiagram.Pages)
                {
                    foreach (Shape srcShape in srcPage.Shapes)
                    {
                        // Export the current shape to an EMF file
                        string emfFile = Path.Combine(tempFolder, $"shape_{srcShape.ID}.emf");
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                        srcShape.ToImage(emfFile, imgOptions);

                        // Add a new page to the target diagram for this shape
                        Page newPage = new Page();
                        targetDiagram.Pages.Add(newPage);

                        // Insert the EMF image into the new page
                        // The AddShape overload that accepts a FileStream is used for image insertion
                        using (FileStream fs = new FileStream(emfFile, FileMode.Open, FileAccess.Read))
                        {
                            // Position the image at the origin (0,0) with default size (0,0 means original size)
                            // The fourth parameter 'isCalculate' is set to false as required by the API
                            long imageShapeId = newPage.AddShape(0, 0, 0, 0, fs);
                            // Retrieve the shape if further manipulation is needed (optional)
                            Shape imageShape = newPage.Shapes.GetShape(imageShapeId);
                            // Example: set the shape name to identify it later
                            imageShape.Name = $"EMF_Shape_{srcShape.ID}";
                        }
                    }
                }

                // Save the combined diagram containing EMF images on separate pages
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up temporary EMF files
                Directory.Delete(tempFolder, true);

                Console.WriteLine("Conversion completed. Combined diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }