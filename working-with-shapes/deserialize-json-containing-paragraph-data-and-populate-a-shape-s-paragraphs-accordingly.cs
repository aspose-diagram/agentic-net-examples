using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

public class ParagraphDto
{
    public string? HorzAlign { get; set; }
    public double? IndLeft { get; set; }
    public double? IndRight { get; set; }
    public double? IndFirst { get; set; }
    public double? SpBefore { get; set; }
    public double? SpAfter { get; set; }
    public double? SpLine { get; set; }
    public int? Bullet { get; set; }
    public string? BulletStr { get; set; }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load JSON containing paragraph definitions
            string jsonPath = "paragraphs.json";
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"JSON file not found: {jsonPath}");
            }

            string jsonContent = File.ReadAllText(jsonPath);
            List<ParagraphDto>? paragraphData = JsonSerializer.Deserialize<List<ParagraphDto>>(jsonContent);
            if (paragraphData == null)
            {
                throw new Exception("Failed to deserialize paragraph JSON.");
            }

            // Load an existing Visio diagram (replace with your actual file)
            string diagramPath = "input.vsdx";
            if (!File.Exists(diagramPath))
            {
                throw new FileNotFoundException($"Diagram file not found: {diagramPath}");
            }

            Diagram diagram = new Diagram(diagramPath);

            // Assume we work with the first page and the shape with ID 1
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                throw new Exception("Target shape not found (ID = 1).");
            }

            // Remove any existing paragraphs
            shape.Paras.Clear();

            // Populate paragraphs from the deserialized data
            foreach (ParagraphDto dto in paragraphData)
            {
                Para para = new Para();

                if (!string.IsNullOrWhiteSpace(dto.HorzAlign) &&
                    Enum.TryParse<HorzAlignValue>(dto.HorzAlign, out var horzAlignEnum))
                {
                    para.HorzAlign.Value = horzAlignEnum;
                }

                if (dto.IndLeft.HasValue)   para.IndLeft.Value   = dto.IndLeft.Value;
                if (dto.IndRight.HasValue)  para.IndRight.Value  = dto.IndRight.Value;
                if (dto.IndFirst.HasValue)  para.IndFirst.Value  = dto.IndFirst.Value;
                if (dto.SpBefore.HasValue)  para.SpBefore.Value  = dto.SpBefore.Value;
                if (dto.SpAfter.HasValue)   para.SpAfter.Value   = dto.SpAfter.Value;
                if (dto.SpLine.HasValue)    para.SpLine.Value    = dto.SpLine.Value;

                if (dto.Bullet.HasValue &&
                    Enum.IsDefined(typeof(BulletValue), dto.Bullet.Value))
                {
                    para.Bullet.Value = (BulletValue)dto.Bullet.Value;
                }

                if (!string.IsNullOrEmpty(dto.BulletStr))
                {
                    para.BulletStr.Value = dto.BulletStr;
                }

                shape.Paras.Add(para);
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}