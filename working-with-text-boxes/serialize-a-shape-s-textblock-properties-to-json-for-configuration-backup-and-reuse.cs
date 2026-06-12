using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramTextBlockBackup
{
    // DTO for serializing TextBlock properties
    public class TextBlockConfig
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

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file
                const string inputPath = "input.vsdx";
                // Path where the JSON configuration will be saved
                const string outputJsonPath = "textblock_config.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape (adjust the ID as needed)
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    Console.WriteLine("Shape with ID 1 not found.");
                    return;
                }

                // Extract TextBlock properties
                TextBlockConfig config = new TextBlockConfig
                {
                    LeftMargin = shape.TextBlock.LeftMargin.Value,
                    RightMargin = shape.TextBlock.RightMargin.Value,
                    TopMargin = shape.TextBlock.TopMargin.Value,
                    BottomMargin = shape.TextBlock.BottomMargin.Value,
                    TextDirection = shape.TextBlock.TextDirection.Value.ToString(),
                    VerticalAlign = shape.TextBlock.VerticalAlign.Value.ToString(),
                    TextBackground = shape.TextBlock.TextBkgnd?.Ufe?.F ?? string.Empty,
                    TextBackgroundTransparency = shape.TextBlock.TextBkgndTrans.Value,
                    DefaultTabStop = shape.TextBlock.DefaultTabStop.Value
                };

                // Serialize to JSON with indentation
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to file
                File.WriteAllText(outputJsonPath, json);

                Console.WriteLine($"TextBlock configuration saved to '{outputJsonPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}