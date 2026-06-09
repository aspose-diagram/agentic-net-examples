using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";
            // Path where the JSON configuration will be saved
            string jsonOutputPath = "headerFooterConfig.json";

            // Load the diagram using the provided constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Extract header and footer text values
                var headerFooterConfig = new
                {
                    HeaderLeft   = diagram.HeaderFooter.HeaderLeft,
                    HeaderCenter = diagram.HeaderFooter.HeaderCenter,
                    HeaderRight  = diagram.HeaderFooter.HeaderRight,
                    FooterLeft   = diagram.HeaderFooter.FooterLeft,
                    FooterCenter = diagram.HeaderFooter.FooterCenter,
                    FooterRight  = diagram.HeaderFooter.FooterRight
                };

                // Serialize the extracted values to formatted JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(headerFooterConfig, jsonOptions);

                // Write the JSON string to the output file
                File.WriteAllText(jsonOutputPath, json);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
