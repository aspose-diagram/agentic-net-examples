using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file and the output file.
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram.
            Diagram diagram = new Diagram(inputPath);

            // Load the new PNG image into a byte array (memory source).
            // In a real scenario, the byte[] could come from any source (e.g., a database, network stream, etc.).
            byte[] newImageBytes = File.ReadAllBytes("newImage.png");

            // Flag to indicate whether a placeholder image was replaced.
            bool replaced = false;

            // Iterate through all pages and shapes to locate a placeholder image.
            // Here we assume the placeholder is a foreign shape (image) with a specific name.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes.
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Example condition: shape name contains "Placeholder".
                        // Adjust the condition as needed for your specific diagram.
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.IndexOf("Placeholder", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Replace the embedded image data.
                            shape.ForeignData.Value = newImageBytes;
                            replaced = true;
                            // If only one placeholder should be replaced, exit loops.
                            break;
                        }
                    }
                }
                if (replaced) break;
            }

            if (!replaced)
            {
                throw new Exception("Placeholder image shape not found in the diagram.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
