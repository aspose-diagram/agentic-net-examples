using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AsyncVisioExample <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            Diagram diagram = await LoadDiagramAsync(inputPath);
            UpdateWindowProperties(diagram);
            await SaveDiagramAsync(diagram, outputPath);
            Console.WriteLine("Diagram processed and saved.");
        }

        // Asynchronously loads a Visio file into a Diagram object.
        private static async Task<Diagram> LoadDiagramAsync(string path)
        {
            // Read file bytes asynchronously.
            byte[] fileBytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
            {
                fileBytes = new byte[fs.Length];
                int read = 0;
                while (read < fileBytes.Length)
                {
                    int bytesRead = await fs.ReadAsync(fileBytes, read, fileBytes.Length - read);
                    if (bytesRead == 0) break;
                    read += bytesRead;
                }
            }

            // Load diagram from memory stream.
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                // Diagram constructor that accepts a Stream.
                return new Diagram(ms);
            }
        }

        // Updates window properties (grid, guides, rulers, etc.) for all windows in the diagram.
        private static void UpdateWindowProperties(Diagram diagram)
        {
            foreach (Window window in diagram.Windows)
            {
                // Enable visual aids.
                window.ShowGrid = BOOL.True;
                window.ShowGuides = BOOL.True;
                window.ShowRulers = BOOL.True;
                window.ShowPageBreaks = BOOL.True;
                window.DynamicGridEnabled = BOOL.True;
                window.ShowConnectionPoints = BOOL.True;

                // Example: maximize the window.
                window.WindowState = WindowStateValue.Maximized;
            }
        }

        // Asynchronously saves the diagram to a file.
        private static async Task SaveDiagramAsync(Diagram diagram, string outputPath)
        {
            // Use a FileStream with async support.
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                // Diagram.Save is synchronous; wrap it in Task.Run to avoid blocking.
                await Task.Run(() => diagram.Save(fs, SaveFileFormat.Vsdx));
            }
        }
    }