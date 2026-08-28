using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VdxImageExtractor
{
    static void Main(string[] args)
    {
        // Folder containing VDX files
        string inputFolder = @"C:\VisioFiles";
        // Folder where extracted images will be saved
        string outputFolder = @"C:\ExtractedImages";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each VDX file in the input folder
        foreach (string vdxPath in Directory.GetFiles(inputFolder, "*.vdx"))
        {
            // Load the diagram using the constructor that accepts a file path
            using (Diagram diagram = new Diagram(vdxPath))
            {
                string filePrefix = Path.GetFileNameWithoutExtension(vdxPath);
                int shapeCounter = 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeCounter++;

                        // Build the output image file name with original file prefix
                        string imageFileName = $"{filePrefix}_shape{shapeCounter}.png";
                        string imagePath = Path.Combine(outputFolder, imageFileName);

                        // Save the shape as an image (PNG format)
                        shape.ToImage(imagePath, new ImageSaveOptions(SaveFileFormat.Png));
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
