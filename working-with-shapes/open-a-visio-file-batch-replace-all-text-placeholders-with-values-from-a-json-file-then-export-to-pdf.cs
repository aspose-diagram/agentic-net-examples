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
        try
        {

            // Input Visio file path
            string visioPath = "input.vsdx";
            // JSON file containing placeholder replacements
            string jsonPath = "placeholders.json";
            // Output PDF file path
            string pdfOutputPath = "output.pdf";

            // Load JSON into a dictionary
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"JSON file not found: {jsonPath}");
            string jsonContent = File.ReadAllText(jsonPath);
            Dictionary<string, string> replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
            if (replacements == null)
                throw new Exception("Failed to deserialize JSON replacements.");

            // Load the Visio diagram
            if (!File.Exists(visioPath))
                throw new FileNotFoundException($"Visio file not found: {visioPath}");
            Diagram diagram = new Diagram(visioPath);

            // Iterate all pages and shapes to replace placeholders
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string shapeText = shape.Text.Value.Text;
                    if (string.IsNullOrWhiteSpace(shapeText))
                        continue; // Skip shapes without text

                    string updatedText = shapeText;

                    // Perform batch replacements
                    foreach (KeyValuePair<string, string> kvp in replacements)
                    {
                        // Assuming placeholders are in the form {{Key}}
                        string placeholder = $"{{{{{kvp.Key}}}}}";
                        if (updatedText.Contains(placeholder))
                        {
                            updatedText = updatedText.Replace(placeholder, kvp.Value);
                        }
                    }

                    // If text changed, update the shape
                    if (updatedText != shapeText)
                    {
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(updatedText));
                    }
                }
            }

            // Save the modified diagram as PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // Fallback font
            diagram.Save(pdfOutputPath, pdfOptions);

            Console.WriteLine("Placeholder replacement completed and PDF saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
