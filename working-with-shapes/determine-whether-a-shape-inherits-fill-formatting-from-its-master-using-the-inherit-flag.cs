using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (replace with actual shape ID)
                // Here we simply take the first shape in the collection for demonstration
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Compare fill properties with inherited fill values
                bool foregndMatches = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                bool patternMatches = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                // Determine if the shape is inheriting fill formatting from its master
                bool isInheritingFill = foregndMatches && patternMatches;

                Console.WriteLine($"Shape ID {shape.ID} inherits fill from master: {isInheritingFill}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }