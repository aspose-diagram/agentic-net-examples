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
            // Expect three arguments: input Visio file, JSON file with replacements, output PDF file
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: VisioBatchReplace <input.vsdx> <replacements.json> <output.pdf>");
                return;
            }

            string inputVisioPath = args[0];
            string jsonPath = args[1];
            string outputPdfPath = args[2];

            // Validate input files
            if (!File.Exists(inputVisioPath))
                throw new FileNotFoundException($"Visio file not found: {inputVisioPath}");
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"JSON file not found: {jsonPath}");

            // Load replacement dictionary from JSON (expects {"placeholder":"value", ...})
            Dictionary<string, string> replacements;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
                if (replacements == null)
                    throw new Exception("Failed to deserialize JSON into a dictionary.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON file: {ex.Message}");
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputVisioPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading Visio file: {ex.Message}");
            }

            // Iterate through all pages and shapes to replace text
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get current plain text of the shape
                    string currentText = shape.Text.Value.Text;
                    if (string.IsNullOrWhiteSpace(currentText))
                        continue;

                    string newText = currentText;

                    // Perform replacements for each placeholder
                    foreach (KeyValuePair<string, string> kvp in replacements)
                    {
                        // Assuming placeholders are in the format {{key}}
                        string placeholder = $"{{{{{kvp.Key}}}}}";
                        if (newText.Contains(placeholder))
                        {
                            newText = newText.Replace(placeholder, kvp.Value);
                        }
                    }

                    // If text changed, update the shape's text
                    if (newText != currentText)
                    {
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(newText));
                    }
                }
            }

            // Export the modified diagram to PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // Fallback font

            try
            {
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"PDF exported successfully to: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving PDF: {ex.Message}");
            }
        }
    }