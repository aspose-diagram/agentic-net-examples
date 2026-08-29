using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace ParagraphSerializationExample
{
    // DTO representing the serializable data of a paragraph
    public class ParagraphDto
    {
        public int Index { get; set; }
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

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Get the first shape on the page (adjust selection logic as needed)
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                Shape shape = page.Shapes[0];

                // Collect paragraph information
                List<ParagraphDto> paragraphs = new List<ParagraphDto>();

                for (int i = 0; i < shape.Paras.Count; i++)
                {
                    var para = shape.Paras[i];

                    ParagraphDto dto = new ParagraphDto
                    {
                        Index = i,
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

                    paragraphs.Add(dto);
                }

                // Serialize to JSON
                string json = JsonSerializer.Serialize(paragraphs, new JsonSerializerOptions { WriteIndented = true });

                // Output JSON (could be written to a file or sent to a service)
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}