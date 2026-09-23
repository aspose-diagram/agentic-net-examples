using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

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
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access header and footer text values
            var hf = diagram.HeaderFooter;

            // Populate configuration object
            var config = new HeaderFooterConfig
            {
                HeaderLeft = hf.HeaderLeft,
                HeaderCenter = hf.HeaderCenter,
                HeaderRight = hf.HeaderRight,
                FooterLeft = hf.FooterLeft,
                FooterCenter = hf.FooterCenter,
                FooterRight = hf.FooterRight
            };

            // Serialize to JSON with indentation
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, jsonOptions);

            // Write JSON to file
            string jsonPath = "headerFooterConfig.json";
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Header and footer values have been exported to {jsonPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}