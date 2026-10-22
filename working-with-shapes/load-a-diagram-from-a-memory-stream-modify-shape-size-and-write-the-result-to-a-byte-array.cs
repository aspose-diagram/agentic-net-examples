using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the file into a memory stream
        byte[] inputBytes;
        try
        {
            inputBytes = File.ReadAllBytes(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read input file: {ex.Message}");
            return;
        }

        using (MemoryStream inputStream = new MemoryStream(inputBytes))
        {
            // Aspose.Diagram cannot load directly from a stream, so write to a temporary file first
            string tempFilePath = Path.GetTempFileName();
            try
            {
                // Write the memory stream content to the temporary file
                using (FileStream tempFile = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    inputStream.Position = 0;
                    inputStream.CopyTo(tempFile);
                }

                Diagram diagram;
                try
                {
                    // Load the diagram from the temporary file
                    diagram = new Diagram(tempFilePath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                    return;
                }

                // Ensure the diagram has at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page firstPage = diagram.Pages[0];
                if (firstPage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the first page
                Shape shape = firstPage.Shapes[0];

                // Modify shape size (example: set width to 2.0 and height to 1.0)
                try
                {
                    shape.XForm.Width.Value = 2.0;
                    shape.XForm.Height.Value = 1.0;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to modify shape size: {ex.Message}");
                    return;
                }

                // Save the modified diagram to a memory stream as VSDX
                byte[] outputBytes;
                using (MemoryStream outputStream = new MemoryStream())
                {
                    try
                    {
                        diagram.Save(outputStream, SaveFileFormat.Vsdx);
                        outputBytes = outputStream.ToArray();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to save diagram to memory stream: {ex.Message}");
                        return;
                    }
                }

                // Example usage of the resulting byte array: write its length to console
                Console.WriteLine($"Modified diagram saved to byte array. Size: {outputBytes.Length} bytes.");
            }
            finally
            {
                // Clean up the temporary file
                try
                {
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
                catch
                {
                    // Suppress any exceptions during cleanup
                }
            }
        }
    }
}