using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -------------------------------------------------------------
                // 1. Create a custom StyleSheet (empty – Paragraph class not available).
                // -------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();

                // Assign a unique ID for the stylesheet.
                customStyle.ID = diagram.StyleSheets.Count + 1;

                // Add the stylesheet to the diagram.
                diagram.StyleSheets.Add(customStyle);

                // Apply the stylesheet to all pages (TextStyle, FillStyle, LineStyle).
                foreach (Page page in diagram.Pages)
                {
                    page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
                }

                // -------------------------------------------------------------
                // 2. Set line spacing (SpLine) to 1.5 on all multi‑line text shapes.
                // -------------------------------------------------------------
                const double ExpectedLineSpacing = 1.5;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True) continue;

                        // Retrieve plain text of the shape.
                        string plainText = shape.Text.Value.Text ?? string.Empty;

                        // Consider only shapes that contain a line‑break.
                        if (!plainText.Contains("\n")) continue;

                        // Set the desired line spacing for each paragraph.
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            shape.Paras[i].SpLine.Value = ExpectedLineSpacing;
                        }
                    }
                }

                // -------------------------------------------------------------
                // 3. Verify that multi‑line text shapes have the expected line spacing.
                // -------------------------------------------------------------
                bool allVerified = true;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True) continue;

                        // Retrieve plain text of the shape.
                        string plainText = shape.Text.Value.Text ?? string.Empty;

                        // Consider only shapes that contain a line‑break.
                        if (!plainText.Contains("\n")) continue;

                        // Check each paragraph's SpLine value.
                        bool shapeVerified = true;
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            double actualSpacing = shape.Paras[i].SpLine.Value;
                            if (Math.Abs(actualSpacing - ExpectedLineSpacing) > 0.001)
                            {
                                shapeVerified = false;
                                break;
                            }
                        }

                        if (!shapeVerified)
                        {
                            allVerified = false;
                            Console.WriteLine($"Shape ID {shape.ID} on Page {page.ID} has incorrect line spacing.");
                        }
                    }
                }

                if (allVerified)
                {
                    Console.WriteLine("All multi‑line text shapes have the expected line spacing.");
                }
                else
                {
                    Console.WriteLine("Some shapes did not match the expected line spacing. See details above.");
                }

                // -------------------------------------------------------------
                // 4. Save the modified diagram.
                // -------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
