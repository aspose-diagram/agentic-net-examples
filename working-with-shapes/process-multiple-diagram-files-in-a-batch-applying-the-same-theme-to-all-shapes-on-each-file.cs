using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input folder containing Visio files (VSDX). Use first argument if provided, otherwise default to "InputDiagrams".
            string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";

            // Output folder where themed diagrams will be saved. Use second argument if provided, otherwise default to "OutputDiagrams".
            string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process each VSDX file in the input folder.
            foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Apply the theme to every non-deleted shape on each page.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked for deletion.
                            if (shape.Del == BOOL.False)
                            {
                                // Apply a preset theme to the shape.
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                            }
                        }
                    }

                    // Determine the output file path (same file name in the output folder).
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Log any errors for the current file and continue processing the rest.
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }