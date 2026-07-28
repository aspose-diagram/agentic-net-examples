using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string diagramPath = "input.vsdx";
            string jsonPath = "paragraphs.json";
            string outputPath = "output.vsdx";

            // Load JSON containing paragraph definitions
            string jsonContent = File.ReadAllText(jsonPath);
            List<ParagraphDto> paragraphData = JsonSerializer.Deserialize<List<ParagraphDto>>(jsonContent);

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (or adjust to target a specific page)
            Page page = diagram.Pages[0];

            // Retrieve a shape to modify – here we take the first shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                targetShape = shp;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found on the page.");
                return;
            }

            // Clear existing paragraphs
            targetShape.Paras.Clear();

            // Populate paragraphs from JSON data
            foreach (ParagraphDto dto in paragraphData)
            {
                Para para = new Para();

                // HorzAlign
                if (!string.IsNullOrWhiteSpace(dto.HorzAlign) &&
                    Enum.TryParse<HorzAlignValue>(dto.HorzAlign, out var horzAlignEnum))
                {
                    para.HorzAlign.Value = horzAlignEnum;
                }

                // Indentation and spacing values (in inches)
                if (dto.IndLeft.HasValue)   para.IndLeft.Value   = dto.IndLeft.Value;
                if (dto.IndRight.HasValue)  para.IndRight.Value  = dto.IndRight.Value;
                if (dto.IndFirst.HasValue)  para.IndFirst.Value  = dto.IndFirst.Value;
                if (dto.SpBefore.HasValue)  para.SpBefore.Value  = dto.SpBefore.Value;
                if (dto.SpAfter.HasValue)   para.SpAfter.Value   = dto.SpAfter.Value;
                if (dto.SpLine.HasValue)    para.SpLine.Value    = dto.SpLine.Value;

                // Bullet style
                if (!string.IsNullOrWhiteSpace(dto.Bullet) &&
                    Enum.TryParse<BulletValue>(dto.Bullet, out var bulletEnum))
                {
                    para.Bullet.Value = bulletEnum;
                }

                // Bullet string (custom bullet character)
                if (!string.IsNullOrWhiteSpace(dto.BulletStr))
                {
                    para.BulletStr.Value = dto.BulletStr;
                }

                // Add the configured paragraph to the shape
                targetShape.Paras.Add(para);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}