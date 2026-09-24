using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";
                // Path where the updated diagram will be saved
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Dictionary containing placeholder keys and their replacement values
                var replacements = new Dictionary<string, string>
                {
                    { "{CompanyName}", "Acme Corp" },
                    { "{Date}", DateTime.Today.ToShortDateString() },
                    { "{Author}", "John Doe" }
                    // Add more placeholder-value pairs as needed
                };

                // Iterate through all pages and shapes to replace placeholder text
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the plain text of the shape
                        string currentText = shape.Text.Value.Text;

                        // Skip shapes without text
                        if (string.IsNullOrWhiteSpace(currentText))
                            continue;

                        string updatedText = currentText;

                        // Perform replacements for each placeholder
                        foreach (KeyValuePair<string, string> kvp in replacements)
                        {
                            if (updatedText.Contains(kvp.Key))
                            {
                                updatedText = updatedText.Replace(kvp.Key, kvp.Value);
                            }
                        }

                        // If any replacement occurred, update the shape's text
                        if (!updatedText.Equals(currentText))
                        {
                            // Clear existing text runs
                            shape.Text.Value.Clear();
                            // Add the new text as a single text run
                            shape.Text.Value.Add(new Txt(updatedText));
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }