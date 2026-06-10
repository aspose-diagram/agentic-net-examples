using System;
using System.IO;

// Load an existing Visio diagram
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Access the HeaderFooter object
            HeaderFooter hf = diagram.HeaderFooter;

            // Set the header to display the document title (centered)
            // If the document title is not set, you can assign a custom title here
            hf.HeaderCenter = diagram.DocumentProps.Title ?? "My Diagram Title";

            // Set the footer to display the page number (centered)
            // Visio field codes can be inserted using the special syntax &[Page]
            // Aspose.Diagram will preserve this field and render the correct page number on each page
            hf.FooterCenter = "Page &[Page]";

            // Optionally, you can set left/right portions or margins if needed
            // hf.HeaderLeft = "Left Header Text";
            // hf.FooterRight = "Right Footer Text";

            // Save the diagram back to a file (preserving the original format)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
