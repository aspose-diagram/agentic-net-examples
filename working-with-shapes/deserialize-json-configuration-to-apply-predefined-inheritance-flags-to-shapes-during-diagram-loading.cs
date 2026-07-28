using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramInheritanceDemo
{
    // DTO for JSON configuration
    public class DiagramConfig
    {
        public List<ShapeInheritance> Shapes { get; set; } = new();
    }

    public class ShapeInheritance
    {
        public long ShapeId { get; set; }
        public bool InheritFill { get; set; }
        public bool InheritLine { get; set; }
        public bool InheritChars { get; set; }
        public bool InheritProps { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: 1) path to the Visio file, 2) path to the JSON config file
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramInheritanceDemo <VisioFilePath> <ConfigJsonPath>");
                return;
            }

            string visioPath = args[0];
            string jsonPath = args[1];

            if (!File.Exists(visioPath))
            {
                Console.WriteLine($"Visio file not found: {visioPath}");
                return;
            }

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"Config JSON file not found: {jsonPath}");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Read and deserialize JSON configuration
            string jsonContent = File.ReadAllText(jsonPath);
            DiagramConfig config = JsonSerializer.Deserialize<DiagramConfig>(jsonContent);
            if (config == null || config.Shapes == null)
            {
                Console.WriteLine("Invalid configuration file.");
                return;
            }

            // Apply inheritance settings to each specified shape
            foreach (var shapeConfig in config.Shapes)
            {
                // Search for the shape across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        targetShape = page.Shapes.GetShape(shapeConfig.ShapeId);
                        if (targetShape != null)
                            break;
                    }
                    catch
                    {
                        // Shape not on this page; continue searching
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeConfig.ShapeId} not found.");
                    continue;
                }

                // Inherit Fill values
                if (shapeConfig.InheritFill)
                {
                    // Copy foreground, background and pattern from inherited fill
                    targetShape.Fill.FillForegnd.Value = targetShape.InheritFill.FillForegnd.Value;
                    targetShape.Fill.FillBkgnd.Value = targetShape.InheritFill.FillBkgnd.Value;
                    targetShape.Fill.FillPattern.Value = targetShape.InheritFill.FillPattern.Value;
                }

                // Inherit Line values
                if (shapeConfig.InheritLine)
                {
                    targetShape.Line.LineColor.Value = targetShape.InheritLine.LineColor.Value;
                    targetShape.Line.LineWeight.Value = targetShape.InheritLine.LineWeight.Value;
                    targetShape.Line.LinePattern.Value = targetShape.InheritLine.LinePattern.Value;
                }

                // Inherit character formatting (first character as example)
                if (shapeConfig.InheritChars && targetShape.InheritChars.Count > 0)
                {
                    // Ensure the shape has a Char collection entry
                    if (targetShape.Chars.Count == 0)
                        targetShape.Chars.Add(new Aspose.Diagram.Char());

                    // Copy properties from the first inherited Char
                    var inheritChar = targetShape.InheritChars[0];
                    var targetChar = targetShape.Chars[0];

                    targetChar.Color.Value = inheritChar.Color.Value;
                    targetChar.Font.Value = inheritChar.Font.Value;
                    targetChar.Size.Value = inheritChar.Size.Value;
                    targetChar.Style.Value = inheritChar.Style.Value;
                }

                // Inherit custom properties (Props)
                if (shapeConfig.InheritProps && targetShape.InheritProps.Count > 0)
                {
                    // Clear existing Props and copy inherited ones
                    targetShape.Props.Clear();
                    foreach (var inheritProp in targetShape.InheritProps)
                    {
                        var newProp = new Aspose.Diagram.Prop
                        {
                            Name = inheritProp.Name,
                            Label = { Value = inheritProp.Label.Value },
                            Prompt = { Value = inheritProp.Prompt.Value },
                            Type = { Value = inheritProp.Type.Value },
                            Format = { Value = inheritProp.Format.Value },
                            Value = { Val = inheritProp.Value.Val }
                        };
                        targetShape.Props.Add(newProp);
                    }
                }

                Console.WriteLine($"Processed shape ID {shapeConfig.ShapeId}");
            }

            // Save the modified diagram
            string outputPath = Path.Combine(
                Path.GetDirectoryName(visioPath) ?? "",
                Path.GetFileNameWithoutExtension(visioPath) + "_Modified.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
    }
}