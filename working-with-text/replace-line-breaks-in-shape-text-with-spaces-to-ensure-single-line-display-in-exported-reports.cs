using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Text object
                    if (shape.Text == null)
                        continue;

                    // Iterate through each text element (FormatTxt) in the shape's text collection
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Cast to Txt to access the actual text string
                        if (fmt is Txt txt)
                        {
                            // Replace any line break characters with a single space
                            string updated = txt.Text
                                .Replace("\r\n", " ")
                                .Replace("\n", " ")
                                .Replace("\r", " ");

                            // Assign the modified text back to the shape
                            txt.Text = updated;
                        }
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}