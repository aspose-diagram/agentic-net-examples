using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeInheritanceExport
{
    // Simple DTO to hold inheritance flags for a shape
    public class ShapeInheritanceInfo
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public bool HasInheritChars { get; set; }
        public bool HasInheritFill { get; set; }
        public bool HasInheritGeoms { get; set; }
        public bool HasInheritLine { get; set; }
        public bool HasInheritParas { get; set; }
        public bool HasInheritProps { get; set; }
        public bool HasInheritTextBlock { get; set; }
        public bool HasInheritUsers { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                Diagram diagram = new Diagram("input.vsdx");

                var inheritanceList = new List<ShapeInheritanceInfo>();

                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        var info = new ShapeInheritanceInfo
                        {
                            ID = shape.ID,
                            Name = shape.Name,
                            // The presence of each inheritance collection indicates that the shape inherits that aspect
                            HasInheritChars = shape.InheritChars != null,
                            HasInheritFill = shape.InheritFill != null,
                            HasInheritGeoms = shape.InheritGeoms != null,
                            HasInheritLine = shape.InheritLine != null,
                            HasInheritParas = shape.InheritParas != null,
                            HasInheritProps = shape.InheritProps != null,
                            HasInheritTextBlock = shape.InheritTextBlock != null,
                            HasInheritUsers = shape.InheritUsers != null
                        };

                        inheritanceList.Add(info);
                    }
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(inheritanceList, jsonOptions);

                // Write JSON to a file (replace with desired output path)
                File.WriteAllText("shapeInheritance.json", json);

                Console.WriteLine("Shape inheritance settings have been exported to shapeInheritance.json");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}