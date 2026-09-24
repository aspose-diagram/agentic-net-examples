using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Uniform margin value in inches (e.g., 0.1 inch)
        private const double UniformMarginInches = 0.1;

        static void Main(string[] args)
        {
            try
            {

                // Input folder containing diagram files
                string inputFolder = @"C:\Diagrams\Input";
                // Output folder for processed diagrams
                string outputFolder = @"C:\Diagrams\Output";

                // Ensure output folder exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Process all supported Visio files in the input folder
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");
                foreach (string filePath in diagramFiles)
                {
                    try
                    {
                        // Load diagram
                        Diagram diagram = new Diagram(filePath);

                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Apply uniform paragraph margins to each paragraph of the shape
                                ApplyUniformParagraphMargins(shape, UniformMarginInches);
                            }
                        }

                        // Build output file path preserving original file name
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                        // Save the modified diagram
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
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

        /// <summary>
        /// Sets uniform left, right, and first line indentation for all paragraphs of a shape.
        /// </summary>
        /// <param name="shape">The shape whose paragraphs will be modified.</param>
        /// <param name="marginInches">Margin value in inches.</param>
        private static void ApplyUniformParagraphMargins(Shape shape, double marginInches)
        {
            // Ensure the shape has at least one paragraph
            if (shape.Paras == null || shape.Paras.Count == 0)
                return;

            // Iterate through each paragraph and set margins
            foreach (Para para in shape.Paras)
            {
                // Indentation from left edge
                para.IndLeft.Value = marginInches;
                // Indentation from right edge
                para.IndRight.Value = marginInches;
                // First line indentation
                para.IndFirst.Value = marginInches;
            }
        }
    }