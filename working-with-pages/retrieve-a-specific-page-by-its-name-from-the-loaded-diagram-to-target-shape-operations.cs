using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // (Assuming the create/load rule provides a method to load a diagram from a file)
            Diagram diagram = new Diagram(@"C:\Diagrams\sample.vsdx");

            // Retrieve the page with the specified name
            // The PageCollection.GetPage(string) method returns the Page object matching the name.
            string targetPageName = "MyPage";
            Page targetPage = diagram.Pages.GetPage(targetPageName);

            // Verify that the page was found
            if (targetPage == null)
            {
                Console.WriteLine($"Page \"{targetPageName}\" not found in the diagram.");
                return;
            }

            Console.WriteLine($"Successfully retrieved page \"{targetPage.Name}\" (ID: {targetPage.ID}).");

            // Example: retrieve a shape by name on the obtained page
            string shapeName = "MyShape";
            Shape shape = targetPage.Shapes.GetShape(shapeName);

            if (shape != null)
            {
                Console.WriteLine($"Shape \"{shape.Name}\" found on page \"{targetPage.Name}\".");
                // Perform further shape operations here...
            }
            else
            {
                Console.WriteLine($"Shape \"{shapeName}\" not found on page \"{targetPage.Name}\".");
            }

            // Save the diagram if any modifications were made
            diagram.Save(@"C:\Diagrams\sample_modified.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
