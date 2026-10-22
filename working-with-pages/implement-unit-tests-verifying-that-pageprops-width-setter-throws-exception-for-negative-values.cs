using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Run the test that verifies setting a negative page width throws an exception.
        TestNegativePageWidth();
    }

    /// <summary>
    /// Creates a new diagram, accesses its first page, and attempts to set a negative width.
    /// The test passes if an exception is thrown; otherwise it fails.
    /// </summary>
    static void TestNegativePageWidth()
    {
        try
        {
            // Create an empty diagram. The default constructor creates a diagram with a single page.
            using (Diagram diagram = new Diagram())
            {
                // Access the first (and only) page.
                Page page = diagram.Pages[0];

                // Attempt to set a negative width. According to Aspose.Diagram API,
                // this should raise an ArgumentException (or a derived exception).
                page.PageSheet.PageProps.PageWidth.Value = -5.0;

                // If no exception is thrown, the test has failed.
                Console.Error.WriteLine("TestNegativePageWidth FAILED: No exception was thrown for negative width.");
            }
        }
        catch (Exception ex)
        {
            // Expected path: an exception should be thrown for negative width.
            Console.WriteLine("TestNegativePageWidth PASSED: Caught expected exception -> " + ex.Message);
        }
    }
}