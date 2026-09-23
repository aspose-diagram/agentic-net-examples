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

            // Insert a footer with automatic page numbering aligned to the right margin
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Optional: set the distance of the footer from the page edge (in inches)
            diagram.HeaderFooter.FooterMargin.Value = 0.5;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
