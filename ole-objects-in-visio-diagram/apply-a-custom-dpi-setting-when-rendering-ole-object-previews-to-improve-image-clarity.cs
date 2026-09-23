using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Folder where OLE preview images will be saved
        string outputFolder = "OlePreviews";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify OLE foreign objects
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectData != null &&
                        shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Configure image export options with a custom DPI (e.g., 300)
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        saveOptions.Resolution = 300f; // DPI setting

                        // Build a unique file name for the preview image
                        string outputPath = Path.Combine(
                            outputFolder,
                            $"Page{page.ID}_Shape{shape.ID}.png");

                        // Render the OLE object preview to an image file
                        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            shape.ToImage(fileStream, saveOptions);
                        }

                        Console.WriteLine($"Saved OLE preview for shape ID {shape.ID} on page {page.ID} to {outputPath}");
                    }
                }
            }

            // Save the (potentially unchanged) diagram back to a file
            string outputDiagramPath = "output.vsdx";
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}