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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the PageSheet associated with the current page
                PageSheet pageSheet = page.PageSheet;

                // Example: access page layout or page properties from the PageSheet
                // (Here we just demonstrate accessing the PageProps object)
                PageProps props = pageSheet.PageProps;

                // Output some basic information about the page
                Console.WriteLine($"Page Name: {page.Name}");
                // If needed, you can further inspect properties such as width/height from props
                // Console.WriteLine($"Page Width: {props.Width}, Height: {props.Height}");
            }

            // Save the diagram (if any modifications were made to the PageSheets)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
