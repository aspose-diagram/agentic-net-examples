using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // TODO: Perform any required modifications to the diagram here.
                // Example: change the background of the first page to white.
                // if (diagram.Pages.Count > 0)
                // {
                //     diagram.Pages[0].Background = BOOL.False; // placeholder modification
                // }
                // -----------------------------------------------------------------

                // Save the modified diagram into a memory stream (VSDX format)
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                    // Reset stream position to the beginning for further processing
                    memoryStream.Position = 0;

                    // Example: output the size of the generated stream
                    Console.WriteLine($"Diagram saved to memory stream. Size: {memoryStream.Length} bytes.");

                    // The memoryStream can now be used for further processing in a web service
                    // (e.g., returned as a file response, uploaded, etc.)
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