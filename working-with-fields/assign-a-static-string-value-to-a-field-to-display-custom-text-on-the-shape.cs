using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page (position at (2,2) inches)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new field and assign a static string value
                Field field = new Field();
                field.Value.Val = "Custom Text";

                // Add the field to the shape's Fields collection
                shape.Fields.Add(field);

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Field added and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }