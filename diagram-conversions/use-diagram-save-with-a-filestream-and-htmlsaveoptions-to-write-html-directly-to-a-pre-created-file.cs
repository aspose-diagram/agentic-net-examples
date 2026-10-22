using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Implementation of IStreamProvider to handle embedded resources during HTML export.
    class SimpleStreamProvider : IStreamProvider
    {
        public void InitStream(StreamProviderOptions options)
        {
            // Ensure the directory for the resource exists.
            string directory = Path.GetDirectoryName(options.DefaultPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a file stream for the resource path.
            options.Stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // Close the stream if it was created.
            options.Stream?.Close();
        }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output HTML file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML save options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Export hidden pages? No.
                ExportHiddenPage = false,
                // Export comments? No.
                IsExportComments = false,
                // Assign the custom stream provider for handling embedded resources.
                StreamProvider = new SimpleStreamProvider()
            };

            // Open a FileStream for the output HTML file.
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Save the diagram as HTML directly to the FileStream using the options.
                diagram.Save(outputStream, htmlOptions);
            }

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}