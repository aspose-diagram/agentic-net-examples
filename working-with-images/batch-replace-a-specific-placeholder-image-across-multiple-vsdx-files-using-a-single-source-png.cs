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

            // Folder containing the VSDX files to process
            string inputFolder = @"C:\Visio\SourceFiles";
            // Folder where the updated VSDX files will be saved
            string outputFolder = @"C:\Visio\UpdatedFiles";
            // Path to the PNG image that will replace the placeholder
            string placeholderImagePath = @"C:\Images\NewPlaceholder.png";
            // Name of the placeholder shape to replace (as defined in the Visio files)
            string placeholderShapeName = "PlaceholderImage";

            // Ensure output folder exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Load the PNG image bytes once
            byte[] newImageBytes = File.ReadAllBytes(placeholderImagePath);

            // Process each VSDX file in the input folder
            string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx");
            foreach (string filePath in vsdxFiles)
            {
                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx))
                    {
                        // Iterate through all pages and shapes
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                // Identify foreign (image) shapes with the specific placeholder name
                                if (shape.Type == TypeValue.Foreign && shape.Name == placeholderShapeName)
                                {
                                    // Replace the embedded image data
                                    shape.ForeignData.Value = newImageBytes;
                                }
                            }
                        }

                        // Determine output file path
                        string fileName = Path.GetFileName(filePath);
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Save the updated diagram
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Successfully processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Batch replacement completed.");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
