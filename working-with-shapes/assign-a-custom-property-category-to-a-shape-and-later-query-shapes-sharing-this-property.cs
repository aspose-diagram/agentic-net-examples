using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Get the first page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: PinX, PinY, Width, Height, MasterName
                long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a custom property (Prop) named "Category"
                Prop categoryProp = new Prop();
                categoryProp.Name = "Category";                     // Property name
                categoryProp.Label.Value = "Category";              // Visible label
                categoryProp.Type.Value = TypePropValue.String;    // Data type
                categoryProp.Value.Val = "Finance";                 // Property value

                // Add the custom property to the shape
                shape.Props.Add(categoryProp);

                // ---- Additional shapes with the same custom property for demonstration ----
                long shapeId2 = page.AddShape(8.0, 5.0, 2.0, 1.0, "Rectangle");
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                Prop catProp2 = new Prop();
                catProp2.Name = "Category";
                catProp2.Label.Value = "Category";
                catProp2.Type.Value = TypePropValue.String;
                catProp2.Value.Val = "Marketing";
                shape2.Props.Add(catProp2);

                long shapeId3 = page.AddShape(11.0, 5.0, 2.0, 1.0, "Rectangle");
                Shape shape3 = page.Shapes.GetShape(shapeId3);
                Prop catProp3 = new Prop();
                catProp3.Name = "Category";
                catProp3.Label.Value = "Category";
                catProp3.Type.Value = TypePropValue.String;
                catProp3.Value.Val = "Finance";
                shape3.Props.Add(catProp3);
                // -------------------------------------------------------------------------

                // Query all shapes that have a custom property named "Category"
                Console.WriteLine("Shapes with custom property \"Category\":");
                foreach (Page pg in diagram.Pages)
                {
                    foreach (Shape shp in pg.Shapes)
                    {
                        // Ensure the shape has a Props collection
                        if (shp.Props != null)
                        {
                            foreach (Prop prop in shp.Props)
                            {
                                if (prop.Name == "Category")
                                {
                                    Console.WriteLine($"Shape ID: {shp.ID}, Category: {prop.Value.Val}");
                                    // Break after finding the property to avoid duplicate prints for the same shape
                                    break;
                                }
                            }
                        }
                    }
                }

                // Save the diagram to a VSDX file
                diagram.Save("CustomPropertyDemo.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }