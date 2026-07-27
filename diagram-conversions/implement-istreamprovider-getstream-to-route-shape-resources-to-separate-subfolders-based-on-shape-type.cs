using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExport
{
    // Custom stream provider that routes resources to subfolders based on shape type.
    public class CustomStreamProvider : IStreamProvider
    {
        // Called before a resource stream is created.
        public void InitStream(StreamProviderOptions options)
        {
            // Determine the base directory where the original resource would be saved.
            string originalPath = options.DefaultPath; // Read‑only property containing the intended file name.
            string fileName = Path.GetFileName(originalPath);
            string baseDir = Path.GetDirectoryName(originalPath) ?? Directory.GetCurrentDirectory();

            // Simple heuristic to decide subfolder based on file name/content.
            string subFolder;
            if (originalPath.IndexOf("image", StringComparison.OrdinalIgnoreCase) >= 0)
                subFolder = "Images";
            else if (originalPath.IndexOf("foreign", StringComparison.OrdinalIgnoreCase) >= 0)
                subFolder = "Foreign";
            else
                subFolder = "Resources";

            // Ensure the subfolder exists.
            string targetDir = Path.Combine(baseDir, subFolder);
            Directory.CreateDirectory(targetDir);

            // Full path for the resource file.
            string targetPath = Path.Combine(targetDir, fileName);

            // Assign the stream that Aspose.Diagram will write to.
            options.Stream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource stream has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created.
            options.Stream?.Dispose();
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
                htmlOptions.StreamProvider = new CustomStreamProvider();

                // Export the diagram to HTML; resources (images, foreign objects, etc.) will be placed
                // in subfolders according to the logic in CustomStreamProvider.
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("Diagram exported to HTML with resources routed to subfolders.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}