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

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path (multi‑page document)
                string outputPath = "output.vsdx";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(inputPath);

                // Create an empty target diagram
                Diagram targetDiagram = new Diagram();

                // Iterate through each page in the source diagram
                foreach (Page srcPage in sourceDiagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape srcShape in srcPage.Shapes)
                    {
                        // Export the shape to a temporary EMF file
                        string tempEmfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".emf");
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                        srcShape.ToImage(tempEmfPath, imgOptions);

                        // Create a new page in the target diagram for this shape
                        Page newPage = new Page();
                        targetDiagram.Pages.Add(newPage);

                        // Insert the EMF image into the new page as a shape
                        using (FileStream fs = new FileStream(tempEmfPath, FileMode.Open, FileAccess.Read))
                        {
                            // Position and size (in inches) for the inserted image shape
                            double pinX = 5.0;   // X coordinate of the shape's pin
                            double pinY = 5.0;   // Y coordinate of the shape's pin
                            double width = 5.0;  // Width of the image
                            double height = 5.0; // Height of the image

                            // AddShape overload that accepts a Stream inserts the image as a foreign shape
                            long imageShapeId = newPage.AddShape(pinX, pinY, width, height, fs);
                            // The returned ID can be used later if further manipulation is required
                        }

                        // Clean up the temporary EMF file
                        File.Delete(tempEmfPath);
                    }
                }

                // Save the combined multi‑page diagram
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }