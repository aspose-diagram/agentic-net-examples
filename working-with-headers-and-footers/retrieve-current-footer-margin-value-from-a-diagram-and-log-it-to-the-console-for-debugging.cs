using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve the footer margin (value is in inches)
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