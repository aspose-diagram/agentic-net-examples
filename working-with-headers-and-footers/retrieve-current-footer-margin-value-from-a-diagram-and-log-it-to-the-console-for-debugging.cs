using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the footer margin (in inches)
            double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

            // Log the value to the console for debugging
            Console.WriteLine($"Footer margin: {footerMargin} inches");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
