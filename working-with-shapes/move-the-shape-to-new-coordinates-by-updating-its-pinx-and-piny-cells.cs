using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        // Expected: inputPath shapeName newPinX newPinY outputPath
        if (args.Length != 5)
        {
            Console.Error.WriteLine("Usage: <inputPath> <shapeName> <newPinX> <newPinY> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string shapeName = args[1];
        string pinXStr = args[2];
        string pinYStr = args[3];
        string outputPath = args[4];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the new coordinate values.
        if (!double.TryParse(pinXStr, out double newPinX))
        {
            Console.Error.WriteLine($"Invalid PinX value: {pinXStr}");
            return;
        }

        if (!double.TryParse(pinYStr, out double newPinY))
        {
            Console.Error.WriteLine($"Invalid PinY value: {pinYStr}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the target shape by its NameU.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Compare the universal name of the shape.
                    if (shape.NameU == shapeName)
                    {
                        // Update the shape's position.
                        shape.XForm.PinX.Value = newPinX;
                        shape.XForm.PinY.Value = newPinY;

                        shapeFound = true;
                        // Assuming shape names are unique; break out of loops.
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with name \"{shapeName}\" not found.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Shape \"{shapeName}\" moved to ({newPinX}, {newPinY}) and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}