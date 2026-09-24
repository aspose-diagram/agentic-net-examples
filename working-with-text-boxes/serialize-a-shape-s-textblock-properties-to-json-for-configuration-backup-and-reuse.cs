using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class TextBlockConfig
{
    public double LeftMargin { get; set; }
    public double RightMargin { get; set; }
    public double TopMargin { get; set; }
    public double BottomMargin { get; set; }
    public string TextDirection { get; set; }
    public string VerticalAlign { get; set; }
    public string TextBackground { get; set; }
    public double TextBackgroundTransparency { get; set; }
    public double DefaultTabStop { get; set; }
}

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("No pages found in the diagram.");
                return;
            }

            var page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Retrieve a shape (example: the first shape with ID 1)
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 1 not found.");
                return;
            }

            // Extract TextBlock properties
            var config = new TextBlockConfig
            {
                LeftMargin = shape.TextBlock.LeftMargin.Value,
                RightMargin = shape.TextBlock.RightMargin.Value,
                TopMargin = shape.TextBlock.TopMargin.Value,
                BottomMargin = shape.TextBlock.BottomMargin.Value,
                TextDirection = shape.TextBlock.TextDirection.Value.ToString(),
                VerticalAlign = shape.TextBlock.VerticalAlign.Value.ToString(),
                TextBackground = shape.TextBlock.TextBkgnd.Ufe.F,
                TextBackgroundTransparency = shape.TextBlock.TextBkgndTrans.Value,
                DefaultTabStop = shape.TextBlock.DefaultTabStop.Value
            };

            // Serialize configuration to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(config, jsonOptions);

            // Save JSON to a file
            string outputPath = "textblock_config.json";
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"TextBlock configuration saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}