using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

public class HeaderFooterSettings
{
    public string HeaderLeft { get; set; } = string.Empty;
    public string HeaderCenter { get; set; } = string.Empty;
    public string HeaderRight { get; set; } = string.Empty;
    public string FooterLeft { get; set; } = string.Empty;
    public string FooterCenter { get; set; } = string.Empty;
    public string FooterRight { get; set; } = string.Empty;
    public double HeaderMargin { get; set; } = 0.5;
    public double FooterMargin { get; set; } = 0.5;
    public string FontFace { get; set; } = "Calibri";
    public int FontWeight { get; set; } = 400;
    public int FontHeight { get; set; } = -16;
    public Color FontColor { get; set; } = Color.Black;
}

public static class DiagramHelper
{
    public static void ApplyHeaderFooter(string diagramPath, HeaderFooterSettings settings)
    {
        if (string.IsNullOrWhiteSpace(diagramPath))
            throw new ArgumentException("Diagram path must be provided.", nameof(diagramPath));

        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        try
        {
            Diagram diagram = new Diagram(diagramPath);

            diagram.HeaderFooter.HeaderLeft = settings.HeaderLeft;
            diagram.HeaderFooter.HeaderCenter = settings.HeaderCenter;
            diagram.HeaderFooter.HeaderRight = settings.HeaderRight;

            diagram.HeaderFooter.FooterLeft = settings.FooterLeft;
            diagram.HeaderFooter.FooterCenter = settings.FooterCenter;
            diagram.HeaderFooter.FooterRight = settings.FooterRight;

            diagram.HeaderFooter.HeaderMargin.Value = settings.HeaderMargin;
            diagram.HeaderFooter.FooterMargin.Value = settings.FooterMargin;

            var font = diagram.HeaderFooter.HeaderFooterFont;
            font.FaceName = settings.FontFace;
            font.Weight = settings.FontWeight;
            font.Height = settings.FontHeight;

            diagram.HeaderFooter.HeaderFooterColor = settings.FontColor;

            diagram.Save(diagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram '{diagramPath}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath>");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        var settings = new HeaderFooterSettings();
        DiagramHelper.ApplyHeaderFooter(diagramPath, settings);
    }
}