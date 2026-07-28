using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape (example: first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Access the shape's TextBlock
            TextBlock textBlock = shape.TextBlock;

            // Collect TextBlock properties into a dictionary for serialization
            var textBlockData = new Dictionary<string, object>
            {
                { "BottomMargin", textBlock.BottomMargin },
                { "DefaultTabStop", textBlock.DefaultTabStop },
                { "Del", textBlock.Del },
                { "LeftMargin", textBlock.LeftMargin },
                { "RightMargin", textBlock.RightMargin },
                { "TextBkgnd", textBlock.TextBkgnd?.ToString() },
                { "TextBkgndTrans", textBlock.TextBkgndTrans },
                { "TextDirection", textBlock.TextDirection?.ToString() },
                { "TopMargin", textBlock.TopMargin },
                { "VerticalAlign", textBlock.VerticalAlign?.ToString() }
            };

            // Serialize the dictionary to formatted JSON
            string json = JsonSerializer.Serialize(textBlockData, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON to a file for backup/reuse
            File.WriteAllText("shape_textblock.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
