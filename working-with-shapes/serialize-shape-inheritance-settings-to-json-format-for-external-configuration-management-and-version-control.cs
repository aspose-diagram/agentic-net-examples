using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

public class ShapeInheritanceInfo
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string InheritChars { get; set; }
    public string InheritFill { get; set; }
    public string InheritGeoms { get; set; }
    public string InheritLine { get; set; }
    public string InheritParas { get; set; }
    public string InheritProps { get; set; }
    public string InheritTextBlock { get; set; }
    public string InheritUsers { get; set; }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            var inheritanceData = new List<ShapeInheritanceInfo>();

            // Iterate through all shapes on the first page (adjust as needed)
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                var info = new ShapeInheritanceInfo
                {
                    ID = shape.ID,
                    Name = shape.Name,
                    InheritChars = shape.InheritChars?.ToString(),
                    InheritFill = shape.InheritFill?.ToString(),
                    InheritGeoms = shape.InheritGeoms?.ToString(),
                    InheritLine = shape.InheritLine?.ToString(),
                    InheritParas = shape.InheritParas?.ToString(),
                    InheritProps = shape.InheritProps?.ToString(),
                    InheritTextBlock = shape.InheritTextBlock?.ToString(),
                    InheritUsers = shape.InheritUsers?.ToString()
                };
                inheritanceData.Add(info);
            }

            // Serialize the collected inheritance settings to JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(inheritanceData, jsonOptions);

            // Save JSON to a file for external configuration management
            File.WriteAllText("shapeInheritance.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}