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

                // Paths – adjust as needed
                string visioPath = "input.vsdx";
                string jsonPath = "replacements.json";
                string pdfOutputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Read and deserialize the JSON file containing placeholder-value pairs
                string jsonContent = File.ReadAllText(jsonPath);
                Dictionary<string, string> replacements = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);

                // Iterate through all pages and shapes to replace text
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate over each text run within the shape
                        foreach (var item in shape.Text.Value)
                        {
                            if (item is Txt txt)
                            {
                                string updatedText = txt.Text;
                                foreach (var kvp in replacements)
                                {
                                    if (updatedText.Contains(kvp.Key))
                                    {
                                        updatedText = updatedText.Replace(kvp.Key, kvp.Value);
                                    }
                                }
                                txt.Text = updatedText;
                            }
                        }
                    }
                }

                // Configure PDF save options (set a default font to avoid missing font issues)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the modified diagram as PDF
                diagram.Save(pdfOutputPath, pdfOptions);

                Console.WriteLine("Replacement completed and PDF saved to: " + pdfOutputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }