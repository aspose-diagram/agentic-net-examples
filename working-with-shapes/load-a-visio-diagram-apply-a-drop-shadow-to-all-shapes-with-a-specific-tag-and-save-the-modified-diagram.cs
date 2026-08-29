using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect: inputPath outputPath tagName [tagValue]
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <tagName> [tagValue]");
            return;
        }

        // Assign command‑line arguments to variables
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string tagName = args[2];
        string tagValue = args.Length >= 4 ? args[3] : null;

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion
                    if (shape.Del == BOOL.True) continue;

                    // Ensure the shape has a Props collection before searching
                    if (shape.Props == null) continue;

                    bool tagMatched = false;

                    // Search for a custom property (Prop) that matches the requested tag
                    foreach (Prop prop in shape.Props)
                    {
                        // Compare property name (case‑insensitive) and, if a value was supplied, also compare the value
                        if (string.Equals(prop.Name, tagName, StringComparison.OrdinalIgnoreCase) &&
                            (tagValue == null || string.Equals(prop.Value.Val, tagValue, StringComparison.OrdinalIgnoreCase)))
                        {
                            tagMatched = true;
                            break;
                        }
                    }

                    // If the shape does not have the required tag, move to the next shape
                    if (!tagMatched) continue;

                    // ---- Apply a simple drop shadow to the shape ----
                    // Enable simple shadow type
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                    // Set shadow color to semi‑transparent black
                    shape.Fill.ShdwForegnd.Value = "#000000";
                    shape.Fill.ShdwForegndTrans.Value = 0.3; // 30 % transparent
                    // Define shadow offset (in inches)
                    shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                    shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}