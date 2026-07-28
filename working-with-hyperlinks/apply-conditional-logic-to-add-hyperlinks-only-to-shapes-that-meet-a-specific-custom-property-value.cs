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

                // Add a new page (the diagram starts with one page by default)
                Page page = diagram.Pages[0];

                // Add three shapes using the built‑in "Rectangle" master
                long shapeId1 = page.AddShape(2.0, 2.0, "Rectangle");
                long shapeId2 = page.AddShape(4.0, 2.0, "Rectangle");
                long shapeId3 = page.AddShape(6.0, 2.0, "Rectangle");

                // Retrieve the shape objects
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                Shape shape3 = page.Shapes.GetShape(shapeId3);

                // Helper method to add a custom string property named "Category"
                void AddCategoryProperty(Shape shape, string value)
                {
                    // Create a new Prop (custom property)
                    Prop prop = new Prop();
                    prop.Name = "Category";
                    prop.Value.Val = value;
                    // Optional: explicitly set the property type to string
                    prop.Type.Value = TypePropValue.String;

                    // Add the property to the shape
                    shape.Props.Add(prop);
                }

                // Assign custom property values
                AddCategoryProperty(shape1, "Target");   // This shape should receive a hyperlink
                AddCategoryProperty(shape2, "Other");    // No hyperlink
                AddCategoryProperty(shape3, "Target");   // This shape should receive a hyperlink

                // Define the hyperlink to be added
                string hyperlinkAddress = "https://example.com";

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Look for the custom property "Category"
                    bool isTarget = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "Category" && prop.Value.Val == "Target")
                        {
                            isTarget = true;
                            break;
                        }
                    }

                    // If the shape meets the condition, add a hyperlink
                    if (isTarget)
                    {
                        Hyperlink link = new Hyperlink();
                        link.Name = "WebLink";
                        link.Address.Value = hyperlinkAddress;
                        // Optional: add a description (tooltip)
                        link.Description.Value = "Open example website";

                        // Ensure the Hyperlinks collection is not null (it is always instantiated)
                        shape.Hyperlinks.Add(link);
                    }
                }

                // Save the diagram to a VSDX file
                diagram.Save("ConditionalHyperlinks.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }