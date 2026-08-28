using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Configure the global footer font (applies to all footer fields)
                var footerFont = diagram.HeaderFooter.HeaderFooterFont;
                footerFont.FaceName = "Times New Roman";   // Font family
                footerFont.Height = 9;                     // Font size in points
                footerFont.Italic = BOOL.True;             // Italic style
                // Optional: set normal weight (400) if needed
                footerFont.Weight = 400;

                // Example: set the center footer text (content can be adjusted as needed)
                diagram.HeaderFooter.FooterCenter = "Center Footer";

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }