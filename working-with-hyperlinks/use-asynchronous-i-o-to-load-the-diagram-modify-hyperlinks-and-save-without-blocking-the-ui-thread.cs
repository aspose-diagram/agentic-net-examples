using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static async Task Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                try
                {
                    await ProcessDiagramAsync(inputPath, outputPath);
                    Console.WriteLine("Diagram processed and saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine($"[UnauthorizedAccessException] {ex.Message}");
            }
    }

        private static async Task ProcessDiagramAsync(string inputPath, string outputPath)
        {
            // Asynchronously read the source Visio file into a memory stream
            byte[] fileBytes;
            using (FileStream inputStream = new FileStream(
                inputPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true))
            {
                fileBytes = new byte[inputStream.Length];
                int bytesRead = 0;
                while (bytesRead < fileBytes.Length)
                {
                    int read = await inputStream.ReadAsync(
                        fileBytes,
                        bytesRead,
                        fileBytes.Length - bytesRead);
                    if (read == 0) break;
                    bytesRead += read;
                }
            }

            // Load the diagram from the memory stream (synchronous constructor)
            Diagram diagram;
            using (MemoryStream loadStream = new MemoryStream(fileBytes))
            {
                diagram = new Diagram(loadStream);
            }

            // Iterate through all pages and shapes to modify hyperlinks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Hyperlinks != null)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Example modification: prepend a query parameter to the address
                            string original = link.Address?.Value ?? string.Empty;
                            if (!string.IsNullOrEmpty(original))
                            {
                                link.Address.Value = original + "?modified=true";
                            }
                        }
                    }
                }
            }

            // Save the modified diagram into a memory stream
            using (MemoryStream saveStream = new MemoryStream())
            {
                diagram.Save(saveStream, SaveFileFormat.Vsdx);
                saveStream.Position = 0;

                // Asynchronously write the memory stream to the output file
                using (FileStream outputStream = new FileStream(
                    outputPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await saveStream.CopyToAsync(outputStream);
                }
            }
        }
    }