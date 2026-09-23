using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string diagramPath = "sample.vsdx"; // replace with your file path
            Diagram diagram = new Diagram(diagramPath);

            // Identify the page and shape you want to inspect
            int pageIndex = 0;          // first page (zero‑based)
            long shapeId = 1;           // replace with the actual shape ID

            // Retrieve the page and shape objects
            Page page = diagram.Pages[pageIndex];
            Shape shape = page.Shapes.GetShape(shapeId);

            // -----------------------------------------------------------------
            // 1. List custom shape properties (Props) – name and data type
            // -----------------------------------------------------------------
            Console.WriteLine("Custom Properties (Props):");
            foreach (Prop prop in shape.Props)
            {
                // Property name
                string propName = prop.Name;

                // Data type stored in the Type cell (enum TypePropValue)
                string dataType = prop.Type.Value.ToString();

                Console.WriteLine($"Name: {propName}, Type: {dataType}");
            }

            // -----------------------------------------------------------------
            // 2. List text insertion fields (Fields) – index and data type
            // -----------------------------------------------------------------
            Console.WriteLine("\nText Fields (Fields):");
            int fieldIndex = 0;
            foreach (Field field in shape.Fields)
            {
                // Fields have no explicit name; use the index as identifier
                string fieldType = field.Type.Value.ToString();

                Console.WriteLine($"Index: {fieldIndex}, Type: {fieldType}");
                fieldIndex++;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
