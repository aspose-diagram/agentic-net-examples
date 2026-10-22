using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Instantiate a new empty Diagram object
            Diagram diagram = new Diagram();

            // Create a new blank page
            Page newPage = new Page();
            // Optionally set a name for the new page
            newPage.Name = "NewPage";

            // Add the new page to the diagram's Pages collection
            diagram.Pages.Add(newPage);

            // Output the total number of pages to confirm addition
            Console.WriteLine($"Page added successfully. Total pages in diagram: {diagram.Pages.Count}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}