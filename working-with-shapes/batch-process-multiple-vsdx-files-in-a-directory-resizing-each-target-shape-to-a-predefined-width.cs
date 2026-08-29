using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchResizeShapes
{
    static void Main()
    {
        try
        {

            // Directory containing source VSDX files
            string inputDirectory = @"C:\Visio\Input";
            // Directory where resized files will be saved
            string outputDirectory = @"C:\Visio\Output";

            // Ensure output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Width (in inches) to set for each target shape
            double targetWidth = 2.0;

            // Name (universal name) of the shape to resize
            string targetShapeNameU = "MyShape";

            // Process each VSDX file in the input directory
            foreach (string filePath in Directory.GetFiles(inputDirectory, "*.vsdx"))
            {
                // Load the diagram using the constructor that accepts a file path
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape matches the target name
                            if (string.Equals(shape.NameU, targetShapeNameU, StringComparison.OrdinalIgnoreCase))
                            {
                                // Resize the shape width to the predefined value
                                shape.SetWidth(targetWidth);
                            }
                        }
                    }

                    // Build output file path
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(filePath));

                    // Save the modified diagram back to VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
