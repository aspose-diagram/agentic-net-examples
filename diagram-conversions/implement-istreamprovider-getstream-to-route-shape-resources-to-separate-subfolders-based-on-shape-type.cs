using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider to route shape resources into subfolders based on file type.
    public class ShapeResourceStreamProvider : IStreamProvider
    {
        // Called before a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Determine the folder based on the resource file extension.
            string extension = Path.GetExtension(options.DefaultPath).ToLowerInvariant();
            string subFolder;

            switch (extension)
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".bmp":
                    subFolder = "images";
                    break;
                case ".svg":
                    subFolder = "svgs";
                    break;
                case ".css":
                    subFolder = "styles";
                    break;
                case ".js":
                    subFolder = "scripts";
                    break;
                default:
                    subFolder = "resources";
                    break;
            }

            // Build the full path: <output directory>/<subFolder>/<file name>
            string outputDirectory = Path.GetDirectoryName(options.DefaultPath);
            string targetDirectory = Path.Combine(outputDirectory, subFolder);
            Directory.CreateDirectory(targetDirectory); // Ensure the folder exists.

            string targetPath = Path.Combine(targetDirectory, Path.GetFileName(options.DefaultPath));

            // Assign a writable file stream to the options.
            options.Stream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                options.Stream = null;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new ShapeResourceStreamProvider();

                // Export the diagram to HTML. Resources will be placed in subfolders.
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("HTML export completed with resources organized into subfolders.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}