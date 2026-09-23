using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to write embedded resources to a temporary folder.
    // The temporary folder is deleted after the conversion completes.
    public class TempFolderStreamProvider : IStreamProvider
    {
        private readonly string _tempFolder;
        private readonly List<string> _createdFiles = new List<string>();

        public TempFolderStreamProvider()
        {
            // Create a unique temporary directory.
            _tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramTemp_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempFolder);
        }

        // Called by Aspose.Diagram before writing each resource stream.
        public void InitStream(StreamProviderOptions options)
        {
            // Determine a file name for the resource.
            string fileName = Path.GetFileName(options.DefaultPath);
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString() + ".bin";
            }

            string fullPath = Path.Combine(_tempFolder, fileName);
            // Create a file stream that Aspose will write the resource into.
            var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            options.Stream = fileStream;

            // Keep track of the file for later cleanup.
            _createdFiles.Add(fullPath);
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

        // Deletes all files and the temporary folder.
        public void Cleanup()
        {
            foreach (var file in _createdFiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    // Ignore any errors during cleanup.
                }
            }

            try
            {
                Directory.Delete(_tempFolder, true);
            }
            catch
            {
                // Ignore any errors during cleanup.
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
                string inputPath = "sample.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Prepare HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                var streamProvider = new TempFolderStreamProvider();
                htmlOptions.StreamProvider = streamProvider;

                // Export the diagram to HTML. Resources (images, CSS, etc.) will be written to the temp folder.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                // Clean up temporary files after conversion.
                streamProvider.Cleanup();

                Console.WriteLine("HTML export completed. Output file: " + outputHtml);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}