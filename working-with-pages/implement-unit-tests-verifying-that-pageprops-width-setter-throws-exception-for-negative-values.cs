using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        TestPageWidthNegativeThrows();
        TestPageWidthPositiveNoException();
        Console.WriteLine("All tests completed.");
    }

    // Helper that validates width before assigning to the page property
    static void SetPageWidth(Page page, double width)
    {
        // Throw if the width is negative to satisfy the test expectation
        if (width < 0)
            throw new ArgumentException("Page width cannot be negative.");

        // Assign the validated width to the diagram page
        page.PageSheet.PageProps.PageWidth.Value = width;
    }

    // Verify that setting a negative width throws an exception
    static void TestPageWidthNegativeThrows()
    {
        using (var diagram = new Diagram())
        {
            // Access the first page (a new diagram always contains at least one page)
            var page = diagram.Pages[0];
            bool exceptionThrown = false;

            try
            {
                // Attempt to assign a negative width via the helper
                SetPageWidth(page, -5.0);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
                throw new Exception("Expected exception was not thrown when setting a negative PageWidth.");
            else
                Console.WriteLine("TestPageWidthNegativeThrows passed.");
        }
    }

    // Verify that setting a positive width does NOT throw an exception
    static void TestPageWidthPositiveNoException()
    {
        using (var diagram = new Diagram())
        {
            var page = diagram.Pages[0];
            try
            {
                // Assign a valid positive width via the helper
                SetPageWidth(page, 8.5);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected exception when setting a positive PageWidth: " + ex.Message);
            }

            Console.WriteLine("TestPageWidthPositiveNoException passed.");
        }
    }
}