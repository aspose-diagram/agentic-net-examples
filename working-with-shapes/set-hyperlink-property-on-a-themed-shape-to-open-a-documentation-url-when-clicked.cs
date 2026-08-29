using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new blank diagram
                Diagram diagram = new Diagram();

                // Get the active page where the shape will be placed
                Page page = diagram.ActivePage;

                // Add a rectangle shape (master name "Rectangle") at position (2, 2) inches
                // The AddShape method returns the shape ID (long)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the Shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new Hyperlink instance
                Hyperlink link = new Hyperlink();

                // Set the URL that will be opened when the shape is clicked
                link.Address.Value = "https://example.com/documentation";

                // Optional: set a description (tooltip) for the hyperlink
                link.Description.Value = "Open documentation";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Save the diagram to a VSDX file
                diagram.Save("ThemedShapeWithHyperlink.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }