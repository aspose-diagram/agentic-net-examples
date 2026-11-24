using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (replace with actual path or pass as argument)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram from a memory stream
        Diagram diagram;
        try
        {
            // Read all bytes into a memory stream
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream inputStream = new MemoryStream(fileBytes))
            {
                diagram = new Diagram(inputStream);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Modify the size of the first shape on the first page
        try
        {
            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0 || diagram.Pages[0].Shapes.Count == 0)
            {
                Console.Error.WriteLine("Diagram does not contain any pages or shapes to modify.");
                return;
            }

            // Retrieve the shape
            Shape shape = diagram.Pages[0].Shapes[0];

            // Set new width and height (example values)
            double newWidth = 4.0;   // inches or Visio units
            double newHeight = 2.5;  // inches or Visio units

            shape.XForm.Width.Value = newWidth;
            shape.XForm.Height.Value = newHeight;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error modifying shape size: {ex.Message}");
            return;
        }

        // Save the modified diagram to a byte array
        byte[] resultBytes;
        try
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Save to the stream in VSDX format
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                resultBytes = outputStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram to byte array: {ex.Message}");
            return;
        }

        // Output result information (e.g., size of the resulting byte array)
        Console.WriteLine($"Diagram saved to byte array. Size: {resultBytes.Length} bytes.");
        // The byte array 'resultBytes' can now be used as needed.
    }
}