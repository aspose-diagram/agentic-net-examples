using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                try
                {
                    // Create a new empty diagram
                    Diagram diagram = new Diagram();

                    // Add a rectangle shape to the first page
                    // Parameters: pinX, pinY, width, height, master name, page index
                    long shapeId = diagram.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle", 0);

                    // Retrieve the shape instance from the page
                    Page page = diagram.Pages[0];
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Record the initial hyperlink count
                    int initialCount = shape.Hyperlinks.Count;

                    // Create and add a new hyperlink
                    Hyperlink link = new Hyperlink();
                    link.Address.Value = "https://example.com";
                    shape.Hyperlinks.Add(link);

                    // Verify that the count increased by one
                    int newCount = shape.Hyperlinks.Count;
                    if (newCount != initialCount + 1)
                    {
                        throw new Exception($"Hyperlink count mismatch. Expected {initialCount + 1}, but got {newCount}.");
                    }

                    Console.WriteLine("Test passed: Hyperlink collection count correctly updated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Test failed: {ex.Message}");
                    // Re-throw to indicate failure in a test environment
                    throw;
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }