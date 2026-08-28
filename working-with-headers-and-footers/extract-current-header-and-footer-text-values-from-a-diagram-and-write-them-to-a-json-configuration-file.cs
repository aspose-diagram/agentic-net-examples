using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace HeaderFooterExtractor
{
    // DTO for JSON configuration
    public class HeaderFooterConfig
    {
        public string HeaderLeft { get; set; }
        public string HeaderCenter { get; set; }
        public string HeaderRight { get; set; }
        public string FooterLeft { get; set; }
        public string FooterCenter { get; set; }
        public string FooterRight { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                const string diagramPath = "input.vsdx";

                // Path for the output JSON configuration file
                const string outputJsonPath = "headerFooterConfig.json";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Extract header and footer text values
                HeaderFooterConfig config = new HeaderFooterConfig
                {
                    HeaderLeft = diagram.HeaderFooter.HeaderLeft,
                    HeaderCenter = diagram.HeaderFooter.HeaderCenter,
                    HeaderRight = diagram.HeaderFooter.HeaderRight,
                    FooterLeft = diagram.HeaderFooter.FooterLeft,
                    FooterCenter = diagram.HeaderFooter.FooterCenter,
                    FooterRight = diagram.HeaderFooter.FooterRight
                };

                // Serialize to JSON with indentation for readability
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(outputJsonPath, json);

                Console.WriteLine($"Header and footer configuration saved to '{outputJsonPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}