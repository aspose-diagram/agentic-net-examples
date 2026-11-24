using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Set header to display the diagram title (document property Title)
                // -----------------------------------------------------------------
                string title = diagram.DocumentProps.Title;
                diagram.HeaderFooter.HeaderCenter = title ?? string.Empty;

                // -----------------------------------------------------------------
                // Set footer to display dynamic page number and total pages
                // Visio field codes: &p = current page number, &P = total pages
                // -----------------------------------------------------------------
                diagram.HeaderFooter.FooterRight = "Page &p of &P";

                // -----------------------------------------------------------------
                // Adjust margins (in inches) for header and footer
                // -----------------------------------------------------------------
                diagram.HeaderFooter.HeaderMargin.Value = 0.5; // half inch from top
                diagram.HeaderFooter.FooterMargin.Value = 0.5; // half inch from bottom

                // -----------------------------------------------------------------
                // Configure header/footer font (Arial, 12pt, bold)
                // -----------------------------------------------------------------
                HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;
                font.FaceName = "Arial";
                font.Height = 12;               // Font size in points
                font.Weight = 700;              // 700 = bold
                font.Italic = BOOL.False;
                font.Underline = BOOL.False;

                // -----------------------------------------------------------------
                // Set header/footer text color (black)
                // -----------------------------------------------------------------
                diagram.HeaderFooter.HeaderFooterColor = Color.Black;

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}