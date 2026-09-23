using Aspose.Diagram;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing diagram (replace with your actual source file)
            Diagram diagram = new Diagram("input.vsdx");

            // Path where the VSDX file will be saved
            string outputPath = "output.vsdx";

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Define maximum allowed file size (e.g., 5 MB)
            const long maxSizeBytes = 5 * 1024 * 1024; // 5 MB

            // Check the size of the saved file
            FileInfo fileInfo = new FileInfo(outputPath);
            if (fileInfo.Length > maxSizeBytes)
            {
                Console.WriteLine($"File size exceeds limit: {fileInfo.Length} bytes");
            }
            else
            {
                Console.WriteLine($"File saved successfully. Size: {fileInfo.Length} bytes");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
