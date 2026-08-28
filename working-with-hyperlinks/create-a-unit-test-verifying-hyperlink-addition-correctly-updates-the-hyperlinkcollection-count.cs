using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the first page
                // Parameters: pinX, pinY, master name, page index
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Verify the shape's Hyperlinks collection is initially empty
                int initialCount = shape.Hyperlinks.Count;
                if (initialCount != 0)
                {
                    throw new Exception($"Expected initial hyperlink count to be 0, but got {initialCount}.");
                }

                // Create a new hyperlink and set its address
                Hyperlink link = new Hyperlink();
                link.Name = "TestLink";
                link.Address.Value = "https://example.com";

                // Add the hyperlink to the shape
                shape.Hyperlinks.Add(link);

                // Verify the Hyperlinks collection count has increased to 1
                int finalCount = shape.Hyperlinks.Count;
                if (finalCount != 1)
                {
                    throw new Exception($"Expected hyperlink count after addition to be 1, but got {finalCount}.");
                }

                // Output success message
                Console.WriteLine("Hyperlink addition test passed. Hyperlink count is correctly updated.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }