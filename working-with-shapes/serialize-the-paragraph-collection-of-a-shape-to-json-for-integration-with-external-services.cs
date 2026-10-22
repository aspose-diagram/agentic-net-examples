using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape NameU, output JSON path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputDiagramPath> <shapeNameU> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string shapeNameU = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Use the first page for shape lookup
                Page page = diagram.Pages[0];

                // Locate the target shape by its NameU, ignoring deleted shapes
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False && shape.NameU == shapeNameU)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU '{shapeNameU}' not found.");
                    return;
                }

                // Collect paragraph information into DTO objects
                List<ParagraphDto> paragraphs = new List<ParagraphDto>();
                for (int i = 0; i < targetShape.Paras.Count; i++)
                {
                    // The paragraph type is Para (not Paragraph)
                    Para para = targetShape.Paras[i];

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

                // Serialize the DTO list to formatted JSON
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(paragraphs, jsonOptions);

                // Write the JSON string to the specified output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Paragraph collection serialized to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

// DTO class representing a paragraph's serializable properties
class ParagraphDto
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