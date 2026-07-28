using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace HeaderFooterExtractor
{
    // DTO for JSON serialization
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
        public static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string diagramPath = "input.vsdx";

                // Output JSON configuration file path
                string jsonPath = "headerFooterConfig.json";

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
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonContent = JsonSerializer.Serialize(config, jsonOptions);

                // Write JSON to file
                File.WriteAllText(jsonPath, jsonContent);

                Console.WriteLine($"Header and footer values have been exported to '{jsonPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}