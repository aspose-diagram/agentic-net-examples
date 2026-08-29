using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the input Visio file path.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the output file path (original name with suffix).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", 
                                         Path.GetFileNameWithoutExtension(inputPath) + "_styled.vsdx");

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Create a new stylesheet that will define the desired line spacing.
            StyleSheet style = new StyleSheet();
            // Assign a unique ID based on the current count of stylesheets.
            style.ID = diagram.StyleSheets.Count + 1;

            // Configure paragraph formatting: set line spacing (SpLine) to 0.2 inches.
            // SpLine.Value represents the line spacing multiplier; 0.2 is a typical value.
            Aspose.Diagram.Para para = new Aspose.Diagram.Para();
            para.SpLine.Value = 0.2; // 0.2 inches line spacing
            style.Paras.Add(para);

            // Add the stylesheet to the diagram's collection.
            diagram.StyleSheets.Add(style);

            // Apply the stylesheet to all shapes on each page.
            foreach (Page page in diagram.Pages)
            {
                // Apply the style to the page; the three parameters are style IDs for
                // character, line, and fill styles respectively. Using the same ID for all.
                page.ApplyStyle(style.ID, style.ID, style.ID);
            }

            // Expected line spacing value for verification.
            double expectedLineSpacing = 0.2;

            // Verify that each shape containing multi‑line text respects the line spacing.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string plainText = shape.Text.Value.Text;

                    // Check if the shape contains more than one line (i.e., newline characters).
                    if (!string.IsNullOrEmpty(plainText) && plainText.Contains("\n"))
                    {
                        // Ensure the shape has at least one paragraph to inspect.
                        if (shape.Paras.Count > 0)
                        {
                            // Get the line spacing from the first paragraph.
                            double actualSpacing = shape.Paras[0].SpLine.Value;

                            // Compare the actual spacing with the expected value (tolerance 0.001).
                            if (Math.Abs(actualSpacing - expectedLineSpacing) < 0.001)
                            {
                                Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' passes line spacing verification.");
                            }
                            else
                            {
                                Console.Error.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' fails verification. Expected {expectedLineSpacing}, found {actualSpacing}.");
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has no paragraphs to verify.");
                        }
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Styled diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}