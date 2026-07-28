using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio files
            string inputFile = "input.vsdx";
            string outputFile = "output.vsdx";

            // Load the diagram using the constructor that accepts a file path
            using (Diagram diagram = new Diagram(inputFile))
            {
                // Dictionary containing placeholder keys and their replacement values
                var replacements = new Dictionary<string, string>
                {
                    { "{Name}", "John Doe" },
                    { "{Date}", DateTime.Today.ToString("yyyy-MM-dd") },
                    { "{Company}", "Acme Corp" }
                };

                // Iterate through all pages and shapes, replacing placeholders where found
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (KeyValuePair<string, string> kvp in replacements)
                        {
                            // Replace placeholder text with the corresponding value
                            shape.ReplaceText(kvp.Key, kvp.Value);
                        }
                    }
                }

                // Save the updated diagram using the Save method with a file format enum
                diagram.Save(outputFile, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
