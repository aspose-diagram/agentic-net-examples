using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Folder containing the source Visio files
            string inputFolder = @"C:\Visio\Input";
            // Folder where the modified files will be saved
            string outputFolder = @"C:\Visio\Output";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all Visio files in the input folder (supports .vsdx, .vsd, .vdx, etc.)
            string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                // Only handle files with Visio extensions
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension != ".vsdx" && extension != ".vsd" && extension != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate through each page
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through each shape on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Check if the shape's universal name is "Arrow"
                                if (string.Equals(shape.NameU, "Arrow", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Rotate the shape by 90 degrees (π/2 radians)
                                    double currentAngle = shape.XForm.Angle.Value;
                                    shape.XForm.Angle.Value = currentAngle + (Math.PI / 2);
                                }
                            }
                        }

                        // Determine output file path (preserve original name)
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                        // Save the modified diagram in VSDX format
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }

                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
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
