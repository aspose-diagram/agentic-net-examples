using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Load diagram, add a shape with a custom "Category" property, and save.
        // -----------------------------------------------------------------
        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Obtain the first page, or create a new one if the diagram is empty.
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    // Create a new blank page and add it to the diagram.
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Add a rectangle shape to the selected page.
                // The AddShape overload: AddShape(pinX, pinY, masterName, isCalculate)
                long shapeId = page.AddShape(1.0, 1.0, "Rectangle", false);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a custom property named "Category".
                Prop categoryProp = new Prop();
                categoryProp.Name = "Category";
                categoryProp.Label.Value = "Category";          // Display label.
                categoryProp.Value.Val = "Finance";            // Property value.
                categoryProp.Type.Value = TypePropValue.String; // Data type.

                // Add the custom property to the shape.
                shape.Props.Add(categoryProp);

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Reload the saved diagram and query shapes that have the "Category" property.
        // -----------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(outputPath))
            {
                Console.WriteLine("Shapes with custom property \"Category\":");
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Look for the "Category" custom property.
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Category")
                            {
                                Console.WriteLine($"Shape ID: {shape.ID}, Category: {prop.Value.Val}");
                                break; // Property found; move to next shape.
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error querying diagram: {ex.Message}");
        }
    }
}
