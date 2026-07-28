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

            // Choose the page and shape to which the hyperlink will be added
            // Here we use the first page and its first shape as an example
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Create a new hyperlink instance
            Hyperlink hyperlink = new Hyperlink();

            // Set the SubAddress to the target page name within the same diagram
            // This makes the hyperlink navigate to the specified page when activated
            hyperlink.SubAddress.Value = "TargetPageName"; // replace with actual page name

            // (Optional) Clear the Address since we are linking within the same document
            hyperlink.Address.Value = string.Empty;

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(hyperlink);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
