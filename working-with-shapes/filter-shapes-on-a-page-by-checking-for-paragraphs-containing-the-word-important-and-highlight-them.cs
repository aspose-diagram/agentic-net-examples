using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    bool containsImportant = false;

                    // Check each text run (Txt) within the shape for the keyword "Important"
                    // shape.Text.Value is a collection of FormatTxt objects; Txt objects hold the actual text.
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        Txt txt = fmt as Txt;
                        if (txt != null && txt.Text != null && txt.Text.IndexOf("Important", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            containsImportant = true;
                            break;
                        }
                    }

                    // If the shape contains the keyword, highlight it by setting a yellow fill color
                    if (containsImportant)
                    {
                        // Ensure the Fill object exists before setting its color
                        if (shape.Fill != null && shape.Fill.FillForegnd != null)
                        {
                            // Yellow color in hexadecimal RGB format
                            shape.Fill.FillForegnd.Value = "#FFFF00";
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}