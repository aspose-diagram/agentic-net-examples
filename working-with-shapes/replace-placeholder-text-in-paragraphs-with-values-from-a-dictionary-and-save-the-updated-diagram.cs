using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
        // Keys are placeholders to search for; values are the replacement text.
        Dictionary<string, string> replacements = new Dictionary<string, string>()
        {
            { "{CompanyName}", "Contoso Ltd." },
            { "{Date}", DateTime.Today.ToShortDateString() },
            { "{Author}", "John Doe" }
        };

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Iterate over each text fragment (FormatTxt) within the shape's text collection.
                        foreach (Aspose.Diagram.FormatTxt fmt in shape.Text.Value)
                        {
                            // Cast the fragment to Txt to access the actual text string.
                            Aspose.Diagram.Txt txt = fmt as Aspose.Diagram.Txt;
                            if (txt == null)
                                continue; // Skip non‑text fragments.

                            // Perform placeholder replacement for each dictionary entry.
                            foreach (KeyValuePair<string, string> kvp in replacements)
                            {
                                if (txt.Text != null && txt.Text.Contains(kvp.Key))
                                {
                                    txt.Text = txt.Text.Replace(kvp.Key, kvp.Value);
                                }
                            }
                        }
                    }
                }

                // Save the updated diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}