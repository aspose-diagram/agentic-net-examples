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
            Console.Error.WriteLine("Error: No input diagram file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Example modification: set the first page background to white (optional)
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    // Ensure the page background is visible
                    firstPage.Background = BOOL.False;
                }

                // Serialize the diagram to a byte array using a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the diagram in VSDX format to the memory stream
                    diagram.Save(ms, SaveFileFormat.Vsdx);

                    // Retrieve the byte array
                    byte[] diagramBytes = ms.ToArray();

                    // Output information about the serialized data
                    Console.WriteLine($"Diagram serialized to byte array. Size: {diagramBytes.Length} bytes.");

                    // Example: write the byte array to a file (optional, can be removed)
                    // File.WriteAllBytes("output.vsdx", diagramBytes);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}