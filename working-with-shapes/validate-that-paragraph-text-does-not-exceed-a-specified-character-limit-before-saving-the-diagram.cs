using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, max characters per paragraph
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <maxChars>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string maxCharsArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: parse max characters limit
        if (!int.TryParse(maxCharsArg, out int maxChars) || maxChars < 0)
        {
            Console.Error.WriteLine($"Invalid maximum characters value: {maxCharsArg}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Accumulate the full text of the shape by iterating its FormatTxtCollection
                    string shapeText = string.Empty;
                    foreach (object item in shape.Text.Value)
                    {
                        if (item is Txt txt)
                        {
                            shapeText += txt.Text;
                        }
                    }

                    // If the shape has no text, continue to next shape
                    if (string.IsNullOrEmpty(shapeText))
                        continue;

                    // Validate each paragraph's text length against the limit
                    // Paragraphs themselves do not store text directly; we use the accumulated shape text
                    // Split the text by line breaks to approximate paragraph boundaries
                    string[] paragraphs = shapeText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    for (int i = 0; i < paragraphs.Length; i++)
                    {
                        string paraText = paragraphs[i];
                        if (paraText.Length > maxChars)
                        {
                            Console.Error.WriteLine($"Paragraph {i + 1} in shape ID {shape.ID} exceeds the limit of {maxChars} characters (actual: {paraText.Length}).");
                            return; // Abort saving due to validation failure
                        }
                    }
                }
            }

            // All paragraphs are within the limit; proceed to save the diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}