using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, header/footer XML config path, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: HeaderFooterUpdater <inputDiagram> <configXml> <outputDiagram>");
                return;
            }

            string diagramPath = args[0];
            string configPath = args[1];
            string outputPath = args[2];

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the XML configuration
            XDocument configDoc = XDocument.Load(configPath);
            XElement root = configDoc.Root;
            if (root == null)
            {
                throw new Exception("Invalid XML configuration file.");
            }

            // Apply header text
            XElement headerLeft = root.Element("HeaderLeft");
            if (headerLeft != null) diagram.HeaderFooter.HeaderLeft = headerLeft.Value;

            XElement headerCenter = root.Element("HeaderCenter");
            if (headerCenter != null) diagram.HeaderFooter.HeaderCenter = headerCenter.Value;

            XElement headerRight = root.Element("HeaderRight");
            if (headerRight != null) diagram.HeaderFooter.HeaderRight = headerRight.Value;

            // Apply footer text
            XElement footerLeft = root.Element("FooterLeft");
            if (footerLeft != null) diagram.HeaderFooter.FooterLeft = footerLeft.Value;

            XElement footerCenter = root.Element("FooterCenter");
            if (footerCenter != null) diagram.HeaderFooter.FooterCenter = footerCenter.Value;

            XElement footerRight = root.Element("FooterRight");
            if (footerRight != null) diagram.HeaderFooter.FooterRight = footerRight.Value;

            // Apply margins (values are in inches)
            XElement headerMargin = root.Element("HeaderMargin");
            if (headerMargin != null && double.TryParse(headerMargin.Value, out double hMargin))
            {
                diagram.HeaderFooter.HeaderMargin.Value = hMargin;
            }

            XElement footerMargin = root.Element("FooterMargin");
            if (footerMargin != null && double.TryParse(footerMargin.Value, out double fMargin))
            {
                diagram.HeaderFooter.FooterMargin.Value = fMargin;
            }

            // Apply global font settings for header/footer
            HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;

            XElement fontFace = root.Element("FontFaceName");
            if (fontFace != null) font.FaceName = fontFace.Value;

            XElement fontWeight = root.Element("FontWeight");
            if (fontWeight != null && int.TryParse(fontWeight.Value, out int weight))
            {
                font.Weight = weight; // 700 = bold, 400 = regular
            }

            XElement fontHeight = root.Element("FontHeight");
            if (fontHeight != null && int.TryParse(fontHeight.Value, out int height))
            {
                // Height uses negative calculation mapping; assign directly as per API
                font.Height = height;
            }

            XElement fontItalic = root.Element("FontItalic");
            if (fontItalic != null && bool.TryParse(fontItalic.Value, out bool italic))
            {
                font.Italic = italic ? BOOL.True : BOOL.False;
            }

            XElement fontUnderline = root.Element("FontUnderline");
            if (fontUnderline != null && bool.TryParse(fontUnderline.Value, out bool underline))
            {
                font.Underline = underline ? BOOL.True : BOOL.False;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }