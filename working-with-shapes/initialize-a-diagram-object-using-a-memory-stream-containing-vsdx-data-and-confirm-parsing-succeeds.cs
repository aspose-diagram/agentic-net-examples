using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input VSDX file path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the VSDX file into a memory stream.
        byte[] fileBytes;
        try
        {
            fileBytes = File.ReadAllBytes(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file into memory: {ex.Message}");
            return;
        }

        using (MemoryStream memoryStream = new MemoryStream(fileBytes))
        {
            // Write the memory stream to a temporary file because Diagram cannot be constructed directly from a stream.
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".vsdx");
            try
            {
                using (FileStream tempFile = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(tempFile);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating temporary file: {ex.Message}");
                return;
            }

            // Attempt to load the diagram from the temporary file.
            try
            {
                using (Diagram diagram = new Diagram(tempFilePath))
                {
                    // Confirm that the diagram was parsed by checking the page collection.
                    if (diagram.Pages != null && diagram.Pages.Count > 0)
                    {
                        Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");
                    }
                    else
                    {
                        Console.WriteLine("Diagram loaded, but it contains no pages.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            }
            finally
            {
                // Clean up the temporary file.
                try
                {
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
                catch
                {
                    // Suppress any cleanup errors.
                }
            }
        }
    }
}