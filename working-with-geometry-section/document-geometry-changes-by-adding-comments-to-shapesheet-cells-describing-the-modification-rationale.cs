using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional second argument)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
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

                        // Example geometry modification:
                        // Increase the shape's width by 10% to demonstrate a change.
                        // This modifies the XForm.Width cell in the ShapeSheet.
                        double originalWidth = shape.XForm.Width.Value;
                        double newWidth = originalWidth * 1.10;
                        shape.XForm.Width.Value = newWidth;

                        // Document the change by adding a comment to the shape's Misc.Comment cell.
                        // This comment resides in the ShapeSheet and explains the rationale.
                        string commentText = $"Width increased from {originalWidth:F2} to {newWidth:F2} (10% increase) to improve layout spacing.";
                        // Ensure the Comment cell exists before assigning
                        if (shape.Misc != null && shape.Misc.Comment != null)
                        {
                            shape.Misc.Comment.Value = commentText;
                        }
                    }
                }

                // Save the modified diagram to the specified output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}