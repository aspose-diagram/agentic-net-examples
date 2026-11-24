using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <VisioFilePath> <JsonFilePath> <OutputPdfPath>
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <JsonFilePath> <OutputPdfPath>");
            return;
        }

        string visioPath = args[0];
        string jsonPath = args[1];
        string outputPdfPath = args[2];

        // Guard: ensure input Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard: ensure JSON file exists
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load replacement dictionary from JSON
        Dictionary<string, string> replacements;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
            if (replacements == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON into a dictionary.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading JSON file: {ex.Message}");
            return;
        }

        // Load the Visio diagram and perform text replacements
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through each text run (Txt) within the shape
                        foreach (FormatTxt fmt in shape.Text.Value)
                        {
                            if (fmt is Txt txt)
                            {
                                // Perform placeholder replacements
                                foreach (KeyValuePair<string, string> kvp in replacements)
                                {
                                    if (!string.IsNullOrEmpty(txt.Text) && txt.Text.Contains(kvp.Key))
                                    {
                                        txt.Text = txt.Text.Replace(kvp.Key, kvp.Value);
                                    }
                                }
                            }
                        }
                    }
                }

                // Prepare PDF save options (default options are sufficient)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Export the modified diagram to PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"PDF exported successfully to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}