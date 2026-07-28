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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Folder where individual shape PNGs will be saved
            string outputFolder = "ShapeImages";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.False)
                    {
                        // Build a unique file name for each shape
                        string fileName = $"shape_{shape.ID}.png";
                        string outPath = Path.Combine(outputFolder, fileName);

                        // Export the shape as a lossless PNG
                        ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                        shape.ToImage(outPath, options);
                    }
                }
            }

            // (Optional) Save the diagram back if any modifications were made
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
