using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Folder where individual shape PNGs will be saved
            string outputFolder = "ShapeImages";

            // Create output folder if it does not exist
            if (!System.IO.Directory.Exists(outputFolder))
            {
                System.IO.Directory.CreateDirectory(outputFolder);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Construct a unique file name for the shape image
                    string fileName = $"Page{page.ID}_Shape{shape.ID}.png";
                    string outputPath = System.IO.Path.Combine(outputFolder, fileName);

                    // Configure PNG export options (PNG is inherently lossless)
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

                    // Export the shape to a PNG file
                    shape.ToImage(outputPath, options);
                }
            }

            Console.WriteLine("All shapes have been exported as PNG images.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
