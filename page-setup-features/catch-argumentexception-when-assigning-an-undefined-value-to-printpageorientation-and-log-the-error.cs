using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        // Create an empty diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Access the first page
            Page page = diagram.Pages[0];

            try
            {
                // Assign an invalid enum value to trigger ArgumentException
                page.PageSheet.PrintProps.PrintPageOrientation.Value = (PrintPageOrientationValue)999;
            }
            catch (ArgumentException ex)
            {
                // Log the error details
                Console.WriteLine($"Error assigning PrintPageOrientation: {ex.Message}");
            }
        }
    }
}
