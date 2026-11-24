using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text.
                        if (shape.Text != null && shape.Text.Value != null && shape.Text.Value.Count > 0)
                        {
                            // Apply character formatting if there are character runs.
                            if (shape.Chars != null && shape.Chars.Count > 0)
                            {
                                for (int i = 0; i < shape.Chars.Count; i++)
                                {
                                    // Set font name.
                                    shape.Chars[i].FontName.Value = "Arial";

                                    // Set font size (points are converted to inches internally; 12 points = 12/72 inches).
                                    shape.Chars[i].Size.Value = 12.0 / 72.0;

                                    // Set font color (black) using a hex string.
                                    shape.Chars[i].Color.Value = "#000000";

                                    // Apply bold style.
                                    shape.Chars[i].Style.Value = StyleValue.Bold;
                                }
                            }

                            // Apply paragraph line spacing if there are paragraphs.
                            if (shape.Paras != null && shape.Paras.Count > 0)
                            {
                                for (int p = 0; p < shape.Paras.Count; p++)
                                {
                                    // Set line spacing to 1.5 lines.
                                    shape.Paras[p].SpLine.Value = 1.5;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the new file using the two‑argument overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}