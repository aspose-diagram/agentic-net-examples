using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Helper to set page width with validation
    static void SetPageWidth(Page page, double width)
    {
        // Validate width is non-negative; throw ArgumentException if invalid
        if (width < 0)
            throw new ArgumentException("Page width cannot be negative.");

        // Assign the validated width to the page's PageWidth cell
        page.PageSheet.PageProps.PageWidth.Value = width;
    }

    static void Main()
    {
        // Wrap Aspose operations to capture unexpected errors
        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first page (a default page is created)
                Page page = diagram.Pages[0];

                // Test that setting a positive width works without exception
                try
                {
                    SetPageWidth(page, 8.5); // inches
                    Console.WriteLine("Positive width set successfully.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Setting a positive width should not throw an exception.", ex);
                }

                // Test that setting a negative width throws an exception
                bool exceptionThrown = false;
                try
                {
                    SetPageWidth(page, -5.0); // invalid negative width
                }
                catch (ArgumentException)
                {
                    // Expected exception type
                    exceptionThrown = true;
                    Console.WriteLine("Negative width correctly threw ArgumentException.");
                }
                catch (Exception ex)
                {
                    // Unexpected exception type
                    throw new Exception("Unexpected exception type thrown for negative width.", ex);
                }

                if (!exceptionThrown)
                {
                    throw new Exception("Expected ArgumentException was not thrown for negative width.");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}