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
            Console.Error.WriteLine("Usage: Program <input_vsdx_path>");
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
        byte[] fileBytes;
        try
        {
            fileBytes = File.ReadAllBytes(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading file: {ex.Message}");
            return;
        }

        using (MemoryStream memoryStream = new MemoryStream(fileBytes))
        {
            // Since Aspose.Diagram does not support loading directly from a stream,
            // write the stream to a temporary file and load from that file.
            string tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), ".vsdx");
            try
            {
                File.WriteAllBytes(tempFilePath, memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error writing temporary file: {ex.Message}");
                return;
            }

            try
            {
                // Load the diagram from the temporary file.
                using (Diagram diagram = new Diagram(tempFilePath))
                {
                    // Extract the document title property.
                    string title = diagram.DocumentProps.Title;

                    // Output the title to the console.
                    Console.WriteLine($"Document Title: {title}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
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