using System;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Configure global font settings before loading any diagram.
                // Set the default fallback font.
                FontConfigs.DefaultFontName = "Arial";

                // Add a font folder (recursive) so that all required fonts are available.
                // The second parameter indicates whether to search subfolders.
                FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

                // Attempt to set a custom font cache size.
                // Aspose.Diagram does not expose a public FontCacheSize property,
                // so we use reflection to set a possible internal field if it exists.
                try
                {
                    // Look for a static field named "fontCacheSize" in the FontConfigs type.
                    FieldInfo cacheField = typeof(FontConfigs).GetField("fontCacheSize", BindingFlags.Static | BindingFlags.NonPublic);
                    if (cacheField != null)
                    {
                        // Example: set cache size to 200 MB (value in bytes).
                        long cacheSizeBytes = 200L * 1024 * 1024;
                        cacheField.SetValue(null, cacheSizeBytes);
                        Console.WriteLine($"Font cache size set to {cacheSizeBytes} bytes via reflection.");
                    }
                    else
                    {
                        Console.WriteLine("Font cache size field not found; using default cache settings.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error configuring font cache size: {ex.Message}");
                }

                // Load a diagram (replace with actual file path as needed).
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Perform any processing on the diagram here...

                // Save the diagram after processing.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }