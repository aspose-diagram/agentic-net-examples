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

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Read and deserialize the JSON file into a dictionary
            string jsonContent = File.ReadAllText(jsonPath);
            Dictionary<string, string> replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

            // Iterate through all pages and shapes to replace placeholder text
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    if (string.IsNullOrWhiteSpace(shapeText))
                        continue;

                    // Perform replacements
                    bool changed = false;
                    foreach (KeyValuePair<string, string> kvp in replacements)
                    {
                        if (shapeText.Contains(kvp.Key))
                        {
                            shapeText = shapeText.Replace(kvp.Key, kvp.Value);
                            changed = true;
                        }
                    }

                    // If any replacement occurred, update the shape's text
                    if (changed)
                    {
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(shapeText));
                    }
                }
            }

            // Save the modified diagram as PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine("Export completed successfully.");
        }
    }