using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the first page (page index 0)
            // Parameters: PinX, PinY, master name, page index
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the shape instance from the page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the Hyperlinks collection is initialized
            if (shape.Hyperlinks == null)
            {
                throw new Exception("Hyperlinks collection is null.");
            }

            // Verify initial count is zero
            int initialCount = shape.Hyperlinks.Count;
            if (initialCount != 0)
            {
                throw new Exception($"Expected initial hyperlink count to be 0, but got {initialCount}.");
            }
            Console.WriteLine("Initial hyperlink count verified as 0.");

            // Create and add the first hyperlink
            Hyperlink link1 = new Hyperlink();
            link1.Name = "Link1";
            link1.Address.Value = "https://example.com";
            shape.Hyperlinks.Add(link1);

            // Verify count after adding first hyperlink
            int countAfterFirst = shape.Hyperlinks.Count;
            if (countAfterFirst != 1)
            {
                throw new Exception($"Expected hyperlink count to be 1 after adding first link, but got {countAfterFirst}.");
            }
            Console.WriteLine("Hyperlink count after first addition verified as 1.");

            // Create and add a second hyperlink
            Hyperlink link2 = new Hyperlink();
            link2.Name = "Link2";
            link2.Address.Value = "https://contoso.com";
            shape.Hyperlinks.Add(link2);

            // Verify count after adding second hyperlink
            int countAfterSecond = shape.Hyperlinks.Count;
            if (countAfterSecond != 2)
            {
                throw new Exception($"Expected hyperlink count to be 2 after adding second link, but got {countAfterSecond}.");
            }
            Console.WriteLine("Hyperlink count after second addition verified as 2.");

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Hyperlink addition unit test completed successfully.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
