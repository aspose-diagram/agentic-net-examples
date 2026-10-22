using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0).
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // 1. Add two rectangle shapes to the page.
                // -----------------------------------------------------------------
                // Add first rectangle.
                long rect1Id = page.AddShape(2.0, 5.0, "Rectangle", false);
                Shape rect1 = page.Shapes.GetShape(rect1Id);

                // Add second rectangle.
                long rect2Id = page.AddShape(6.0, 5.0, "Rectangle", false);
                Shape rect2 = page.Shapes.GetShape(rect2Id);

                // -----------------------------------------------------------------
                // 2. Insert a custom text field into the first rectangle.
                // -----------------------------------------------------------------
                Field field = new Field();
                // Set the displayed value of the field.
                field.Value.Val = "Sample Field";
                // Initialise the formula (UFEV) – empty formula with undefined unit.
                field.Value.Ufev.Unit = MeasureConst.Undefined;
                field.Value.Ufev.F = "";
                // Add the field to the shape's field collection.
                rect1.Fields.Add(field);

                // -----------------------------------------------------------------
                // 3. Add a dynamic connector shape.
                // -----------------------------------------------------------------
                long connectorId = page.AddShape(4.0, 5.0, "Dynamic connector", false);
                Shape connector = page.Shapes.GetShape(connectorId);

                // -----------------------------------------------------------------
                // 4. Connect the two rectangles via the dynamic connector.
                // -----------------------------------------------------------------
                page.ConnectShapesViaConnector(
                    rect1.ID,
                    ConnectionPointPlace.Right,
                    rect2.ID,
                    ConnectionPointPlace.Left,
                    connectorId);

                // -----------------------------------------------------------------
                // 5. Save the modified diagram.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}