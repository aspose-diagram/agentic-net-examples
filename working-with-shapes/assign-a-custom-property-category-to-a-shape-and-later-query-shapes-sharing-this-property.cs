using System;
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

                // Add a new page to the diagram
                diagram.Pages.Add(new Page());
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, master name ("Rectangle")
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape has a Props collection (it always does)
                // Create a custom property named "Category"
                Prop categoryProp = new Prop();
                categoryProp.Name = "Category";                 // Property name
                categoryProp.Label.Value = "Category";          // Optional label
                categoryProp.Type.Value = TypePropValue.String; // Data type
                categoryProp.Value.Val = "Finance";             // Property value

                // Add the custom property to the shape
                shape.Props.Add(categoryProp);

                // Save the diagram to a file
                diagram.Save("CustomPropertyDiagram.vsdx", SaveFileFormat.Vsdx);

                // -----------------------------------------------------------------
                // Query shapes that have the custom property "Category"
                // -----------------------------------------------------------------
                Console.WriteLine("Shapes with custom property \"Category\":");
                foreach (Page pg in diagram.Pages)
                {
                    foreach (Shape shp in pg.Shapes)
                    {
                        // Look for a property named "Category"
                        foreach (Prop p in shp.Props)
                        {
                            if (p.Name == "Category")
                            {
                                Console.WriteLine($"Shape ID: {shp.ID}, Category Value: {p.Value.Val}");
                                // Break after finding the property for this shape
                                break;
                            }
                        }
                    }
                }

                // Keep console window open when run outside of IDE
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }