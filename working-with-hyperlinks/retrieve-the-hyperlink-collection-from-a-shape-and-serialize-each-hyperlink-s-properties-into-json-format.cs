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

            // Example: get the first shape on the first page
            // Adjust the indices as needed to target a specific shape
            Shape shape = diagram.Pages[0].Shapes[0];

            // Prepare a list to hold hyperlink data
            var hyperlinkData = new List<object>();

            // Iterate through each hyperlink in the shape's collection
            foreach (Hyperlink hl in shape.Hyperlinks)
            {
                var item = new
                {
                    Address = hl.Address,
                    Description = hl.Description,
                    SubAddress = hl.SubAddress,
                    NewWindow = hl.NewWindow,
                    Invisible = hl.Invisible,
                    Default = hl.Default,
                    Frame = hl.Frame,
                    SortKey = hl.SortKey?.Value, // Str2Value may be null
                    Name = hl.Name,
                    NameU = hl.NameU,
                    ID = hl.ID,
                    Del = hl.Del
                };
                hyperlinkData.Add(item);
            }

            // Serialize the list to formatted JSON
            string json = JsonSerializer.Serialize(hyperlinkData, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to a file
            File.WriteAllText("hyperlinks.json", json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
