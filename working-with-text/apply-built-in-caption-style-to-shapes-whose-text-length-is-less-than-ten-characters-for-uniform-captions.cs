using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the built‑in "Caption" style sheet once
                StyleSheet captionStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Caption")
                    {
                        captionStyle = ss;
                        break;
                    }
                }

                if (captionStyle == null)
                {
                    Console.Error.WriteLine("StyleSheet named 'Caption' was not found in the document.");
                    // Continue without applying style
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape
                        string text = shape.Text.Value.ToString();

                        // Apply the Caption style if text length is less than 10 characters
                        if (!string.IsNullOrEmpty(text) && text.Length < 10 && captionStyle != null)
                        {
                            shape.TextStyle = captionStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}