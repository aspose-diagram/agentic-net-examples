using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all page‑level annotations (comments).
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Retrieve the shape ID that the comment is attached to.
                        int shapeId = ann.ShapeID;

                        // Attempt to get the associated shape on the current page.
                        Shape shape = null;
                        try
                        {
                            shape = page.Shapes.GetShape(shapeId);
                        }
                        catch
                        {
                            // If the shape cannot be found, continue to the next annotation.
                            shape = null;
                        }

                        // Original comment text.
                        string originalComment = ann.Comment.Value ?? string.Empty;

                        // Translate the comment text using an external service (placeholder implementation).
                        string translatedComment = TranslateComment(originalComment);

                        // Update the annotation with the translated text.
                        ann.Comment.Value = translatedComment;

                        // (Optional) You could also log which shape was updated.
                        // Console.WriteLine($"Updated comment for shape ID {shapeId} on page '{page.Name}'.");
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Placeholder for a real translation service.
    /// Replace this method with actual API calls to a translation provider.
    /// </summary>
    /// <param name="text">The text to translate.</param>
    /// <returns>The translated text.</returns>
    private static string TranslateComment(string text)
    {
        // Simple mock translation – append a suffix.
        return $"{text} (translated)";
    }
}