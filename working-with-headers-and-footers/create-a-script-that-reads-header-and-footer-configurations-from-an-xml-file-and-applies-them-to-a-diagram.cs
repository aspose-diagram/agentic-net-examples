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
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configXmlPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the XML configuration file exists.
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration XML file not found: {configPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Load and parse the XML configuration.
                XDocument xmlDoc = XDocument.Load(configPath);
                XElement root = xmlDoc.Element("HeaderFooterConfig");
                if (root == null)
                {
                    Console.Error.WriteLine("Invalid XML format: missing <HeaderFooterConfig> root element.");
                    return;
                }

                // ----- Header text -----
                XElement headerLeftElem = root.Element("HeaderLeft");
                if (headerLeftElem != null)
                    diagram.HeaderFooter.HeaderLeft = headerLeftElem.Value;

                XElement headerCenterElem = root.Element("HeaderCenter");
                if (headerCenterElem != null)
                    diagram.HeaderFooter.HeaderCenter = headerCenterElem.Value;

                XElement headerRightElem = root.Element("HeaderRight");
                if (headerRightElem != null)
                    diagram.HeaderFooter.HeaderRight = headerRightElem.Value;

                // ----- Footer text -----
                XElement footerLeftElem = root.Element("FooterLeft");
                if (footerLeftElem != null)
                    diagram.HeaderFooter.FooterLeft = footerLeftElem.Value;

                XElement footerCenterElem = root.Element("FooterCenter");
                if (footerCenterElem != null)
                    diagram.HeaderFooter.FooterCenter = footerCenterElem.Value;

                XElement footerRightElem = root.Element("FooterRight");
                if (footerRightElem != null)
                    diagram.HeaderFooter.FooterRight = footerRightElem.Value;

                // ----- Margins -----
                XElement headerMarginElem = root.Element("HeaderMargin");
                if (headerMarginElem != null && double.TryParse(headerMarginElem.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double headerMargin))
                    diagram.HeaderFooter.HeaderMargin.Value = headerMargin;

                XElement footerMarginElem = root.Element("FooterMargin");
                if (footerMarginElem != null && double.TryParse(footerMarginElem.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double footerMargin))
                    diagram.HeaderFooter.FooterMargin.Value = footerMargin;

                // ----- Font settings -----
                HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;

                XElement fontFaceElem = root.Element("FontFace");
                if (fontFaceElem != null)
                    font.FaceName = fontFaceElem.Value;

                // Font size is set via the Height property (negative value mapping).
                XElement fontSizeElem = root.Element("FontSize");
                if (fontSizeElem != null && double.TryParse(fontSizeElem.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double fontSize))
                {
                    // Apply the required negative mapping: Height = round(PointSize * -1.333)
                    int height = (int)Math.Round(fontSize * -1.333);
                    font.Height = height;
                }

                // Boldness is controlled via the Weight property (700 = bold, 400 = regular).
                XElement boldElem = root.Element("Bold");
                if (boldElem != null && bool.TryParse(boldElem.Value, out bool isBold))
                    font.Weight = isBold ? 700 : 400;

                // Italic flag uses BOOL enum.
                XElement italicElem = root.Element("Italic");
                if (italicElem != null && bool.TryParse(italicElem.Value, out bool isItalic))
                    font.Italic = isItalic ? BOOL.True : BOOL.False;

                // Underline flag uses BOOL enum.
                XElement underlineElem = root.Element("Underline");
                if (underlineElem != null && bool.TryParse(underlineElem.Value, out bool isUnderline))
                    font.Underline = isUnderline ? BOOL.True : BOOL.False;

                // ----- Header/Footer text color -----
                XElement colorElem = root.Element("Color");
                if (colorElem != null)
                {
                    string colorStr = colorElem.Value.Trim();
                    // Expecting a hex string like "#RRGGBB" or a known color name.
                    try
                    {
                        Color parsedColor;
                        if (colorStr.StartsWith("#") && colorStr.Length == 7)
                        {
                            // Parse hex RGB.
                            int rgb = int.Parse(colorStr.Substring(1), NumberStyles.HexNumber);
                            // Aspose.Drawing.Color.FromArgb expects ARGB; set alpha to 255.
                            parsedColor = Color.FromArgb(255, (rgb >> 16) & 0xFF, (rgb >> 8) & 0xFF, rgb & 0xFF);
                        }
                        else
                        {
                            // Fallback to known color names (e.g., "Black").
                            parsedColor = Color.FromName(colorStr);
                        }
                        diagram.HeaderFooter.HeaderFooterColor = parsedColor;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to parse color '{colorStr}': {ex.Message}");
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}