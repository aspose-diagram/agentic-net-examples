using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Unique identifier of the shape to locate
                long shapeId = 12345; // replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the shape by its unique ID from the first page
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Access the inherited fill information
                // InheritFill provides the fill values inherited from the parent style and master
                string inheritedForeColor = shape.InheritFill.FillForegnd.Value;
                string inheritedBackColor = shape.InheritFill.FillBkgnd.Value;
                int inheritedPattern = shape.InheritFill.FillPattern.Value;

                // Output the inherited fill properties
                Console.WriteLine($"Shape ID: {shapeId}");
                Console.WriteLine($"Inherited Fill Foreground Color: {inheritedForeColor}");
                Console.WriteLine($"Inherited Fill Background Color: {inheritedBackColor}");
                Console.WriteLine($"Inherited Fill Pattern: {inheritedPattern}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }