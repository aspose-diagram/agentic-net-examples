using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Directory containing Visio files
            string inputDirectory = @"C:\VisioFiles";
            // Directory to save processed files
            string outputDirectory = @"C:\VisioFiles\Processed";

            // Ensure output directory exists
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            // Get all Visio files (VSDX, VDX, VSD) in the input directory
            string[] visioFiles = Directory.GetFiles(inputDirectory, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vdx" && extension != ".vsd")
                    continue; // Skip non‑Visio files

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Apply a common theme to every shape in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Optional: set a page‑level theme
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply the theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                    }
                }

                // Determine output file path
                string fileName = Path.GetFileName(filePath);
                string outputPath = Path.Combine(outputDirectory, fileName);

                // Save the modified diagram back to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Processing completed.");
        }
    }