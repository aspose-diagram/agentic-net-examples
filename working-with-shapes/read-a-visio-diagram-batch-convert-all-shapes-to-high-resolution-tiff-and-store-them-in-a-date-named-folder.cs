using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command‑line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a date‑named output folder (e.g., 20231021)
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("yyyyMMdd"));
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape image
                        string fileName = $"Page{page.ID}_Shape{shape.ID}.tiff";
                        string outPath = Path.Combine(outputFolder, fileName);

                        // Configure high‑resolution TIFF export options
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Tiff);
                        // If the Resolution property is available, uncomment the following line:
                        // saveOptions.Resolution = 300;

                        // Export the shape to a TIFF image
                        shape.ToImage(outPath, saveOptions);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}