using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing the JSON structure
    private class JsonModel
    {
        public long ShapeId { get; set; }
        public List<ParagraphData> Paragraphs { get; set; } = new();
    }

    private class ParagraphData
    {
        public int HorzAlign { get; set; }          // Maps to HorzAlignValue enum
        public double IndLeft { get; set; }        // Inches
        public double IndRight { get; set; }       // Inches
        public double IndFirst { get; set; }       // Inches
        public double SpBefore { get; set; }       // Inches
        public double SpAfter { get; set; }        // Inches
        public double SpLine { get; set; }         // Lines
        public int Bullet { get; set; }            // Maps to BulletValue enum
        public string BulletStr { get; set; }      // Optional bullet string
    }

    static void Main(string[] args)
    {
        // Expect three arguments: diagram path, JSON path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <jsonPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Load and deserialize JSON
        JsonModel model;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            model = JsonSerializer.Deserialize<JsonModel>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (model == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading or parsing JSON: {ex.Message}");
            return;
        }

        // Load the diagram and apply paragraph data
        try
        {
            // Use a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Retrieve the target shape by ID
                Shape shape = page.Shapes.GetShape((int)model.ShapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {model.ShapeId} not found on the first page.");
                    return;
                }

                // Clear existing paragraphs
                shape.Paras.Clear();

                // Populate paragraphs from JSON data
                foreach (ParagraphData paraData in model.Paragraphs)
                {
                    // Create a new paragraph instance
                    Para para = new Para();

                    // Horizontal alignment
                    para.HorzAlign.Value = (HorzAlignValue)paraData.HorzAlign;

                    // Indentation values (in inches)
                    para.IndLeft.Value = paraData.IndLeft;
                    para.IndRight.Value = paraData.IndRight;
                    para.IndFirst.Value = paraData.IndFirst;

                    // Spacing values
                    para.SpBefore.Value = paraData.SpBefore;
                    para.SpAfter.Value = paraData.SpAfter;
                    para.SpLine.Value = paraData.SpLine; // line spacing in lines

                    // Bullet settings
                    para.Bullet.Value = (BulletValue)paraData.Bullet;
                    if (!string.IsNullOrEmpty(paraData.BulletStr))
                    {
                        para.BulletStr.Value = paraData.BulletStr;
                    }

                    // Add the configured paragraph to the shape
                    shape.Paras.Add(para);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }
}