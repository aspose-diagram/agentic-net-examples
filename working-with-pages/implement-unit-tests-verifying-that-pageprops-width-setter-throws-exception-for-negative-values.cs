using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // No external file inputs are required for this test, so no file existence checks are needed.

        try
        {
            // Create a new empty diagram inside a using block as required by the API guidelines.
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one page. A newly created diagram contains a default page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("FAIL: Diagram does not contain any pages.");
                    return;
                }

                // Retrieve the first page.
                Page page = diagram.Pages[0];

                // Test 1: Setting a negative width should throw an exception.
                bool negativeWidthExceptionThrown = false;
                try
                {
                    // Attempt to set a negative page width.
                    page.PageSheet.PageProps.PageWidth.Value = -5.0;
                }
                catch (Exception ex)
                {
                    // Expected path: an exception is thrown for invalid negative width.
                    negativeWidthExceptionThrown = true;
                    Console.WriteLine($"PASS: Caught expected exception for negative width: {ex.Message}");
                }

                if (!negativeWidthExceptionThrown)
                {
                    Console.Error.WriteLine("FAIL: No exception thrown when setting a negative page width.");
                }

                // Test 2: Setting a valid positive width should succeed without exception.
                bool positiveWidthSet = false;
                try
                {
                    // Set a valid positive width (e.g., 8.5 inches).
                    page.PageSheet.PageProps.PageWidth.Value = 8.5;
                    positiveWidthSet = true;
                    Console.WriteLine("PASS: Positive width set successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"FAIL: Exception thrown when setting a positive width: {ex.Message}");
                }

                // Verify that the width was actually updated.
                if (positiveWidthSet && Math.Abs(page.PageSheet.PageProps.PageWidth.Value - 8.5) > 0.0001)
                {
                    Console.Error.WriteLine("FAIL: Page width value does not match the expected value after setting.");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions from Aspose operations.
            Console.Error.WriteLine($"ERROR: Unexpected exception occurred: {ex.Message}");
        }
    }
}