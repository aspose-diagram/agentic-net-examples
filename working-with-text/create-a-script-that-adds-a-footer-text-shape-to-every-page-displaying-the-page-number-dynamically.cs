using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure the global footer to show the page number dynamically
                // '&p' is the Visio field code for the current page number
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Optional: adjust footer margin (in inches) from the page edge
                diagram.HeaderFooter.FooterMargin.Value = 0.5;

                // Optional: set footer font properties
                diagram.HeaderFooter.HeaderFooterFont.FaceName = "Arial";
                diagram.HeaderFooter.HeaderFooterFont.Height = 12;          // point size
                diagram.HeaderFooter.HeaderFooterFont.Weight = 700;        // 700 = bold
                diagram.HeaderFooter.HeaderFooterFont.Underline = BOOL.False;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }