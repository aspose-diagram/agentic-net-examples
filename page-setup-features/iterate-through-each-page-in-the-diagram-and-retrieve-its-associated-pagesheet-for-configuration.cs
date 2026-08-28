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

                // Example: access page layout and page properties for configuration
                PageLayout layout = pageSheet.PageLayout;
                PageProps props = pageSheet.PageProps;

                // Output basic information (you can replace this with actual configuration logic)
                Console.WriteLine($"Page Name: {page.Name}");
                // The PageProps class contains properties such as Width and Height.
                // Uncomment the following lines if you need to read those values.
                // Console.WriteLine($"Width: {props.Width}, Height: {props.Height}");
            }

            // Save the diagram after any modifications (if any)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
