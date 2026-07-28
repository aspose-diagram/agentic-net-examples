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
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a specific shape by its ID (replace 1 with the target shape ID)
                Shape shape = page.Shapes.GetShape(1);

                // Disable line inheritance by explicitly setting a line color.
                // This overrides any inherited line color from the master or style.
                shape.Line.LineColor.Value = "#FF0000";

                // Assign a new line weight (thickness) in inches.
                shape.Line.LineWeight.Value = 0.05; // 0.05 inches

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }