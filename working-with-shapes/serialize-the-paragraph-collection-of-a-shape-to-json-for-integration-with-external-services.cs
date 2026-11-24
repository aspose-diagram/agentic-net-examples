using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, target shape NameU, output JSON path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeNameU> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string shapeNameU = args[1];
        string outputPath = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape targetShape = null;

                // Iterate through all pages and shapes to find the shape with the specified NameU
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        if (shape.NameU == shapeNameU)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU '{shapeNameU}' not found.");
                    return;
                }

                // Prepare a list to hold paragraph DTOs
                List<ParagraphDto> paragraphs = new List<ParagraphDto>();

                // Iterate over the paragraph collection using indexer as required
                for (int i = 0; i < targetShape.Paras.Count; i++)
                {
                    // The paragraph type is 'Para' in Aspose.Diagram
                    Para para = targetShape.Paras[i];

                    // Build DTO with selected properties
                    ParagraphDto dto = new ParagraphDto
                    {
                        Index = para.IX,
                        HorizontalAlignment = para.HorzAlign.ToString(),
                        IndentLeft = para.IndLeft?.Value,
                        IndentRight = para.IndRight?.Value,
                        IndentFirst = para.IndFirst?.Value,
                        SpaceBefore = para.SpBefore?.Value,
                        SpaceAfter = para.SpAfter?.Value,
                        SpaceLine = para.SpLine?.Value,
                        Bullet = para.Bullet.ToString(),
                        BulletString = para.BulletStr?.Value
                    };

                    paragraphs.Add(dto);
                }

                // Serialize the paragraph list to JSON with indentation
                string json = JsonSerializer.Serialize(paragraphs, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Paragraph collection serialized to JSON successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // DTO class representing a paragraph's relevant properties for JSON serialization
    private class ParagraphDto
    {
        public int Index { get; set; }
        public string? HorizontalAlignment { get; set; }
        public double? IndentLeft { get; set; }
        public double? IndentRight { get; set; }
        public double? IndentFirst { get; set; }
        public double? SpaceBefore { get; set; }
        public double? SpaceAfter { get; set; }
        public double? SpaceLine { get; set; }
        public string? Bullet { get; set; }
        public string? BulletString { get; set; }
    }
}
