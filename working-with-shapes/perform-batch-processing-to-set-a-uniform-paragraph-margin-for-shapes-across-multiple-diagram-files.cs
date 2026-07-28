using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        // Uniform paragraph margin in inches
        private const double ParagraphMarginInches = 0.1;

        static void Main(string[] args)
        {
            try
            {

                // Input folder containing Visio files (VSDX). Adjust as needed.
                string inputFolder = @"C:\VisioFiles\Input";
                // Output folder where modified files will be saved.
                string outputFolder = @"C:\VisioFiles\Output";

                // Ensure output directory exists.
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all VSDX files in the input folder.
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

                foreach (string filePath in diagramFiles)
                {
                    try
                    {
                        // Load the diagram.
                        Diagram diagram = new Diagram(filePath);

                        // Iterate through all pages.
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page.
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip shapes that are marked as deleted.
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Ensure the shape has at least one paragraph.
                                if (shape.Paras == null || shape.Paras.Count == 0)
                                    continue;

                                // Apply uniform margins to each paragraph of the shape.
                                foreach (Para para in shape.Paras)
                                {
                                    para.IndLeft.Value = ParagraphMarginInches;
                                    para.IndRight.Value = ParagraphMarginInches;
                                    para.IndFirst.Value = ParagraphMarginInches;
                                }
                            }
                        }

                        // Save the modified diagram to the output folder using the same file name.
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);

                        Console.WriteLine($"Processed and saved: {outputPath}");
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