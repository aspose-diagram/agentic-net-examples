using System;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: HeaderFooterUpdater <diagramPath> <configXmlPath>");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string configPath = args[1];
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"File not found: {configPath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        XDocument configDoc;
        try
        {
            configDoc = XDocument.Load(configPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading XML configuration: {ex.Message}");
            return;
        }

        XElement root = configDoc.Element("HeaderFooter");
        if (root == null)
        {
            Console.Error.WriteLine("Invalid configuration file: missing <HeaderFooter> root element.");
            return;
        }

        string headerLeft = (string)root.Element("HeaderLeft");
        if (headerLeft != null) diagram.HeaderFooter.HeaderLeft = headerLeft;

        string headerCenter = (string)root.Element("HeaderCenter");
        if (headerCenter != null) diagram.HeaderFooter.HeaderCenter = headerCenter;

        string headerRight = (string)root.Element("HeaderRight");
        if (headerRight != null) diagram.HeaderFooter.HeaderRight = headerRight;

        string footerLeft = (string)root.Element("FooterLeft");
        if (footerLeft != null) diagram.HeaderFooter.FooterLeft = footerLeft;

        string footerCenter = (string)root.Element("FooterCenter");
        if (footerCenter != null) diagram.HeaderFooter.FooterCenter = footerCenter;

        string footerRight = (string)root.Element("FooterRight");
        if (footerRight != null) diagram.HeaderFooter.FooterRight = footerRight;

        XElement headerMarginElem = root.Element("HeaderMargin");
        if (headerMarginElem != null && double.TryParse(headerMarginElem.Value, out double headerMargin))
            diagram.HeaderFooter.HeaderMargin.Value = headerMargin;

        XElement footerMarginElem = root.Element("FooterMargin");
        if (footerMarginElem != null && double.TryParse(footerMarginElem.Value, out double footerMargin))
            diagram.HeaderFooter.FooterMargin.Value = footerMargin;

        XElement colorElem = root.Element("Color");
        if (colorElem != null)
        {
            string hex = colorElem.Value.TrimStart('#');
            if (int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int argb))
            {
                if (hex.Length == 6)
                    argb = (0xFF << 24) | argb; // assume fully opaque if alpha not provided
                diagram.HeaderFooter.HeaderFooterColor = Color.FromArgb(argb);
            }
        }

        XElement fontElem = root.Element("Font");
        if (fontElem != null)
        {
            var font = diagram.HeaderFooter.HeaderFooterFont;

            string faceName = (string)fontElem.Element("FaceName");
            if (faceName != null) font.FaceName = faceName;

            string weightStr = (string)fontElem.Element("Weight");
            if (int.TryParse(weightStr, out int weight)) font.Weight = weight;

            string heightStr = (string)fontElem.Element("Height");
            if (int.TryParse(heightStr, out int height)) font.Height = height;
        }

        string outputPath = Path.Combine(
            Path.GetDirectoryName(diagramPath) ?? "",
            Path.GetFileNameWithoutExtension(diagramPath) + "_Updated.vsdx");

        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with updated header/footer to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}