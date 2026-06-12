using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace ParagraphSerializationExample
{
    // DTO representing the data we want to export for each paragraph
    public class ParagraphInfo
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

                // Load the Visio diagram (adjust the path as needed)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Choose the page and shape you want to serialize
                // Here we take the first page and the first shape on that page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(1);

                // Collect paragraph information
                List<ParagraphInfo> paragraphs = new List<ParagraphInfo>();
                foreach (Para para in shape.Paras)
                {
                    ParagraphInfo info = new ParagraphInfo
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
                    paragraphs.Add(info);
                }

                // Serialize to JSON
                string json = JsonSerializer.Serialize(paragraphs, new JsonSerializerOptions { WriteIndented = true });

                // Output JSON to console (or write to a file if desired)
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}