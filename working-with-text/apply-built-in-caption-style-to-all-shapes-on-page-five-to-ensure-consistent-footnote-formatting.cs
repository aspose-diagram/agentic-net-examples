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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least five pages (zero‑based index 4).
                if (diagram.Pages.Count < 5)
                {
                    Console.Error.WriteLine("The diagram does not contain a page five (index 4).");
                    return;
                }

                // Retrieve page five (index 4).
                Page pageFive = diagram.Pages[4];

                // Locate the built‑in style sheet named "Caption".
                StyleSheet captionStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    // NameU holds the universal name of the style.
                    if (ss.NameU == "Caption")
                    {
                        captionStyle = ss;
                        break;
                    }
                }

                if (captionStyle == null)
                {
                    Console.Error.WriteLine("Style sheet 'Caption' was not found in the document.");
                    return;
                }

                // Apply the 'Caption' style to every non‑deleted shape on page five.
                foreach (Shape shape in pageFive.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Assign the style to text, fill, and line categories.
                    shape.TextStyle = captionStyle;
                    shape.FillStyle = captionStyle;
                    shape.LineStyle = captionStyle;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Successfully applied 'Caption' style to all shapes on page five and saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}