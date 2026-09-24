using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramParagraphUpdater
{
    // DTO representing a paragraph's formatting properties
    public class ParagraphDto
    {
        public string HorzAlign { get; set; }
        public double? IndLeft { get; set; }
        public double? IndRight { get; set; }
        public double? IndFirst { get; set; }
        public double? SpBefore { get; set; }
        public double? SpAfter { get; set; }
        public double? SpLine { get; set; }
        public string Bullet { get; set; }
        public string BulletStr { get; set; }
    }

    // DTO representing the shape to update
    public class ShapeUpdateDto
    {
        public long ShapeId { get; set; }
        public int PageIndex { get; set; }
        public List<ParagraphDto> Paragraphs { get; set; } = new();
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path to the JSON file containing paragraph data
                string jsonPath = "paragraphs.json";

                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Read and deserialize JSON
                string jsonContent = File.ReadAllText(jsonPath);
                List<ShapeUpdateDto> updates = JsonSerializer.Deserialize<List<ShapeUpdateDto>>(jsonContent);

                if (updates == null)
                    throw new Exception("Failed to deserialize JSON.");

                foreach (var update in updates)
                {
                    // Validate page index
                    if (update.PageIndex < 0 || update.PageIndex >= diagram.Pages.Count)
                    {
                        Console.WriteLine($"Invalid page index {update.PageIndex} for shape ID {update.ShapeId}.");
                        continue;
                    }

                    Page page = diagram.Pages[update.PageIndex];

                    // Retrieve the shape by ID (cast to int as required by GetShape)
                    Shape shape = page.Shapes.GetShape((int)update.ShapeId);
                    if (shape == null)
                    {
                        Console.WriteLine($"Shape with ID {update.ShapeId} not found on page {update.PageIndex}.");
                        continue;
                    }

                    // Clear existing paragraphs
                    shape.Paras.Clear();

                    // Add new paragraphs based on DTO
                    foreach (var paraDto in update.Paragraphs)
                    {
                        Para para = new Para();

                        // HorzAlign
                        if (!string.IsNullOrEmpty(paraDto.HorzAlign))
                        {
                            // Map string to enum; default to LeftAlign if parsing fails
                            if (Enum.TryParse<HorzAlignValue>(paraDto.HorzAlign, out var horzAlign))
                                para.HorzAlign.Value = horzAlign;
                            else
                                para.HorzAlign.Value = HorzAlignValue.LeftAlign;
                        }

                        // Indentation and spacing values (in inches)
                        if (paraDto.IndLeft.HasValue) para.IndLeft.Value = paraDto.IndLeft.Value;
                        if (paraDto.IndRight.HasValue) para.IndRight.Value = paraDto.IndRight.Value;
                        if (paraDto.IndFirst.HasValue) para.IndFirst.Value = paraDto.IndFirst.Value;
                        if (paraDto.SpBefore.HasValue) para.SpBefore.Value = paraDto.SpBefore.Value;
                        if (paraDto.SpAfter.HasValue) para.SpAfter.Value = paraDto.SpAfter.Value;
                        if (paraDto.SpLine.HasValue) para.SpLine.Value = paraDto.SpLine.Value;

                        // Bullet formatting
                        if (!string.IsNullOrEmpty(paraDto.Bullet))
                        {
                            if (Enum.TryParse<BulletValue>(paraDto.Bullet, out var bulletVal))
                                para.Bullet.Value = bulletVal;
                            else
                                para.Bullet.Value = BulletValue.Style1;
                        }

                        if (!string.IsNullOrEmpty(paraDto.BulletStr))
                            para.BulletStr.Value = paraDto.BulletStr;

                        // Add the configured paragraph to the shape
                        shape.Paras.Add(para);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}