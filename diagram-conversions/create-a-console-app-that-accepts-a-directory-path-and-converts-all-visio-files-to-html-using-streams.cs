using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: DiagramToHtml <directoryPath>");
            return;
        }

        string inputDirectory = args[0];

        // Verify that the supplied directory exists.
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Error: Directory does not exist -> {inputDirectory}");
            return;
        }

        // Define Visio file extensions to process.
        string[] visioExtensions = new string[]
        {
            ".vsdx", ".vsd", ".vsx", ".vtx",
            ".vssx", ".vstx", ".vss", ".vst",
            ".vsdm", ".vssm", ".vstm"
        };

        // Retrieve all files in the directory.
        string[] allFiles = Directory.GetFiles(inputDirectory);

        foreach (string filePath in allFiles)
        {
            // Filter files by supported Visio extensions.
            string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
            bool isVisio = false;
            foreach (string ext in visioExtensions)
            {
                if (ext == fileExtension)
                {
                    isVisio = true;
                    break;
                }
            }

            if (!isVisio)
            {
                continue; // Skip non‑Visio files.
            }

            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Warning: File not found -> {filePath}");
                continue;
            }

            try
            {
                // Determine output HTML file path (same folder, same name with .html extension).
                string outputHtmlPath = Path.Combine(
                    inputDirectory,
                    Path.GetFileNameWithoutExtension(filePath) + ".html");

                // Open the source Visio file as a read‑only stream.
                using (FileStream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Load the diagram from the stream inside a using block as required.
                    using (Diagram diagram = new Diagram(inputStream))
                    {
                        // Prepare the output stream for the HTML file.
                        using (FileStream outputStream = new FileStream(outputHtmlPath, FileMode.Create, FileAccess.Write))
                        {
                            // Create HTML save options (default settings are sufficient for basic conversion).
                            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                            // Save the diagram to the output stream using the HTML options.
                            diagram.Save(outputStream, htmlOptions);
                        }
                    }
                }

                Console.WriteLine($"Successfully converted: {filePath} -> {outputHtmlPath}");
            }
            catch (Exception ex)
            {
                // Report any conversion errors without terminating the whole process.
                Console.Error.WriteLine($"Error converting file '{filePath}': {ex.Message}");
            }
        }
    }
}