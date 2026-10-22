using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_modified.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block (mandatory pattern)
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains any text
                        if (shape.Text != null && shape.Text.Value != null && shape.Text.Value.Count > 0)
                        {
                            // Clear existing text
                            shape.Text.Value.Clear();

                            // Add new sample text
                            shape.Text.Value.Add(new Txt("Sample Text"));

                            // Apply character-level formatting to all characters
                            for (int i = 0; i < shape.Chars.Count; i++)
                            {
                                Aspose.Diagram.Char ch = shape.Chars[i];
                                ch.FontName.Value = "Arial";                     // Set font name
                                ch.Size.Value = 12.0 / 72.0;                     // Set font size (points to inches)
                                ch.Style.Value = StyleValue.Bold;                // Apply bold style
                                ch.Color.Value = "#FF0000";                      // Set text color to red (hex string)
                            }

                            // Apply paragraph-level formatting (center alignment)
                            for (int i = 0; i < shape.Paras.Count; i++)
                            {
                                shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing or saving
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}