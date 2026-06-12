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

            // Folder containing the source VSDX files
            string inputFolder = @"C:\InputVsdx";
            // Folder where the modified files will be saved
            string outputFolder = @"C:\OutputVsdx";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Width (in inches) to set for each target shape
            double targetWidth = 2.0;

            // Name (or universal name) of the shape to resize
            string targetShapeName = "MyShape";

            // Process every VSDX file in the input directory
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
            {
                // Load the diagram using the provided constructor
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify the shape by Name or NameU
                        if (shape.Name == targetShapeName || shape.NameU == targetShapeName)
                        {
                            // Resize the shape to the predefined width
                            shape.SetWidth(targetWidth);
                        }
                    }
                }

                // Build the output file path (overwrite with same name)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                // Save the modified diagram using the provided Save method
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Release resources
                diagram.Dispose();
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
