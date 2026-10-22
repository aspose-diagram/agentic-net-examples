using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output preview image path
        string outputPath = "preview.png";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has user-defined cells
                    if (shape.Users != null && shape.Users.Count > 0)
                    {
                        // Modify each user-defined cell's value
                        foreach (User userCell in shape.Users)
                        {
                            // Example modification: prepend "Modified_" to the existing value
                            string originalValue = userCell.Value?.Val ?? string.Empty;
                            userCell.Value.Val = "Modified_" + originalValue;
                        }
                    }
                }
            }

            // Prepare image save options for PNG preview
            ImageSaveOptions imageOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the modified diagram as a PNG preview
            diagram.Save(outputPath, imageOptions);

            Console.WriteLine($"Preview image saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}