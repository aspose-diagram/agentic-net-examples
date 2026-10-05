using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input VSDX file path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Read the VSDX file into a memory stream.
        MemoryStream memoryStream = new MemoryStream();
        try
        {
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(memoryStream);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file into memory stream: {ex.Message}");
            return;
        }

        // Aspose.Diagram cannot load directly from a stream, so write the stream to a temporary file.
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");
        try
        {
            memoryStream.Position = 0;
            using (FileStream tempFile = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(tempFile);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing temporary file: {ex.Message}");
            return;
        }

        // Load the diagram from the temporary file and extract the title property.
        Diagram diagram = null;
        try
        {
            diagram = new Diagram(tempPath);
            string title = diagram.DocumentProps.Title ?? string.Empty;
            Console.WriteLine($"Document Title: {title}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram or extracting title: {ex.Message}");
        }
        finally
        {
            // Clean up resources.
            diagram?.Dispose();

            try
            {
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Warning: Unable to delete temporary file: {ex.Message}");
            }

            memoryStream.Dispose();
        }
    }
}