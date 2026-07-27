using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source diagram and the VSDX output
            string inputPath = "input.vsd";          // existing diagram file
            string outputPath = "output.vsdx";       // target VSDX file

            // Maximum allowed file size in bytes (example: 5 MB)
            long maxSizeBytes = 5 * 1024 * 1024;

            // Load the diagram from the input file using the built‑in constructor
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram to VSDX format using the provided Save method
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Verify that the saved file does not exceed the size limit
            FileInfo fileInfo = new FileInfo(outputPath);
            if (fileInfo.Length > maxSizeBytes)
            {
                Console.WriteLine($"Error: File size {fileInfo.Length} bytes exceeds the limit of {maxSizeBytes} bytes.");
            }
            else
            {
                Console.WriteLine($"Success: File saved as '{outputPath}' with size {fileInfo.Length} bytes.");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
