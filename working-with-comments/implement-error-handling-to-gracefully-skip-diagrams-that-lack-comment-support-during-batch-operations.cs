using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing diagram files; adjust as needed or pass as first argument
            string inputFolder = args.Length > 0 ? args[0] : @"C:\Diagrams";
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Process each Visio file in the folder
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in diagramFiles)
            {
                // Filter supported Visio extensions
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    Console.WriteLine($"Skipping unsupported file type: {filePath}");
                    continue;
                }

                Console.WriteLine($"Processing file: {filePath}");
                Diagram diagram = null;
                try
                {
                    // Load the diagram
                    diagram = new Diagram(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load diagram '{filePath}': {ex.Message}");
                    continue; // Skip to next file
                }

                bool commentSupported = true;

                // Attempt to add a comment to each page; if any operation fails, treat as unsupported
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Add a page-level comment at coordinates (1,1)
                        page.AddComment(1.0, 1.0, "Batch processed comment");
                    }
                    catch (Exception ex)
                    {
                        // If adding a comment throws, mark as unsupported and break out
                        Console.WriteLine($"Comment operation not supported on page '{page.Name}': {ex.Message}");
                        commentSupported = false;
                        break;
                    }
                }

                if (!commentSupported)
                {
                    Console.WriteLine($"Skipping file due to lack of comment support: {filePath}");
                    // Dispose diagram (Diagram implements IDisposable)
                    diagram.Dispose();
                    continue;
                }

                // Save the modified diagram with a suffix to avoid overwriting the original
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(filePath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(filePath) + "_processed" + extension);

                try
                {
                    // Determine appropriate SaveFileFormat based on extension
                    SaveFileFormat format = extension switch
                    {
                        ".vsdx" => SaveFileFormat.Vsdx,
                        ".vsd" => SaveFileFormat.Vsd,
                        ".vdx" => SaveFileFormat.Vdx,
                        _ => SaveFileFormat.Vsdx // Fallback, should not reach here
                    };

                    diagram.Save(outputPath, format);
                    Console.WriteLine($"Saved processed diagram to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save diagram '{outputPath}': {ex.Message}");
                }
                finally
                {
                    // Ensure resources are released
                    diagram.Dispose();
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }