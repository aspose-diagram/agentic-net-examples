using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        // Size threshold in inches – shapes larger than this will have gluing disabled
        const double SizeThreshold = 2.0;

        static void Main(string[] args)
        {
            try
            {

                // Input folder (first argument) and output folder (second argument)
                string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";
                string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Get all Visio files in the input folder (VSDX, VSD, VDX, etc.)
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                foreach (string filePath in diagramFiles)
                {
                    // Process only supported Visio formats based on file extension
                    string extension = Path.GetExtension(filePath).ToLowerInvariant();
                    if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                    {
                        Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    try
                    {
                        // Load the diagram
                        using (Diagram diagram = new Diagram(filePath))
                        {
                            // Iterate through all pages
                            foreach (Page page in diagram.Pages)
                            {
                                // Iterate through all shapes on the page
                                foreach (Shape shape in page.Shapes)
                                {
                                    // Retrieve shape dimensions (in inches)
                                    double width = shape.XForm.Width.Value;
                                    double height = shape.XForm.Height.Value;

                                    // If either dimension exceeds the threshold, disable dynamic glue
                                    if (width > SizeThreshold || height > SizeThreshold)
                                    {
                                        // Ensure the Misc cell collection is present before setting GlueType
                                        if (shape.Misc != null && shape.Misc.GlueType != null)
                                        {
                                            shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                                        }
                                    }
                                }
                            }

                            // Prepare output file path (preserve original file name)
                            string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                            // Save the modified diagram in the same format as the original
                            // Use the appropriate SaveFileFormat based on extension
                            SaveFileFormat format = extension switch
                            {
                                ".vsdx" => SaveFileFormat.Vsdx,
                                ".vsd" => SaveFileFormat.Vsd,
                                ".vdx" => SaveFileFormat.Vdx,
                                _ => SaveFileFormat.Vsdx // fallback (should not reach here)
                            };

                            diagram.Save(outputPath, format);
                            Console.WriteLine($"Processed and saved: {outputPath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }

                Console.WriteLine("Batch processing completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }