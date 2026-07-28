using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file (adjust the path as needed)
            string inputPath = "input.vsdx";

            // Output preview image file
            string outputPath = "preview.png";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to modify user-defined cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example: look for a user-defined cell named "CustomWidth" and set its value
                    foreach (User userCell in shape.Users)
                    {
                        if (userCell.Name == "CustomWidth")
                        {
                            userCell.Value.Val = "200"; // new value
                            Console.WriteLine($"Shape ID {shape.ID}: User cell '{userCell.Name}' set to {userCell.Value.Val}");
                        }
                    }
                }
            }

            // Save the modified diagram as a PNG image for visual preview
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Preview image saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
