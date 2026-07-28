using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram has at least three pages (index 0‑based)
            if (diagram.Pages.Count < 3)
            {
                throw new Exception("The diagram does not contain a third page to clone.");
            }

            // Retrieve the third page (index 2)
            Page thirdPage = diagram.Pages[2];

            // Create a new blank page that will hold the clone
            Page clonedPage = new Page();

            // Add the new page to the diagram's page collection
            diagram.Pages.Add(clonedPage);

            // Copy the contents of the third page into the new page
            thirdPage.Copy(clonedPage);

            // Modify the height of the cloned page (value is in inches)
            double newHeightInches = 11.0; // example height
            clonedPage.PageSheet.PageProps.PageHeight.Value = newHeightInches;

            // Save the updated diagram back to the same file
            diagram.Save(filePath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
