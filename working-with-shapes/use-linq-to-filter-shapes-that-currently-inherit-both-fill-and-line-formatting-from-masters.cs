using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Query all shapes across all pages that inherit BOTH fill and line formatting from their master
                var inheritedShapes = diagram.Pages
                    .SelectMany(page => page.Shapes.Cast<Shape>()) // flatten shapes from all pages
                    .Where(shape =>
                        // Compare a representative fill property with its inherited counterpart
                        shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                        // Compare a representative line property with its inherited counterpart
                        shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value)
                    .ToList();

                // Output the results
                Console.WriteLine($"Found {inheritedShapes.Count} shape(s) inheriting both fill and line formatting from masters:");
                foreach (var shape in inheritedShapes)
                {
                    Console.WriteLine($"- Shape ID: {shape.ID}, NameU: {shape.NameU}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }