using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ParagraphSerializationExample
{
    // DTO representing a paragraph's formatting properties
    public class ParagraphDto
    {
        public string HorzAlign { get; set; }
        public double IndLeft { get; set; }
        public double IndRight { get; set; }
        public double IndFirst { get; set; }
        public double SpBefore { get; set; }
        public double SpAfter { get; set; }
        public double SpLine { get; set; }
        public string Bullet { get; set; }
        public string BulletStr { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Choose the page and shape you want to serialize paragraphs from
                // Here we use the first page and the first shape as an example
                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                // Retrieve the shape (by index)
                Shape shape = page.Shapes[0];

                // Collect paragraph information
                List<ParagraphDto> paragraphList = new List<ParagraphDto>();
                foreach (Para para in shape.Paras)
                {
                    ParagraphDto dto = new ParagraphDto
                    {
                        HorzAlign = para.HorzAlign.Value.ToString(),
                        IndLeft = para.IndLeft.Value,
                        IndRight = para.IndRight.Value,
                        IndFirst = para.IndFirst.Value,
                        SpBefore = para.SpBefore.Value,
                        SpAfter = para.SpAfter.Value,
                        SpLine = para.SpLine.Value,
                        Bullet = para.Bullet.Value.ToString(),
                        BulletStr = para.BulletStr.Value
                    };
                    paragraphList.Add(dto);
                }

                // Serialize the collection to JSON
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(paragraphList, options);

                // Output JSON to console
                Console.WriteLine("Serialized Paragraph Collection:");
                Console.WriteLine(json);

                // Optionally write JSON to a file
                string outputPath = "paragraphs.json";
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"JSON saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}