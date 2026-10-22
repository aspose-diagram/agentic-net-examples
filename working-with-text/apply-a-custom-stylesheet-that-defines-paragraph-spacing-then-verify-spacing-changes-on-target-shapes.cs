using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom stylesheet that defines paragraph line spacing.
                // ------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();

                // Assign a unique ID for the stylesheet.
                customStyle.ID = diagram.StyleSheets.Count + 1;

                // Optional: give the stylesheet a name for readability.
                customStyle.Name = "CustomParagraphSpacing";

                // Define a paragraph with desired line spacing (e.g., 2.0 lines).
                Para para = new Para();
                para.SpLine.Value = 2.0; // Set spacing to 2 lines.

                // Add the paragraph definition to the stylesheet.
                customStyle.Paras.Add(para);

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(customStyle);

                // ------------------------------------------------------------
                // Apply the stylesheet to the first page of the diagram.
                // ApplyStyle takes three style IDs: Char, Line, Fill.
                // Using the same ID for all three applies the full style.
                // ------------------------------------------------------------
                if (diagram.Pages.Count > 0)
                {
                    diagram.Pages[0].ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
                }
                else
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // ------------------------------------------------------------
                // Verification: ensure that paragraph spacing on target shapes
                // matches the value defined in the stylesheet (2.0 lines).
                // ------------------------------------------------------------
                const double expectedSpacing = 2.0;
                bool verificationFailed = false;

                Page firstPage = diagram.Pages[0];
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate over all paragraphs in the shape.
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        Para p = shape.Paras[i];
                        double actualSpacing = p.SpLine.Value;

                        if (Math.Abs(actualSpacing - expectedSpacing) > 0.0001)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} paragraph {i} spacing mismatch. Expected: {expectedSpacing}, Actual: {actualSpacing}");
                            verificationFailed = true;
                        }
                    }
                }

                if (verificationFailed)
                {
                    throw new Exception("Paragraph spacing verification failed. See errors above.");
                }
                else
                {
                    Console.WriteLine("Paragraph spacing verification succeeded.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
