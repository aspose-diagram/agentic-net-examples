using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Model representing a paragraph's formatting and text as defined in the input JSON.
    private class ParagraphData
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
        public string Text { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, JSON file path, shape ID (numeric).
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <jsonPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string jsonPath = args[1];
        string shapeIdArg = args[2];

        // Guard against missing files.
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

        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Load and deserialize the JSON containing paragraph definitions.
        ParagraphData[] paragraphs;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            paragraphs = JsonSerializer.Deserialize<ParagraphData[]>(jsonContent);
            if (paragraphs == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON or JSON is empty.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading or parsing JSON: {ex.Message}");
            return;
        }

        // Load the Visio diagram and apply paragraph data to the specified shape.
        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the first page (adjust if needed).
                Page page = diagram.Pages[0];

                // Retrieve the target shape by its ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Clear existing paragraphs.
                shape.Paras.Clear();

                // Build combined text for the shape (each paragraph separated by a line break).
                string combinedText = string.Empty;

                for (int i = 0; i < paragraphs.Length; i++)
                {
                    ParagraphData pd = paragraphs[i];

                    // Create a new paragraph object.
                    Para para = new Para();

                    // Horizontal alignment.
                    if (!string.IsNullOrEmpty(pd.HorzAlign) &&
                        Enum.TryParse(pd.HorzAlign, out HorzAlignValue horzAlignVal))
                    {
                        para.HorzAlign.Value = horzAlignVal;
                    }

                    // Indentation and spacing.
                    para.IndLeft.Value = pd.IndLeft;
                    para.IndRight.Value = pd.IndRight;
                    para.IndFirst.Value = pd.IndFirst;
                    para.SpBefore.Value = pd.SpBefore;
                    para.SpAfter.Value = pd.SpAfter;
                    para.SpLine.Value = pd.SpLine;

                    // Bullet style.
                    if (!string.IsNullOrEmpty(pd.Bullet) &&
                        Enum.TryParse(pd.Bullet, out BulletValue bulletVal))
                    {
                        para.Bullet.Value = bulletVal;
                    }

                    // Custom bullet string.
                    para.BulletStr.Value = pd.BulletStr ?? string.Empty;

                    // Add the configured paragraph to the shape.
                    shape.Paras.Add(para);

                    // Append paragraph text.
                    combinedText += pd.Text ?? string.Empty;
                    if (i < paragraphs.Length - 1)
                    {
                        combinedText += "\r\n";
                    }
                }

                // Replace existing text with the combined paragraph text.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(combinedText));

                // Refresh shape data to ensure changes are applied.
                shape.RefreshData();

                // Save the modified diagram to a new file.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? "",
                    Path.GetFileNameWithoutExtension(diagramPath) + "_updated.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}