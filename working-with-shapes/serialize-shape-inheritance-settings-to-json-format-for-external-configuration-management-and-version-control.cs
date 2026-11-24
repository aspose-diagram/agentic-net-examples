using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output JSON file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var inheritanceList = new List<ShapeInheritanceInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Prepare inheritance flags
                        bool fillInherited = false;
                        bool lineInherited = false;

                        // Check fill inheritance if InheritFill is available
                        if (shape.InheritFill != null && shape.Fill != null &&
                            shape.InheritFill.FillForegnd != null && shape.Fill.FillForegnd != null)
                        {
                            // Compare the FillForegnd values (case‑insensitive)
                            fillInherited = string.Equals(
                                shape.Fill.FillForegnd.Value,
                                shape.InheritFill.FillForegnd.Value,
                                StringComparison.OrdinalIgnoreCase);
                        }

                        // Check line inheritance if InheritLine is available
                        if (shape.InheritLine != null && shape.Line != null &&
                            shape.InheritLine.LineColor != null && shape.Line.LineColor != null)
                        {
                            // Compare the LineColor values (case‑insensitive)
                            lineInherited = string.Equals(
                                shape.Line.LineColor.Value,
                                shape.InheritLine.LineColor.Value,
                                StringComparison.OrdinalIgnoreCase);
                        }

                        // Collect information for this shape
                        inheritanceList.Add(new ShapeInheritanceInfo
                        {
                            ShapeId = shape.ID,          // Shape.ID is a long
                            NameU = shape.NameU,
                            FillInherited = fillInherited,
                            LineInherited = lineInherited
                        });
                    }
                }

                // Serialize the list to JSON with indentation
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(inheritanceList, jsonOptions);

                // Write JSON to the output file
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"Shape inheritance settings have been serialized to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper class to hold shape inheritance information for JSON serialization
    private class ShapeInheritanceInfo
    {
        public long ShapeId { get; set; }
        public string NameU { get; set; }
        public bool FillInherited { get; set; }
        public bool LineInherited { get; set; }
    }
}