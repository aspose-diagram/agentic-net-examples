using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        diagram.Pages.Add(new Page());
        Page page = diagram.Pages[0];

        // Add first rectangle shape using DrawRectangle (returns a shape ID)
        long shapeId1 = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);
        Shape shape1 = page.Shapes.GetShape(shapeId1);

        // Add custom property "Category" to the first shape
        Prop categoryProp1 = new Prop();
        categoryProp1.Name = "Category";
        categoryProp1.Label.Value = "Category";
        categoryProp1.Value.Val = "Finance";
        categoryProp1.Type.Value = TypePropValue.String;
        shape1.Props.Add(categoryProp1);

        // Add second rectangle shape
        long shapeId2 = page.DrawRectangle(5.0, 2.0, 2.0, 1.0);
        Shape shape2 = page.Shapes.GetShape(shapeId2);

        // Add the same custom property to the second shape
        Prop categoryProp2 = new Prop();
        categoryProp2.Name = "Category";
        categoryProp2.Label.Value = "Category";
        categoryProp2.Value.Val = "Finance";
        categoryProp2.Type.Value = TypePropValue.String;
        shape2.Props.Add(categoryProp2);

        // Query and list all shapes that have the "Category" custom property
        Console.WriteLine("Shapes with custom property \"Category\":");
        foreach (Page pg in diagram.Pages)
        {
            foreach (Shape shp in pg.Shapes)
            {
                foreach (Prop prop in shp.Props)
                {
                    if (prop.Name == "Category")
                    {
                        Console.WriteLine($"Shape ID: {shp.ID}, Category Value: {prop.Value.Val}");
                        break; // property found, move to next shape
                    }
                }
            }
        }

        // Save the diagram to a VSDX file
        diagram.Save("CustomPropertyDemo.vsdx", SaveFileFormat.Vsdx);
    }
}
