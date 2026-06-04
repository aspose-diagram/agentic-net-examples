using System;
using System.IO;
using Aspose.Diagram;

public class DiagramProcessor
{
    // Saves a diagram to VSDX format and ensures the file size does not exceed the specified limit (in bytes).
    public static void SaveDiagramAsVsdx(string inputFilePath, string outputFilePath, long maxFileSizeBytes)
    {
        // Load the diagram from the input file. The constructor automatically detects the format.
        Diagram diagram = new Diagram(inputFilePath);

        // Save the diagram to VSDX format using the Save method with SaveFileFormat enumeration.
        diagram.Save(outputFilePath, SaveFileFormat.Vsdx);

        // Verify the saved file size.
        FileInfo savedFileInfo = new FileInfo(outputFilePath);
        if (savedFileInfo.Length > maxFileSizeBytes)
        {
            // If the file exceeds the limit, delete the oversized file and throw an exception.
            diagram.Dispose();
            File.Delete(outputFilePath);
            throw new InvalidOperationException(
                $"Saved VSDX file size ({savedFileInfo.Length} bytes) exceeds the allowed limit of {maxFileSizeBytes} bytes.");
        }

        // Clean up resources.
        diagram.Dispose();
    }
}

// Example usage:
// long maxSize = 5 * 1024 * 1024; // 5 MB limit
// DiagramProcessor.SaveDiagramAsVsdx("source.vsd", "result.vsdx", maxSize);

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramProcessor.SaveDiagramAsVsdx("", "", 0);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
