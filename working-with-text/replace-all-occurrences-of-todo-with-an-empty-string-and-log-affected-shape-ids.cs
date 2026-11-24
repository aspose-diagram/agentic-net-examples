using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            // Append suffix to original file name
            string dir = Path.GetDirectoryName(inputPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_modified.vsdx");
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has text collection
                        if (shape.Text == null || shape.Text.Value == null)
                            continue;

                        bool shapeModified = false;

                        // Iterate through each text element within the shape
                        foreach (FormatTxt fmt in shape.Text.Value)
                        {
                            Txt txt = fmt as Txt;
                            if (txt != null && txt.Text != null && txt.Text.Contains("TODO"))
                            {
                                // Replace all occurrences of 'TODO' with empty string
                                txt.Text = txt.Text.Replace("TODO", string.Empty);
                                shapeModified = true;
                            }
                        }

                        // Log the shape ID if any replacement occurred
                        if (shapeModified)
                        {
                            Console.WriteLine($"Modified shape ID: {shape.ID}");
                        }
                    }
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}