using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Entry point – async to avoid blocking the UI thread (console in this case)
        static async Task Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Perform load, modify, and save on a background thread
                await Task.Run(() =>
                {
                    // Load diagram from a file stream (asynchronous file I/O)
                    using (FileStream inputStream = new FileStream(
                        inputPath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read,
                        bufferSize: 4096,
                        useAsync: true))
                    {
                        // Diagram constructor reads from the stream
                        Diagram diagram = new Diagram(inputStream);

                        // Update all hyperlinks in the diagram
                        UpdateHyperlinks(diagram);

                        // Save the modified diagram to a file stream (asynchronous file I/O)
                        using (FileStream outputStream = new FileStream(
                            outputPath,
                            FileMode.Create,
                            FileAccess.Write,
                            FileShare.None,
                            bufferSize: 4096,
                            useAsync: true))
                        {
                            // Save using the overload that accepts a stream and a format
                            diagram.Save(outputStream, SaveFileFormat.Vsdx);
                        }
                    }
                });

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Iterates through all pages and shapes, updating hyperlink addresses
        private static void UpdateHyperlinks(Diagram diagram)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Hyperlinks collection exists
                    if (shape.Hyperlinks != null)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Set a new address for each hyperlink
                            link.Address.Value = "https://example.com";
                        }
                    }
                }
            }
        }
    }