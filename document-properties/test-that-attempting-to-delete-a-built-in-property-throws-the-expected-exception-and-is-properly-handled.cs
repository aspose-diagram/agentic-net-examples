using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {
            // Create an empty diagram
            Diagram diagram = new Diagram();

            // Access a built‑in property (Title)
            string originalTitle = diagram.DocumentProps.Title;
            Console.WriteLine($"Original Title: {originalTitle}");

            // Attempt to delete the built‑in property by removing a custom property with the same name.
            // This operation is expected to throw an exception.
            try
            {
                // Create a CustomProp that mimics the built‑in property name
                CustomProp fakeProp = new CustomProp
                {
                    Name = "Title",
                    PropType = PropType.String,
                    CustomValue = new CustomValue { ValueString = "Fake" }
                };

                // Attempt to remove it from the custom properties collection.
                // Since this property does not exist, an exception should be thrown.
                diagram.DocumentProps.CustomProps.Remove(fakeProp);

                // If no exception is thrown, the test has failed
                throw new Exception("Expected exception was not thrown when attempting to delete a built‑in property.");
            }
            catch (Exception ex)
            {
                // Expected path: an exception is thrown because the built‑in property cannot be deleted this way
                Console.WriteLine("Caught expected exception: " + ex.Message);
            }

            // Verify that the built‑in property remains unchanged
            if (diagram.DocumentProps.Title != originalTitle)
            {
                throw new Exception("Built‑in property Title was altered unexpectedly.");
            }
            else
            {
                Console.WriteLine("Built‑in property Title remains unchanged as expected.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}