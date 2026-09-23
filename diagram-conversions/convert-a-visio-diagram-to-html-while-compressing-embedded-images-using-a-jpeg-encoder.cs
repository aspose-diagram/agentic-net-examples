using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging;

namespace VisioToHtmlWithJpegCompression
{
    // Custom stream provider that captures the image data generated during HTML export,
    // re-encodes it as JPEG, and writes the JPEG file to the target location.
    public class JpegStreamProvider : IStreamProvider
    {
        // Stores the temporary memory streams for each resource path.
        private readonly Dictionary<string, MemoryStream> _tempStreams = new();

        // Called by Aspose.Diagram before writing a resource (e.g., an image).
        public void InitStream(StreamProviderOptions options)
        {
            // Create a memory stream to capture the original image data.
            var ms = new MemoryStream();
            options.Stream = ms;
            _tempStreams[options.DefaultPath] = ms;
        }

        // Called after the resource has been written to the provided stream.
        public void CloseStream(StreamProviderOptions options)
        {
            if (!_tempStreams.TryGetValue(options.DefaultPath, out var ms))
                return;

            // Reset the memory stream position to read the captured data.
            ms.Position = 0;

            // Load the image (originally PNG) from the memory stream using the fully qualified Aspose.Drawing.Image.
            using var originalImage = Aspose.Drawing.Image.FromStream(ms);

            // Determine the JPEG file path (same name, .jpg extension).
            string jpegPath = Path.ChangeExtension(options.DefaultPath, ".jpg");

            // Save the image as JPEG with default quality.
            using var outStream = new FileStream(jpegPath, FileMode.Create, FileAccess.Write);
            originalImage.Save(outStream, ImageFormat.Jpeg);

            // Cleanup: delete the original PNG file if it was created.
            try
            {
                if (File.Exists(options.DefaultPath))
                    File.Delete(options.DefaultPath);
            }
            catch
            {
                // Ignored – cleanup is best-effort.
            }

            // Remove the temporary stream from the dictionary.
            _tempStreams.Remove(options.DefaultPath);
        }
    }

    class Program
    {
        static void Main()
        {
            // Input Visio file path.
            string inputPath = "input.vsdx";

            // Guard: ensure the input file exists.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the Visio diagram inside a try/catch to capture loading errors.
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Configure HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                ExportHiddenPage = false,
                // Assign the custom JPEG stream provider.
                StreamProvider = new JpegStreamProvider()
            };

            // Output HTML file path.
            string outputHtml = "output.html";

            // Export the diagram to HTML inside a try/catch to capture saving errors.
            try
            {
                diagram.Save(outputHtml, htmlOptions);
                Console.WriteLine($"Diagram exported to HTML: {outputHtml}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error exporting to HTML: {ex.Message}");
                return;
            }

            // After export, replace image references from .png to .jpg in the generated HTML.
            try
            {
                // Guard: ensure the HTML file was created.
                if (!File.Exists(outputHtml))
                {
                    Console.Error.WriteLine($"HTML file not found after export: {outputHtml}");
                    return;
                }

                string htmlContent = File.ReadAllText(outputHtml);
                string updatedContent = htmlContent.Replace(".png", ".jpg");
                File.WriteAllText(outputHtml, updatedContent);
                Console.WriteLine("Image references in HTML updated to use JPEG files.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating HTML image references: {ex.Message}");
                throw;
            }
        }
    }
}