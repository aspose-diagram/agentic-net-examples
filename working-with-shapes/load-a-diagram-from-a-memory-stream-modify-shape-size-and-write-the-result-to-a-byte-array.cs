using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    // Loads a Visio diagram from a byte array, resizes the first shape found,
    // and returns the modified diagram as a new byte array.
    public static byte[] ResizeFirstShape(byte[] diagramBytes)
    {
        // Load diagram from the input memory stream
        using (MemoryStream inputStream = new MemoryStream(diagramBytes))
        {
            Diagram diagram = new Diagram(inputStream);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            Aspose.Diagram.Shape targetShape = null;
            foreach (Aspose.Diagram.Shape shape in page.Shapes)
            {
                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                throw new Exception("No shape found in the diagram.");
            }

            // Modify the shape's width and height (values are in inches)
            targetShape.XForm.Width.Value = 2.0;   // New width
            targetShape.XForm.Height.Value = 1.5;  // New height

            // Save the modified diagram to an output memory stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                return outputStream.ToArray();
            }
        }
    }

    // Example entry point demonstrating usage
    public static void Main()
    {
        try
        {

            // Path to an existing Visio file
            string inputPath = "input.vsdx";
            // Path where the modified file will be saved
            string outputPath = "output.vsdx";

            // Read the original file into a byte array
            byte[] inputBytes = File.ReadAllBytes(inputPath);

            // Process the diagram
            byte[] outputBytes = ResizeFirstShape(inputBytes);

            // Write the result back to disk
            File.WriteAllBytes(outputPath, outputBytes);

            Console.WriteLine("Diagram has been resized and saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
