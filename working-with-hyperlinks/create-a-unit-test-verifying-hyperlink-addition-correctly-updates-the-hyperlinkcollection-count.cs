using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new shape instance (not attached to any diagram)
            Shape shape = new Shape();

            // Verify initial Hyperlink collection count is zero
            int initialCount = shape.Hyperlinks.Count;
            if (initialCount != 0)
            {
                throw new Exception($"Expected initial Hyperlink count to be 0, but got {initialCount}.");
            }
            Console.WriteLine("Initial Hyperlink count verified as 0.");

            // Create a new Hyperlink and set its address
            Hyperlink link = new Hyperlink();
            link.Address.Value = "https://example.com";
            link.Name = "ExampleLink";

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Verify the Hyperlink collection count is now 1
            int afterAddCount = shape.Hyperlinks.Count;
            if (afterAddCount != 1)
            {
                throw new Exception($"Expected Hyperlink count after addition to be 1, but got {afterAddCount}.");
            }
            Console.WriteLine("Hyperlink addition verified; count is now 1.");

            // Optionally, verify the added hyperlink's properties
            Hyperlink retrievedLink = shape.Hyperlinks[0];
            if (retrievedLink.Address.Value != "https://example.com")
            {
                throw new Exception("Hyperlink address does not match the expected value.");
            }
            Console.WriteLine("Hyperlink address verified successfully.");

            Console.WriteLine("All hyperlink tests passed.");
        }
    }