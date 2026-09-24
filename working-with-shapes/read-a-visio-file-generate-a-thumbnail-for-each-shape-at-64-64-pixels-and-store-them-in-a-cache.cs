using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect the Visio file path as the first argument.
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to a Visio file.");
                return;
            }

            string visioPath = args[0];

            // Load the Visio diagram.
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Cache to store thumbnails: key = shape ID, value = PNG bytes.
            Dictionary<long, byte[]> thumbnailCache = new Dictionary<long, byte[]>();

            // Prepare image save options for 64x64 PNG thumbnails.
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the output size. PageSize expects width and height in inches; using 64x64 pixels at 96 DPI.
            // 64 pixels / 96 DPI ≈ 0.6667 inches.
            imgOptions.PageSize = new PageSize(0.6667f, 0.6667f);
            // Ensure background pages are not exported.
            imgOptions.ExportHiddenPage = false;

            // Temporary folder for intermediate image files.
            string tempFolder = Path.Combine(Path.GetTempPath(), "VisioShapeThumbnails");
            Directory.CreateDirectory(tempFolder);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Generate a unique temporary file name.
                    string tempFile = Path.Combine(tempFolder, $"shape_{shape.ID}_{Guid.NewGuid()}.png");

                    try
                    {
                        // Export the shape to a PNG file using the prepared options.
                        shape.ToImage(tempFile, imgOptions);

                        // Read the PNG bytes into memory.
                        byte[] imageBytes = File.ReadAllBytes(tempFile);

                        // Store in the cache using the shape's unique ID.
                        thumbnailCache[shape.ID] = imageBytes;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to generate thumbnail for shape ID {shape.ID}: {ex.Message}");
                    }
                    finally
                    {
                        // Clean up the temporary file.
                        if (File.Exists(tempFile))
                        {
                            try { File.Delete(tempFile); } catch { /* ignore cleanup errors */ }
                        }
                    }
                }
            }

            // Example usage: write out the number of cached thumbnails.
            Console.WriteLine($"Generated thumbnails for {thumbnailCache.Count} shapes.");

            // Optional: persist the cache to disk or use it as needed.
            // For demonstration, we could write the first thumbnail to a file.
            if (thumbnailCache.Count > 0)
            {
                var firstEntry = new KeyValuePair<long, byte[]>();
                foreach (var kvp in thumbnailCache)
                {
                    firstEntry = kvp;
                    break;
                }

                string outputPath = Path.Combine(Environment.CurrentDirectory, $"thumbnail_shape_{firstEntry.Key}.png");
                File.WriteAllBytes(outputPath, firstEntry.Value);
                Console.WriteLine($"Sample thumbnail saved to: {outputPath}");
            }

            // Clean up temporary folder.
            try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }
        }
    }