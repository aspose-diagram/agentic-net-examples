using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input diagram file path
                string diagramPath = "input.vsdx";
                // XML configuration file path
                string configPath = "HeaderFooterConfig.xml";
                // Output diagram file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Load and parse the XML configuration
                if (!File.Exists(configPath))
                    throw new FileNotFoundException($"Configuration file not found: {configPath}");

                XDocument configDoc = XDocument.Load(configPath);
                XElement root = configDoc.Element("HeaderFooterConfig");
                if (root == null)
                    throw new Exception("Invalid configuration file: missing HeaderFooterConfig root element.");

                // Apply header text
                diagram.HeaderFooter.HeaderLeft = (string)root.Element("HeaderLeft") ?? "";
                diagram.HeaderFooter.HeaderCenter = (string)root.Element("HeaderCenter") ?? "";
                diagram.HeaderFooter.HeaderRight = (string)root.Element("HeaderRight") ?? "";

                // Apply footer text
                diagram.HeaderFooter.FooterLeft = (string)root.Element("FooterLeft") ?? "";
                diagram.HeaderFooter.FooterCenter = (string)root.Element("FooterCenter") ?? "";
                diagram.HeaderFooter.FooterRight = (string)root.Element("FooterRight") ?? "";

                // Apply margins (in inches)
                if (double.TryParse((string)root.Element("HeaderMargin"), out double headerMargin))
                    diagram.HeaderFooter.HeaderMargin.Value = headerMargin;
                if (double.TryParse((string)root.Element("FooterMargin"), out double footerMargin))
                    diagram.HeaderFooter.FooterMargin.Value = footerMargin;

                // Configure global header/footer font
                var font = diagram.HeaderFooter.HeaderFooterFont;
                if (root.Element("FontFace") != null)
                    font.FaceName = (string)root.Element("FontFace");
                if (int.TryParse((string)root.Element("FontWeight"), out int weight))
                    font.Weight = weight; // 700 = bold, 400 = regular
                if (int.TryParse((string)root.Element("FontHeight"), out int height))
                    font.Height = height; // negative value per specification (e.g., -16 for 12pt)

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }