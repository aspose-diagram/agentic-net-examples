using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";
            // Path to the output Visio diagram after re‑embedding images
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold Base64 strings for each image shape (optional, for demonstration)
            Dictionary<long, string> imageBase64Map = new Dictionary<long, string>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image (foreign) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Extract the raw image bytes
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Convert to Base64 string
                        string base64 = Convert.ToBase64String(imageBytes);
                        imageBase64Map[shape.ID] = base64;

                        // Output a short preview to the console
                        Console.WriteLine($"Shape ID {shape.ID} extracted as Base64 (first 30 chars): {base64.Substring(0, Math.Min(30, base64.Length))}...");

                        // Re‑embed the image using a data URI approach:
                        //   - Decode the Base64 back to bytes
                        //   - Assign the bytes back to the shape's foreign data
                        //   (Visio stores the image as raw bytes; the data URI is used during HTML export,
                        //    but re‑assigning the bytes restores the original image in the diagram.)
                        byte[] restoredBytes = Convert.FromBase64String(base64);
                        shape.ForeignData.Value = restoredBytes;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved with images re‑embedded.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
