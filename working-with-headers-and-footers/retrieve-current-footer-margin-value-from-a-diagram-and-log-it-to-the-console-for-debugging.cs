using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string filePath = "sample.vsdx";
            Diagram diagram = new Diagram(filePath);

            // Retrieve the current footer margin (in inches)
            double footerMargin = diagram.HeaderFooter.FooterMargin.Value;

            // Output the margin value for debugging
            Console.WriteLine($"Footer margin: {footerMargin} inches");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
