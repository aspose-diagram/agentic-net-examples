using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        TestPageWidthNegativeThrows();
        Console.WriteLine("All tests passed.");
    }

    // Helper to set page width with validation that throws on negative values
    static void SetPageWidth(Page page, double width)
    {
        // Validate width before applying to the diagram model
        if (width < 0)
            throw new ArgumentException("Page width cannot be negative.");

        // Assign the validated width to the page's PageWidth cell
        page.PageSheet.PageProps.PageWidth.Value = width;
    }

    static void TestPageWidthNegativeThrows()
    {
        // Create a new diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            Page page = diagram.Pages[0];

            bool exceptionThrown = false;
            try
            {
                // Attempt to assign a negative width – should raise an ArgumentException
                SetPageWidth(page, -10.0);
            }
            catch (ArgumentException)
            {
                // Expected exception was caught
                exceptionThrown = true;
            }

            if (!exceptionThrown)
            {
                // If no exception was thrown, the test fails
                throw new Exception("Expected ArgumentException was not thrown for negative PageWidth.");
            }

            // Verify that a valid positive width does not throw
            try
            {
                SetPageWidth(page, 8.5);
            }
            catch (Exception ex)
            {
                // Any exception here indicates a failure of the positive case
                throw new Exception("Setting a positive PageWidth threw an unexpected exception: " + ex.Message);
            }
        }
    }
}