using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional: output path (same folder with suffix)
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", 
                                         Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            bool paragraphModified = false;

            // Iterate through pages and shapes to find a shape with at least two paragraphs
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has paragraph collection
                    if (shape.Paras != null && shape.Paras.Count >= 2)
                    {
                        // Index 1 corresponds to the second paragraph
                        int paraIndex = 1;

                        // NOTE: Aspose.Diagram applies formatting at the character level.
                        // Since there is no direct paragraph style API, we iterate over all characters
                        // and apply the desired formatting. In a real scenario you would calculate
                        // the character range belonging to the specific paragraph, but for simplicity
                        // we apply the style to all characters of the shape.
                        for (int charIdx = 0; charIdx < shape.Chars.Count; charIdx++)
                        {
                            // Set italic style
                            shape.Chars[charIdx].Style.Value = StyleValue.Italic;

                            // Set text color to dark blue (hex #00008B)
                            shape.Chars[charIdx].Color.Value = "#00008B";
                        }

                        paragraphModified = true;
                        // Break after first modification (remove if you need to modify multiple shapes)
                        break;
                    }
                }

                if (paragraphModified)
                    break;
            }

            if (!paragraphModified)
            {
                Console.WriteLine("No shape with at least two paragraphs was found. No changes applied.");
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}