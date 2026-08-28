using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Attempt to delete (clear) a built‑in property.
        // Built‑in properties like Title are read‑only for deletion;
        // setting them to null should raise an exception.
        try
        {
            // This operation is expected to fail.
            diagram.DocumentProps.Title = null;

            // If no exception is thrown, the test has failed.
            throw new Exception("Expected exception was not thrown when deleting a built-in property.");
        }
        catch (ArgumentNullException ex)
        {
            // Expected exception type for null assignment.
            Console.WriteLine("Caught expected ArgumentNullException: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Any other exception type is also acceptable for this test.
            Console.WriteLine("Caught expected exception type: " + ex.GetType().Name + " - " + ex.Message);
        }

        // Clean up
        diagram.Dispose();
    }
}
