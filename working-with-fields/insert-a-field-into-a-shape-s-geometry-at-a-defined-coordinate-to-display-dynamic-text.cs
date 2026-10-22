using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: inputPath, outputPath, coordX, coordY
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <coordX> <coordY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!double.TryParse(args[2], out double coordX) || !double.TryParse(args[3], out double coordY))
        {
            Console.Error.WriteLine("Invalid coordinate values. Please provide numeric X and Y.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the first shape that has a non‑empty text (as an example target)
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains any text
                        if (!string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No suitable shape with text was found in the diagram.");
                    return;
                }

                // ------------------------------------------------------------
                // 1. Insert a dynamic text field into the shape's Fields collection
                // ------------------------------------------------------------
                Field dynamicField = new Field();
                // The field will display the value of a custom property named "MyProp"
                dynamicField.Value.Val = "Prop.MyProp";
                // Ensure the unit is undefined (standard for text fields)
                dynamicField.Value.Ufev.Unit = MeasureConst.Undefined;
                dynamicField.Value.Ufev.F = "";
                // Add the field to the shape
                targetShape.Fields.Add(dynamicField);

                // ------------------------------------------------------------
                // 2. Append a new geometry segment at the specified coordinate
                // ------------------------------------------------------------
                // Ensure the shape has at least one Geom; if not, create one
                Geom geom;
                if (targetShape.Geoms.Count > 0)
                {
                    geom = (Geom)targetShape.Geoms[0];
                }
                else
                {
                    geom = new Geom();
                    targetShape.Geoms.Add(geom);
                }

                // Start a new path with a MoveTo at (0,0) if the geometry is empty
                if (geom.CoordinateCol.Count == 0)
                {
                    MoveTo move = new MoveTo();
                    move.X.Value = 0.0;
                    move.Y.Value = 0.0;
                    geom.CoordinateCol.Add(move);
                }

                // Add a LineTo segment that ends at the user‑defined coordinate
                LineTo line = new LineTo();
                line.X.Value = coordX;
                line.Y.Value = coordY;
                geom.CoordinateCol.Add(line);

                // Refresh shape data so that Visio recalculates geometry and fields
                targetShape.RefreshData();

                // ------------------------------------------------------------
                // 3. Save the modified diagram
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}