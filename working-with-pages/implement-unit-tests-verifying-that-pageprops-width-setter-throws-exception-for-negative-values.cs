using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram (default constructor)
            using (Diagram diagram = new Diagram())
            {
                // Access the first page (a newly created diagram always contains at least one page)
                Page page = diagram.Pages[0];

                // Test: setting a negative page width should throw an exception
                bool widthExceptionThrown = false;
                try
                {
                    // Attempt to assign a negative width
                    page.PageSheet.PageProps.PageWidth.Value = -5.0;
                    // If no exception occurs, the test fails
                    throw new Exception("Expected exception was not thrown for negative width.");
                }
                catch (Exception)
                {
                    // Any exception is considered a pass for this test
                    widthExceptionThrown = true;
                }

                // Report result for width test
                if (widthExceptionThrown)
                {
                    Console.WriteLine("Test passed: Setting a negative page width throws an exception.");
                }
                else
                {
                    Console.WriteLine("Test failed: Negative page width did not throw an exception.");
                }

                // Test: setting a negative page height should also throw an exception
                bool heightExceptionThrown = false;
                try
                {
                    page.PageSheet.PageProps.PageHeight.Value = -10.0;
                    throw new Exception("Expected exception was not thrown for negative height.");
                }
                catch (Exception)
                {
                    heightExceptionThrown = true;
                }

                // Report result for height test
                if (heightExceptionThrown)
                {
                    Console.WriteLine("Test passed: Setting a negative page height throws an exception.");
                }
                else
                {
                    Console.WriteLine("Test failed: Negative page height did not throw an exception.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}