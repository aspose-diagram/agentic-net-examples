using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (isCalculate = false)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape instance using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a custom property named "Category"
            Prop categoryProp = new Prop();
            categoryProp.Name = "Category";                     // Property name
            categoryProp.Label.Value = "Category";              // Display label (optional)
            categoryProp.Value.Val = "Finance";                 // Property value
            // Set the property type using the correct TypePropValue enum
            categoryProp.Type.Value = TypePropValue.String;    // Property type

            // Add the custom property to the shape
            shape.Props.Add(categoryProp);

            // -----------------------------------------------------------------
            // Query all shapes that have a custom property named "Category"
            // -----------------------------------------------------------------
            Console.WriteLine("Shapes with the custom property \"Category\":");
            foreach (Page pg in diagram.Pages)
            {
                foreach (Shape shp in pg.Shapes)
                {
                    foreach (Prop p in shp.Props)
                    {
                        if (p.Name == "Category")
                        {
                            Console.WriteLine($"Shape ID {shp.ID} - Category: {p.Value.Val}");
                        }
                    }
                }
            }

            // Save the diagram to a VSDX file
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            diagram.Save("CustomPropertyDemo.vsdx", saveOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}