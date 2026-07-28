using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load diagram bytes (replace with actual source of the byte array)
        byte[] inputBytes = GetDiagramBytes();

        // Guard against empty input to avoid EndOfStreamException
        if (inputBytes == null || inputBytes.Length == 0)
        {
            Console.Error.WriteLine("Input byte array is empty.");
            return;
        }

        Diagram diagram = null;

        // Load the diagram from the byte array using a MemoryStream
        try
        {
            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            {
                // Diagram constructor accepts a Stream; wrap in try/catch for safety
                diagram = new Diagram(inputStream);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure there is at least one page; add a blank page if necessary
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Access the first page
        Page page = diagram.Pages[0];

        // Example modification: set page size to A4 dimensions (in inches)
        page.PageSheet.PageProps.PageWidth.Value = 8.27;   // Width in inches
        page.PageSheet.PageProps.PageHeight.Value = 11.69; // Height in inches

        // Save the modified diagram to a byte array in VSDX format
        byte[] outputBytes;
        try
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                outputBytes = outputStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
            return;
        }

        // Output result information
        Console.WriteLine($"Diagram processed. Output size: {outputBytes.Length} bytes.");
    }

    // Placeholder method to obtain the input byte array.
    // Replace this implementation with the actual source of your diagram bytes.
    static byte[] GetDiagramBytes()
    {
        string path = "input.vsdx";

        // Guard to ensure the file exists before reading
        if (!File.Exists(path))
        {
            Console.Error.WriteLine($"File not found: {path}");
            return new byte[0];
        }

        return File.ReadAllBytes(path);
    }
}