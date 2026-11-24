using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, create a new file name with suffix "_modified".
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the page named "Details". Page.NameU holds the universal name.
                Page targetPage = null;
                foreach (Page page in diagram.Pages)
                {
                    if (page.NameU == "Details")
                    {
                        targetPage = page;
                        break;
                    }
                }

                // If the page is not found, inform the user and exit.
                if (targetPage == null)
                {
                    Console.Error.WriteLine("Page named \"Details\" was not found in the diagram.");
                    return;
                }

                // Iterate over all shapes on the target page.
                foreach (Shape shape in targetPage.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Move the shape 20 units to the right by adjusting PinX.
                    // PinX is a DoubleValue; its .Value property holds the numeric coordinate.
                    shape.XForm.PinX.Value = shape.XForm.PinX.Value + 20;
                }

                // Save the modified diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}