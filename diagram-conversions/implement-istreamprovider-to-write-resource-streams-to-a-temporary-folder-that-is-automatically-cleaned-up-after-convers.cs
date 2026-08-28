using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to write resources (images, CSS, etc.) to a temporary folder.
    // The folder is deleted when Cleanup() is called after the conversion.
    public class TempFolderStreamProvider : IStreamProvider
    {
        private string _tempFolder;
        private readonly List<string> _createdFiles = new List<string>();

        // Creates the temporary folder on first use and opens a file stream for the resource.
        public void InitStream(StreamProviderOptions options)
        {
            if (string.IsNullOrEmpty(_tempFolder))
            {
                _tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramTemp_" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(_tempFolder);
            }

            // options.DefaultPath is read‑only; use it to build the file name.
            string filePath = Path.Combine(_tempFolder, options.DefaultPath);
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            options.Stream = fileStream;
            _createdFiles.Add(filePath);
        }

        // Closes the stream after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Dispose();
        }

        // Deletes all files and the temporary folder.
        public void Cleanup()
        {
            foreach (var file in _createdFiles)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch
                {
                    // Ignored – best‑effort cleanup.
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(_tempFolder) && Directory.Exists(_tempFolder))
                    Directory.Delete(_tempFolder, true);
            }
            catch
            {
                // Ignored – best‑effort cleanup.
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load a diagram (replace with your actual file path).
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Example: export all pages as separate files.
                    PageCount = int.MaxValue,
                    SaveAsSingleFile = false
                };

                // Assign the custom stream provider.
                var provider = new TempFolderStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export to HTML.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                // Clean up temporary resources.
                provider.Cleanup();

                Console.WriteLine($"Diagram exported to '{outputHtml}'. Temporary resources have been removed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}