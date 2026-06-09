using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the footer margin (in inches) from the global HeaderFooter settings
            double footerMarginInches = diagram.HeaderFooter.FooterMargin.Value;

            // Log the value to the console for debugging
            Console.WriteLine($"Current footer margin: {footerMarginInches} inches");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
