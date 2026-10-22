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
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <jsonPath> <outputPdfPath>");
            return;
        }

        string inputVisioPath = args[0];
        string jsonPath = args[1];
        string outputPdfPath = args[2];

        // Guard against missing input files.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {inputVisioPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load replacement values from JSON.
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

        // Load the Visio diagram and perform text replacements.
        try
        {
            // Use a using block as required by the API.
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through each text run within the shape.
                        foreach (var fmt in shape.Text.Value)
                        {
                            if (fmt is Txt txt)
                            {
                                string text = txt.Text;

                                // Apply each replacement.
                                foreach (var kvp in replacements)
                                {
                                    // Support placeholders like {{Key}} and plain Key.
                                    string placeholder = $"{{{{{kvp.Key}}}}}";
                                    if (text.Contains(placeholder))
                                    {
                                        text = text.Replace(placeholder, kvp.Value);
                                    }
                                    else if (text.Contains(kvp.Key))
                                    {
                                        text = text.Replace(kvp.Key, kvp.Value);
                                    }
                                }

                                // Update the text run with the new content.
                                txt.Text = text;
                            }
                        }
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.ExportHiddenPage = false; // ExportHiddenPage expects a bool, not BOOL.

                // Save the modified diagram as PDF.
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"PDF successfully saved to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
