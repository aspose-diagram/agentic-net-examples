using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class HeaderSettings
{
    // Header text segments
    public string HeaderLeft { get; set; }
    public string HeaderCenter { get; set; }
    public string HeaderRight { get; set; }

    // Footer text segments
    public string FooterLeft { get; set; }
    public string FooterCenter { get; set; }
    public string FooterRight { get; set; }

    // Margins (in inches)
    public double HeaderMargin { get; set; }
    public double FooterMargin { get; set; }

    // Font properties for header/footer
    public string FontFace { get; set; }
    public int FontWeight { get; set; } // 700 = bold, 400 = regular
    public int FontHeight { get; set; } // in points
    public BOOL Italic { get; set; }
    public BOOL Underline { get; set; }
    public Color FontColor { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Prepare sample header/footer settings
        HeaderSettings settings = new HeaderSettings
        {
            HeaderLeft = "Company",
            HeaderCenter = "Quarterly Report",
            HeaderRight = "Date: &d",
            FooterLeft = "Confidential",
            FooterCenter = "",
            FooterRight = "Page &p of &P",
            HeaderMargin = 0.5,
            FooterMargin = 0.5,
            FontFace = "Arial",
            FontWeight = 700,
            FontHeight = 12,
            Italic = BOOL.False,
            Underline = BOOL.False,
            FontColor = Color.Black
        };

        // Apply settings and save the diagram
        ApplyHeaderSettings(diagramPath, settings);
    }

    /// <summary>
    /// Loads a Visio diagram, updates its header/footer settings, and saves it.
    /// </summary>
    /// <param name="diagramPath">Path to the Visio file.</param>
    /// <param name="settings">Header and footer configuration.</param>
    static void ApplyHeaderSettings(string diagramPath, HeaderSettings settings)
    {
        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Update header text
            diagram.HeaderFooter.HeaderLeft = settings.HeaderLeft;
            diagram.HeaderFooter.HeaderCenter = settings.HeaderCenter;
            diagram.HeaderFooter.HeaderRight = settings.HeaderRight;

            // Update footer text
            diagram.HeaderFooter.FooterLeft = settings.FooterLeft;
            diagram.HeaderFooter.FooterCenter = settings.FooterCenter;
            diagram.HeaderFooter.FooterRight = settings.FooterRight;

            // Update margins (use .Value to set the double)
            diagram.HeaderFooter.HeaderMargin.Value = settings.HeaderMargin;
            diagram.HeaderFooter.FooterMargin.Value = settings.FooterMargin;

            // Configure font properties for both header and footer
            HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;
            font.FaceName = settings.FontFace;
            font.Weight = settings.FontWeight;
            font.Height = settings.FontHeight;
            font.Italic = settings.Italic;
            font.Underline = settings.Underline;

            // Set the font color
            diagram.HeaderFooter.HeaderFooterColor = settings.FontColor;

            // Save the updated diagram (overwrites the original file)
            diagram.Save(diagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}