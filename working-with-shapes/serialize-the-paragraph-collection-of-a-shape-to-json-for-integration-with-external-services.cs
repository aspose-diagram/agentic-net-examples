using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class SerializeShapeParas
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            var diagram = new Diagram("input.vsdx");

            // Retrieve a shape – here we use the first shape on the first page as an example
            // Adjust the index or use GetShape(id) / GetShape(name) as needed
            Shape shape = diagram.Pages[0].Shapes[0];

            // Access the paragraph collection of the shape
            ParaCollection paras = shape.Paras;

            // Prepare a list to hold serializable representations of each paragraph
            var paraDtoList = new List<object>();

            foreach (Para para in paras)
            {
                // Create an anonymous object with the properties you want to expose
                var paraDto = new
                {
                    IX = para.IX,
                    Bullet = para.Bullet,
                    BulletStr = para.BulletStr,
                    BulletFont = para.BulletFont,
                    BulletFontSize = para.BulletFontSize,
                    LocalizeBulletFont = para.LocalizeBulletFont,
                    HorzAlign = para.HorzAlign,
                    IndFirst = para.IndFirst,
                    IndLeft = para.IndLeft,
                    IndRight = para.IndRight,
                    SpAfter = para.SpAfter,
                    SpBefore = para.SpBefore,
                    SpLine = para.SpLine,
                    TextPosAfterBullet = para.TextPosAfterBullet,
                    Flags = para.Flags,
                    Del = para.Del
                };

                paraDtoList.Add(paraDto);
            }

            // Serialize the list to JSON with indentation for readability
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(paraDtoList, jsonOptions);

            // Output JSON to a file (replace with your desired output path)
            File.WriteAllText("shape_paras.json", json);

            // Optionally, write to console for quick verification
            Console.WriteLine(json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
