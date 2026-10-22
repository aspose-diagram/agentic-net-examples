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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least two pages.
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram does not contain a second page.");
                    return;
                }

                // ------------------------------------------------------------
                // Create a custom stylesheet that will be used for text alignment.
                // ------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();
                // Assign a unique ID (Visio IDs start at 1).
                customStyle.ID = diagram.StyleSheets.Count + 1;

                // The stylesheet can hold character formatting; we only need it
                // to be present so that we can apply it to the page.
                // (No additional character or fill settings are required here.)

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(customStyle);

                // ------------------------------------------------------------
                // Apply the stylesheet to page two (index 1).
                // ------------------------------------------------------------
                Page pageTwo = diagram.Pages[1];
                // ApplyStyle(lineStyleId, fillStyleId, textStyleId)
                pageTwo.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);

                // ------------------------------------------------------------
                // Align all paragraph texts to center on page two.
                // ------------------------------------------------------------
                foreach (Shape shape in pageTwo.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through each paragraph in the shape.
                    for (int i = 0; i < shape.Paras.Count; i++)
                    {
                        // Set horizontal alignment to center.
                        shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}