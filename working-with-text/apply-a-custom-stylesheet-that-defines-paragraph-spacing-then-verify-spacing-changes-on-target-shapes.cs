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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // Create a custom stylesheet that will be applied to the page.
            // ------------------------------------------------------------
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;
            customStyle.Name = "CustomParagraphStyle";

            // (Optional) Define character style as part of the stylesheet.
            // Here we just set a sample font color.
            Aspose.Diagram.Char sampleChar = new Aspose.Diagram.Char();
            sampleChar.Color.Value = "#0000FF"; // Blue text
            sampleChar.IX = 0;
            customStyle.Chars.Add(sampleChar);

            // Add the stylesheet to the document.
            diagram.StyleSheets.Add(customStyle);

            // ------------------------------------------------------------
            // Apply the stylesheet to the first page of the diagram.
            // ------------------------------------------------------------
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            // ApplyStyle takes three style IDs: character, paragraph, and line.
            page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

            // ------------------------------------------------------------
            // Define the desired paragraph line spacing (in lines).
            // ------------------------------------------------------------
            double targetSpacing = 2.0; // Example: double line spacing

            // ------------------------------------------------------------
            // Apply paragraph spacing to each shape on the page.
            // ------------------------------------------------------------
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Iterate over all paragraphs of the shape.
                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    // Set the paragraph line spacing.
                    shape.Paras[i].SpLine.Value = targetSpacing;
                }
            }

            // ------------------------------------------------------------
            // Verify that the paragraph spacing was applied correctly.
            // ------------------------------------------------------------
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue;

                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    double actualSpacing = shape.Paras[i].SpLine.Value;
                    if (Math.Abs(actualSpacing - targetSpacing) > 0.001)
                    {
                        Console.WriteLine($"[Mismatch] Shape ID {shape.ID}, Paragraph {i}: Spacing = {actualSpacing}");
                    }
                    else
                    {
                        Console.WriteLine($"[OK] Shape ID {shape.ID}, Paragraph {i}: Spacing correctly set to {actualSpacing}");
                    }
                }
            }

            // ------------------------------------------------------------
            // Save the modified diagram to the specified output path.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}