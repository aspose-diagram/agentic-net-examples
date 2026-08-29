using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the source diagram
            Diagram sourceDiagram = new Diagram(inputPath);

            // Create an empty target diagram
            Diagram targetDiagram = new Diagram();

            // Iterate through each page of the source diagram
            foreach (Page srcPage in sourceDiagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape srcShape in srcPage.Shapes)
                {
                    // Skip deleted shapes
                    if (srcShape.Del == BOOL.True) continue;

                    // Generate a temporary EMF file path for the shape export
                    string tempEmfPath = Path.Combine(Path.GetTempPath(),
                        $"shape_{Guid.NewGuid():N}.emf");

                    // Export the shape to EMF using ImageSaveOptions
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                    srcShape.ToImage(tempEmfPath, imgOptions);

                    // Ensure the EMF file was created
                    if (!File.Exists(tempEmfPath))
                    {
                        Console.Error.WriteLine($"Failed to create EMF for shape ID {srcShape.ID}");
                        continue;
                    }

                    // Add a new page to the target diagram for this shape
                    Page newPage = new Page();
                    targetDiagram.Pages.Add(newPage);

                    // Retrieve page dimensions (in inches)
                    double pageWidth = newPage.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = newPage.PageSheet.PageProps.PageHeight.Value;

                    // Position the image at the centre of the page
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Insert the EMF image using a FileStream (the overload expects a stream)
                    using (FileStream emfStream = new FileStream(tempEmfPath, FileMode.Open, FileAccess.Read))
                    {
                        // The AddShape overload returns the shape ID (long)
                        long imageShapeId = newPage.AddShape(pinX, pinY, pageWidth, pageHeight, emfStream);
                        // Optionally retrieve the shape object if further adjustments are needed
                        Shape imageShape = newPage.Shapes.GetShape(imageShapeId);
                        // Example: set the shape name to identify the original shape
                        imageShape.Name = $"Shape_{srcShape.ID}";
                    }

                    // Delete the temporary EMF file
                    try { File.Delete(tempEmfPath); } catch { /* ignore cleanup errors */ }
                }
            }

            // Save the combined multi‑page diagram as VSDX
            targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Successfully created multi‑page diagram: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}