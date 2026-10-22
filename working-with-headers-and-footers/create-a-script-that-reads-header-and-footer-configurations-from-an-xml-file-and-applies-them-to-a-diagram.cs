using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, XML config file, output diagram file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configXmlPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config XML file not found: {configPath}");
            return;
        }

        // Load XML configuration
        XDocument configDoc;
        try
        {
            configDoc = XDocument.Load(configPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XML configuration: {ex.Message}");
            return;
        }

        // Helper to retrieve element value safely
        string GetElementValue(string name)
        {
            XElement elem = configDoc.Root.Element(name);
            return elem != null ? elem.Value.Trim() : null;
        }

        // Header and footer text
        string headerLeft   = GetElementValue("HeaderLeft");
        string headerCenter = GetElementValue("HeaderCenter");
        string headerRight  = GetElementValue("HeaderRight");
        string footerLeft   = GetElementValue("FooterLeft");
        string footerCenter = GetElementValue("FooterCenter");
        string footerRight  = GetElementValue("FooterRight");

        // Margins (in inches)
        double? headerMargin = null;
        double? footerMargin = null;
        string headerMarginStr = GetElementValue("HeaderMargin");
        string footerMarginStr = GetElementValue("FooterMargin");
        if (double.TryParse(headerMarginStr, out double hm)) headerMargin = hm;
        if (double.TryParse(footerMarginStr, out double fm)) footerMargin = fm;

        // Font settings
        string fontFaceName = GetElementValue("FontFaceName");
        int? fontSizePt = null;
        string fontSizeStr = GetElementValue("FontSize");
        if (int.TryParse(fontSizeStr, out int fs)) fontSizePt = fs;

        bool? fontBold = null;
        bool? fontItalic = null;
        bool? fontUnderline = null;
        string boldStr = GetElementValue("FontBold");
        string italicStr = GetElementValue("FontItalic");
        string underlineStr = GetElementValue("FontUnderline");
        if (bool.TryParse(boldStr, out bool parsedBold)) fontBold = parsedBold;
        if (bool.TryParse(italicStr, out bool parsedItalic)) fontItalic = parsedItalic;
        if (bool.TryParse(underlineStr, out bool parsedUnderline)) fontUnderline = parsedUnderline;

        // Font color (hex format #RRGGBB)
        Color fontColor = Color.Black; // default color
        string colorHex = GetElementValue("FontColor");
        if (!string.IsNullOrEmpty(colorHex) && colorHex.StartsWith("#") && colorHex.Length == 7)
        {
            try
            {
                int r = Convert.ToInt32(colorHex.Substring(1, 2), 16);
                int g = Convert.ToInt32(colorHex.Substring(3, 2), 16);
                int b = Convert.ToInt32(colorHex.Substring(5, 2), 16);
                fontColor = Color.FromArgb(r, g, b);
            }
            catch
            {
                // Keep default color if parsing fails
            }
        }

        // Load diagram and apply settings
        try
        {
            // NOTE: Diagram does NOT implement IDisposable; instantiate directly
            Diagram diagram = new Diagram(diagramPath);

            // Apply header/footer text
            if (headerLeft != null)   diagram.HeaderFooter.HeaderLeft   = headerLeft;
            if (headerCenter != null) diagram.HeaderFooter.HeaderCenter = headerCenter;
            if (headerRight != null)  diagram.HeaderFooter.HeaderRight  = headerRight;
            if (footerLeft != null)   diagram.HeaderFooter.FooterLeft   = footerLeft;
            if (footerCenter != null) diagram.HeaderFooter.FooterCenter = footerCenter;
            if (footerRight != null)  diagram.HeaderFooter.FooterRight  = footerRight;

            // Apply margins
            if (headerMargin.HasValue) diagram.HeaderFooter.HeaderMargin.Value = headerMargin.Value;
            if (footerMargin.HasValue) diagram.HeaderFooter.FooterMargin.Value = footerMargin.Value;

            // Configure font properties
            HeaderFooterFont hfFont = diagram.HeaderFooter.HeaderFooterFont;

            if (!string.IsNullOrEmpty(fontFaceName))
                hfFont.FaceName = fontFaceName;

            if (fontBold.HasValue)
                hfFont.Weight = fontBold.Value ? 700 : 400; // 700 = Bold, 400 = Regular

            if (fontSizePt.HasValue)
            {
                // Convert point size to negative height (Visio expects negative value)
                int height = (int)Math.Round(fontSizePt.Value * -1.333);
                hfFont.Height = height;
            }

            if (fontItalic.HasValue)
                hfFont.Italic = fontItalic.Value ? BOOL.True : BOOL.False;

            if (fontUnderline.HasValue)
                hfFont.Underline = fontUnderline.Value ? BOOL.True : BOOL.False;

            // Apply font color
            diagram.HeaderFooter.HeaderFooterColor = fontColor;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}