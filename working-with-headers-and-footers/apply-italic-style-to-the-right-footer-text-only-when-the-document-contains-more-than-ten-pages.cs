using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Check if the document has more than ten pages
                if (diagram.Pages.Count > 10)
                {
                    // Apply italic style to the footer font
                    diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;

                    // Example: set right footer text (optional)
                    diagram.HeaderFooter.FooterRight = "Page &p of &P";
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }