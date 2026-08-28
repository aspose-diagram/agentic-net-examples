using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram (replace with your actual file)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define output file and size limit (e.g., 5 MB)
            string outputPath = "output.vsdx";
            long maxSizeBytes = 5 * 1024 * 1024; // 5 MB

            // Save the diagram in VSDX format using the provided Save method
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

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
