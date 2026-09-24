using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Adjust these values as needed
        private const string InputZipPath = @"C:\Input\Diagrams.zip";
        private const string OutputZipPath = @"C:\Output\ProcessedDiagrams.zip";

        // Mapping of shape universal name (NameU) to desired width and height (in inches)
        private static readonly Dictionary<string, (double Width, double Height)> ShapeSizeMap = new Dictionary<string, (double, double)>(StringComparer.OrdinalIgnoreCase)
        {
            { "Process", (2.0, 1.0) },   // Example: set "Process" shapes to 2" x 1"
            { "Decision", (1.5, 1.5) }   // Example: set "Decision" shapes to 1.5" x 1.5"
        };

        static void Main()
        {
            try
            {

                // Create a temporary working directory
                string tempDirectory = Path.Combine(Path.GetTempPath(), "DiagramBatch_" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(tempDirectory);

                try
                {
                    // Extract all files from the input zip to the temporary directory
                    ZipFile.ExtractToDirectory(InputZipPath, tempDirectory);

                    // Process each diagram file in the temporary directory (including subfolders)
                    string[] diagramFiles = Directory.GetFiles(tempDirectory, "*.*", SearchOption.AllDirectories);
                    foreach (string diagramFilePath in diagramFiles)
                    {
                        // Consider only Visio file extensions supported by Aspose.Diagram
                        string extension = Path.GetExtension(diagramFilePath);
                        if (string.Equals(extension, ".vsdx", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(extension, ".vsd", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(extension, ".vdx", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(extension, ".vssx", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(extension, ".vstx", StringComparison.OrdinalIgnoreCase))
                        {
                            ProcessDiagram(diagramFilePath);
                        }
                    }

                    // Create a new zip containing the processed files
                    if (File.Exists(OutputZipPath))
                    {
                        File.Delete(OutputZipPath);
                    }
                    ZipFile.CreateFromDirectory(tempDirectory, OutputZipPath);
                    Console.WriteLine($"Processing complete. Output zip created at: {OutputZipPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred during processing:");
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    // Clean up temporary directory
                    if (Directory.Exists(tempDirectory))
                    {
                        Directory.Delete(tempDirectory, true);
                    }
                }

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }

        private static void ProcessDiagram(string filePath)
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        continue;
                    }

                    // Check if the shape's universal name matches any entry in the size map
                    string shapeNameU = shape.NameU;
                    if (ShapeSizeMap.TryGetValue(shapeNameU, out (double Width, double Height) newSize))
                    {
                        // Adjust width and height using the .Value accessor
                        shape.XForm.Width.Value = newSize.Width;
                        shape.XForm.Height.Value = newSize.Height;
                        Console.WriteLine($"Adjusted shape '{shapeNameU}' (ID: {shape.ID}) on page '{page.Name}' to {newSize.Width}\" x {newSize.Height}\".");
                    }
                }
            }

            // Save the modified diagram back to the same location (overwrites original)
            diagram.Save(filePath, SaveFileFormat.Vsdx);
        }
    }