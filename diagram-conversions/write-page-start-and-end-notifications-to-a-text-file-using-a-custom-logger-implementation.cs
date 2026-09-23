using System;
using System.IO;
using Aspose.Diagram;

namespace DiagramLoggingExample
{
    // Simple logger that appends messages to a text file
    public class FileLogger : IDisposable
    {
        private readonly StreamWriter _writer;

        public FileLogger(string filePath)
        {
            // Open the file in append mode
            _writer = new StreamWriter(filePath, append: true);
        }

        // Write a timestamped message
        public void Log(string message)
        {
            _writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        // Ensure the writer is properly closed
        public void Dispose()
        {
            _writer?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Initialize the custom logger (replace with desired log file path)
                using (FileLogger logger = new FileLogger("diagram_log.txt"))
                {
                    // Iterate through each page in the diagram
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        var page = diagram.Pages[i];

                        // Log page start
                        logger.Log($"Page {i + 1} (Name: {page.Name}) start");

                        // (Optional) Insert any page‑specific processing here

                        // Log page end
                        logger.Log($"Page {i + 1} (Name: {page.Name}) end");
                    }
                }

                // Save the diagram if any changes were made (replace with desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}