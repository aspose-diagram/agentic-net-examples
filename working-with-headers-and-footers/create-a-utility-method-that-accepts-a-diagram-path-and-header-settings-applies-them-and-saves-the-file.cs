using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum
using Aspose.Drawing; // Required for Color struct

// Settings container for header and footer configuration
public class HeaderSettings
{
    public string HeaderLeft { get; set; } = string.Empty;
    public string HeaderCenter { get; set; } = string.Empty;
    public string HeaderRight { get; set; } = string.Empty;

    public string FooterLeft { get; set; } = string.Empty;
    public string FooterCenter { get; set; } = string.Empty;
    public string FooterRight { get; set; } = string.Empty;

    // Margins are expressed in inches
    public double HeaderMargin { get; set; } = 0.0;
    public double FooterMargin { get; set; } = 0.0;

    // Font appearance for header/footer text
    public string FontFaceName { get; set; } = "Arial";
    public int FontWeight { get; set; } = 400; // 400 = normal, 700 = bold
    public int FontPointSize { get; set; } = 12; // point size
    public Color FontColor { get; set; } = Color.Black;
}

// Utility class that applies header/footer settings to a Visio diagram
public static class HeaderUtility
{
    /// <summary>
    /// Loads a Visio diagram, applies header/footer settings, and saves the file.
    /// </summary>
    /// <param name="diagramPath">Full path to the .vsdx (or other supported) file.</param>
    /// <param name="settings">Header and footer configuration.</param>
    public static void ApplyHeaderSettings(string diagramPath, HeaderSettings settings)
    {
        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Apply header text values (use empty string if null)
            diagram.HeaderFooter.HeaderLeft = settings.HeaderLeft ?? string.Empty;
            diagram.HeaderFooter.HeaderCenter = settings.HeaderCenter ?? string.Empty;
            diagram.HeaderFooter.HeaderRight = settings.HeaderRight ?? string.Empty;

            // Apply footer text values (use empty string if null)
            diagram.HeaderFooter.FooterLeft = settings.FooterLeft ?? string.Empty;
            diagram.HeaderFooter.FooterCenter = settings.FooterCenter ?? string.Empty;
            diagram.HeaderFooter.FooterRight = settings.FooterRight ?? string.Empty;

            // Apply margins (in inches) via the Margin.Value property
            diagram.HeaderFooter.HeaderMargin.Value = settings.HeaderMargin;
            diagram.HeaderFooter.FooterMargin.Value = settings.FooterMargin;

            // Configure font appearance for header/footer
            var headerFooterFont = diagram.HeaderFooter.HeaderFooterFont;

            if (!string.IsNullOrEmpty(settings.FontFaceName))
                headerFooterFont.FaceName = settings.FontFaceName; // Set typeface name

            if (settings.FontWeight != 0)
                headerFooterFont.Weight = settings.FontWeight; // Set weight (400 = normal, 700 = bold)

            if (settings.FontPointSize > 0)
            {
                // Height uses a negative mapping: Height = -(DesiredPointSize * 1.333) rounded
                int mappedHeight = -(int)Math.Round(settings.FontPointSize * 1.333);
                headerFooterFont.Height = mappedHeight;
            }

            // Set the font color for header/footer text
            diagram.HeaderFooter.HeaderFooterColor = settings.FontColor;

            // Save the diagram, overwriting the original file (using Vsdx format)
            diagram.Save(diagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
        }
    }
}

// Entry point for the console application
class Program
{
    static void Main(string[] args)
    {
        // Expect at least one argument: the path to the diagram file
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: HeaderUtility <diagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the provided path points to an existing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Create a HeaderSettings instance with default values (customize as needed)
        HeaderSettings settings = new HeaderSettings
        {
            HeaderCenter = "Confidential",
            FooterRight = "Page: &p",
            FontWeight = 700, // Bold
            FontPointSize = 14,
            FontColor = Color.DarkBlue
        };

        // Apply the header/footer configuration to the diagram
        HeaderUtility.ApplyHeaderSettings(diagramPath, settings);
    }
}