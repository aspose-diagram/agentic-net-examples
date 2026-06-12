using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramParagraphUpdater
{
    // DTO representing a paragraph's formatting data in the JSON file
    public class ParagraphData
    {
        public string HorzAlign { get; set; }          // e.g., "LeftAlign", "Center", "RightAlign"
        public double IndLeft { get; set; }            // left indent (in inches)
        public double IndRight { get; set; }           // right indent (in inches)
        public double IndFirst { get; set; }           // first line indent (in inches)
        public double SpBefore { get; set; }           // space before (in inches)
        public double SpAfter { get; set; }            // space after (in inches)
        public double SpLine { get; set; }             // line spacing (in inches)
        public string Bullet { get; set; }             // e.g., "None", "Style1", "Style2"
        public string BulletStr { get; set; }          // custom bullet string (optional)
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // 0 - path to the source Visio file (e.g., "input.vsdx")
            // 1 - shape ID to modify (numeric)
            // 2 - path to the JSON file containing paragraph data
            // 3 - path for the output Visio file (e.g., "output.vsdx")
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: DiagramParagraphUpdater <inputVisio> <shapeId> <jsonFile> <outputVisio>");
                return;
            }

            string inputVisioPath = args[0];
            long shapeId;
            if (!long.TryParse(args[1], out shapeId))
            {
                Console.WriteLine("Invalid shape ID.");
                return;
            }
            string jsonFilePath = args[2];
            string outputVisioPath = args[3];

            // Load the JSON file
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"JSON file not found: {jsonFilePath}");
                return;
            }

            string jsonContent = File.ReadAllText(jsonFilePath);
            List<ParagraphData> paragraphs;
            try
            {
                paragraphs = JsonSerializer.Deserialize<List<ParagraphData>>(jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to deserialize JSON: {ex.Message}");
                return;
            }

            if (paragraphs == null || paragraphs.Count == 0)
            {
                Console.WriteLine("No paragraph data found in JSON.");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputVisioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Locate the shape by ID (search across all pages)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    targetShape = page.Shapes.GetShape(shapeId);
                    if (targetShape != null)
                        break;
                }
                catch
                {
                    // GetShape throws if ID not found on this page; continue searching
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Clear existing paragraphs
            targetShape.Paras.Clear();

            // Populate paragraphs from JSON
            foreach (ParagraphData pd in paragraphs)
            {
                Para para = new Para();

                // HorzAlign (enum HorzAlignValue)
                if (!string.IsNullOrEmpty(pd.HorzAlign))
                {
                    if (Enum.TryParse(typeof(HorzAlignValue), pd.HorzAlign, out object horzAlignEnum))
                        para.HorzAlign.Value = (HorzAlignValue)horzAlignEnum;
                }

                // Indents and spacing (double values)
                para.IndLeft.Value = pd.IndLeft;
                para.IndRight.Value = pd.IndRight;
                para.IndFirst.Value = pd.IndFirst;
                para.SpBefore.Value = pd.SpBefore;
                para.SpAfter.Value = pd.SpAfter;
                para.SpLine.Value = pd.SpLine;

                // Bullet style (enum BulletValue)
                if (!string.IsNullOrEmpty(pd.Bullet))
                {
                    if (Enum.TryParse(typeof(BulletValue), pd.Bullet, out object bulletEnum))
                        para.Bullet.Value = (BulletValue)bulletEnum;
                }

                // Custom bullet string
                para.BulletStr.Value = pd.BulletStr ?? string.Empty;

                // Add the configured paragraph to the shape
                targetShape.Paras.Add(para);
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputVisioPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }
}