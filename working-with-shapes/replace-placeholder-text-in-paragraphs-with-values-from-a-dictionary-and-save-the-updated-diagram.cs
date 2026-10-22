using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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

        // Define placeholder replacements.
        var replacements = new Dictionary<string, string>
        {
            { "{Name}", "John Doe" },
            { "{Date}", DateTime.Today.ToString("yyyy-MM-dd") },
            // Add more placeholder‑value pairs as needed.
        };

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each text fragment (FormatTxt) in the shape's text collection.
                    foreach (FormatTxt fmt in shape.Text.Value)
                    {
                        // Cast the fragment to Txt to access the actual text.
                        Txt txt = fmt as Txt;
                        if (txt != null && txt.Text != null)
                        {
                            // Apply all replacements to the current text fragment.
                            foreach (KeyValuePair<string, string> kvp in replacements)
                            {
                                if (txt.Text.Contains(kvp.Key))
                                {
                                    txt.Text = txt.Text.Replace(kvp.Key, kvp.Value);
                                }
                            }
                        }
                    }
                }
            }

            // Save the updated diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to free resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}