using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the diagram inside a try‑catch block
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        FileStream outputStream = null;

        // Perform the conversion, ensuring the stream is closed in finally
        try
        {
            // Create (or overwrite) the output file stream
            outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

            // Save the diagram to the desired format (PDF in this example)
            diagram.Save(outputStream, SaveFileFormat.Pdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
        finally
        {
            // Ensure the FileStream is properly closed
            if (outputStream != null)
            {
                try
                {
                    outputStream.Close();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to close stream: {ex.Message}");
                }
            }
        }
    }
}