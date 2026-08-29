using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, logo image file, output PNG path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <visioFile> <logoImage> <outputPng>");
            return;
        }

        // Assign and validate the Visio diagram path.
        string visioPath = args[0];
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Assign and validate the logo image path.
        string logoPath = args[1];
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"File not found: {logoPath}");
            return;
        }

        // Assign the output PNG path (no existence check needed for output).
        string outputPath = args[2];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Collect placeholder shape IDs to avoid modifying the collection while iterating.
                var placeholderIds = new System.Collections.Generic.List<long>();

                // Identify placeholder shapes based on name or text content.
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape's universal name contains "Placeholder".
                    bool isNamePlaceholder = !string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Placeholder", StringComparison.OrdinalIgnoreCase);

                    // Retrieve plain text of the shape; handle possible nulls safely.
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;
                    bool isTextPlaceholder = shapeText.Contains("[Logo]", StringComparison.OrdinalIgnoreCase);

                    // If either condition matches, mark this shape for replacement.
                    if (isNamePlaceholder || isTextPlaceholder)
                    {
                        placeholderIds.Add(shape.ID);
                    }
                }

                // Replace each identified placeholder with the logo image.
                foreach (long placeholderId in placeholderIds)
                {
                    // Retrieve the placeholder shape to obtain its geometry.
                    Shape placeholder = page.Shapes.GetShape(placeholderId);

                    // Preserve position and size for the new image shape.
                    double pinX = placeholder.XForm.PinX.Value;
                    double pinY = placeholder.XForm.PinY.Value;
                    double width = placeholder.XForm.Width.Value;
                    double height = placeholder.XForm.Height.Value;

                    // Mark the placeholder shape for deletion.
                    placeholder.Del = BOOL.True;

                    // Insert the logo image at the same location and size.
                    using (FileStream fs = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                    {
                        // AddShape overload that accepts a stream creates a foreign (image) shape.
                        page.AddShape(pinX, pinY, width, height, fs);
                    }
                }
            }

            // Configure high‑resolution PNG export options.
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Set resolution to 300 DPI for high quality.
                Resolution = 300f,
                // Export only the first page (adjust PageIndex if needed).
                PageIndex = 0,
                // Export a single page.
                PageCount = 1
            };

            // Save the modified diagram as a PNG image.
            diagram.Save(outputPath, pngOptions);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}