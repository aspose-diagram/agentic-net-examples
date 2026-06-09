using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we want to modify the first shape on the first page (ID = 1)
                // Retrieve the shape by its ID (shape IDs are of type long)
                long shapeId = 1;
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Create a new hyperlink
                Hyperlink link = new Hyperlink
                {
                    Name = "ExampleLink",                     // Optional internal name
                    Address = { Value = "https://example.com" }, // External URL
                    Description = { Value = "Visit Example.com" } // Tooltip text shown on hover
                };

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }