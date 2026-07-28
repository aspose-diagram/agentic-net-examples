using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchResizeShapes
{
    // Predefined width in inches for the target shapes
    const double TargetWidthInches = 2.0;

    // Name (or universal name) of the shape to resize
    const string TargetShapeName = "MyTargetShape";

    static void Main()
    {
        try
        {

            // Directory containing the source VSDX files
            string sourceDirectory = @"C:\Visio\Source";

            // Directory where the modified files will be saved
            string outputDirectory = @"C:\Visio\Processed";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Process each VSDX file in the source directory
            foreach (string filePath in Directory.GetFiles(sourceDirectory, "*.vsdx"))
            {
                // Load the diagram using the constructor that accepts a file path
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape matches the target name (Name or NameU)
                            if (string.Equals(shape.Name, TargetShapeName, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(shape.NameU, TargetShapeName, StringComparison.OrdinalIgnoreCase))
                            {
                                // Resize the shape to the predefined width (height remains unchanged)
                                shape.SetWidth(TargetWidthInches);
                            }
                        }
                    }

                    // Build the output file path (same file name, different folder)
                    string outputPath = Path.Combine(outputDirectory, Path.GetFileName(filePath));

                    // Save the modified diagram back to VSDX format using the Save method
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
