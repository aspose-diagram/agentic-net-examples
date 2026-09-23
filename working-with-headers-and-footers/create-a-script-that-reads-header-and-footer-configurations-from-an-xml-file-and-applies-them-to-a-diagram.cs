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
            // Expect three arguments: input diagram path, XML config path, output diagram path
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: HeaderFooterUpdater <inputDiagram> <configXml> <outputDiagram>");
                return;
            }

            string diagramPath = args[0];
            string xmlPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load XML configuration
            XDocument configDoc = XDocument.Load(xmlPath);
            XElement root = configDoc.Root;
            if (root == null)
            {
                Console.WriteLine("Invalid XML configuration.");
                return;
            }

            // Helper to get element value safely
            string GetValue(string elementName)
            {
                XElement el = root.Element(elementName);
                return el != null ? el.Value.Trim() : null;
            }

            // Apply header text values
            string headerLeft = GetValue("HeaderLeft");
            if (!string.IsNullOrEmpty(headerLeft))
                diagram.HeaderFooter.HeaderLeft = headerLeft;

            string headerCenter = GetValue("HeaderCenter");
            if (!string.IsNullOrEmpty(headerCenter))
                diagram.HeaderFooter.HeaderCenter = headerCenter;

            string headerRight = GetValue("HeaderRight");
            if (!string.IsNullOrEmpty(headerRight))
                diagram.HeaderFooter.HeaderRight = headerRight;

            // Apply footer text values
            string footerLeft = GetValue("FooterLeft");
            if (!string.IsNullOrEmpty(footerLeft))
                diagram.HeaderFooter.FooterLeft = footerLeft;

            string footerCenter = GetValue("FooterCenter");
            if (!string.IsNullOrEmpty(footerCenter))
                diagram.HeaderFooter.FooterCenter = footerCenter;

            string footerRight = GetValue("FooterRight");
            if (!string.IsNullOrEmpty(footerRight))
                diagram.HeaderFooter.FooterRight = footerRight;

            // Apply margins (values are in inches)
            string headerMarginStr = GetValue("HeaderMargin");
            if (double.TryParse(headerMarginStr, out double headerMargin))
                diagram.HeaderFooter.HeaderMargin.Value = headerMargin;

            string footerMarginStr = GetValue("FooterMargin");
            if (double.TryParse(footerMarginStr, out double footerMargin))
                diagram.HeaderFooter.FooterMargin.Value = footerMargin;

            // Apply font settings
            HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;

            string fontName = GetValue("FontName");
            if (!string.IsNullOrEmpty(fontName))
                font.FaceName = fontName;

            string fontSizeStr = GetValue("FontSize");
            if (int.TryParse(fontSizeStr, out int fontSize))
                font.Height = fontSize; // Height is an integer representing point size (negative mapping handled internally)

            string fontWeightStr = GetValue("FontWeight");
            if (int.TryParse(fontWeightStr, out int fontWeight))
                font.Weight = fontWeight; // 700 = Bold, 400 = Regular

            string underlineStr = GetValue("Underline");
            if (bool.TryParse(underlineStr, out bool underline))
                font.Underline = underline ? BOOL.True : BOOL.False;

            string italicStr = GetValue("Italic");
            if (bool.TryParse(italicStr, out bool italic))
                font.Italic = italic ? BOOL.True : BOOL.False;

            // Apply color (hex string like "#FF0000")
            string colorHex = GetValue("Color");
            if (!string.IsNullOrEmpty(colorHex))
            {
                // Aspose.Drawing.Color supports FromArgb with ARGB components; parse hex
                if (colorHex.StartsWith("#"))
                {
                    // Remove '#'
                    string hex = colorHex.Substring(1);
                    // Support 6 or 8 digit hex
                    if (hex.Length == 6)
                    {
                        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
                        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
                        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
                        diagram.HeaderFooter.HeaderFooterColor = Color.FromArgb(r, g, b);
                    }
                    else if (hex.Length == 8)
                    {
                        int a = Convert.ToInt32(hex.Substring(0, 2), 16);
                        int r = Convert.ToInt32(hex.Substring(2, 2), 16);
                        int g = Convert.ToInt32(hex.Substring(4, 2), 16);
                        int b = Convert.ToInt32(hex.Substring(6, 2), 16);
                        diagram.HeaderFooter.HeaderFooterColor = Color.FromArgb(a, r, g, b);
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }