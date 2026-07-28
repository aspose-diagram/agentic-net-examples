using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        // Size threshold in inches – shapes larger than this will have gluing disabled
        const double SizeThreshold = 2.0;

        static void Main()
        {
            try
            {

                // Input folder containing Visio files
                string inputFolder = @"C:\Visio\Input";
                // Output folder for processed files
                string outputFolder = @"C:\Visio\Output";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Process each VSDX file in the input folder
                foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
                {
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
                                    // Skip deleted shapes
                                    if (shape.Del == BOOL.True)
                                        continue;

                                    // Retrieve shape dimensions (in inches)
                                    double width = shape.XForm.Width.Value;
                                    double height = shape.XForm.Height.Value;

                                    // If either dimension exceeds the threshold, disable dynamic glue
                                    if (width > SizeThreshold || height > SizeThreshold)
                                    {
                                        shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                                    }
                                }
                            }

                            // Build output file path
                            string fileName = Path.GetFileNameWithoutExtension(filePath);
                            string outputPath = Path.Combine(outputFolder, $"{fileName}_NoGlue.vsdx");

                            // Save the modified diagram
                            diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        }

                        Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
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