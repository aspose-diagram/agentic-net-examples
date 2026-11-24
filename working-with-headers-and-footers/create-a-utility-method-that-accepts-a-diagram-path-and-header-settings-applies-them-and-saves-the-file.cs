using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHeaderUtility
{
    // Class representing header and footer configuration
    public class HeaderFooterSettings
    {
        // Header text segments
        public string HeaderLeft { get; set; } = string.Empty;
        public string HeaderCenter { get; set; } = string.Empty;
        public string HeaderRight { get; set; } = string.Empty;

        // Footer text segments
        public string FooterLeft { get; set; } = string.Empty;
        public string FooterCenter { get; set; } = string.Empty;
        public string FooterRight { get; set; } = string.Empty;

        // Margins (in inches)
        public double HeaderMargin { get; set; } = 0.0;
        public double FooterMargin { get; set; } = 0.0;

        // Font settings for header/footer
        public string FontFaceName { get; set; } = "Arial";
        public int FontHeight { get; set; } = 12; // Height property (int)
        public int FontWeight { get; set; } = 400; // 400 = normal, 700 = bold
        public BOOL Italic { get; set; } = BOOL.False;
        public BOOL Underline { get; set; } = BOOL.False;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input diagram path and output diagram path
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Usage: DiagramHeaderUtility <inputDiagramPath> <outputDiagramPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Guard against missing input file
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Example header/footer settings – in real usage these could be built from user input or config
            var settings = new HeaderFooterSettings
            {
                HeaderLeft = "Company Confidential",
                HeaderCenter = "Report Title",
                HeaderRight = "Date: &d",
                FooterLeft = "Prepared by: John Doe",
                FooterCenter = "",
                FooterRight = "Page &p of &P",
                HeaderMargin = 0.25,
                FooterMargin = 0.25,
                FontFaceName = "Calibri",
                FontHeight = 10,
                FontWeight = 700, // Bold
                Italic = BOOL.False,
                Underline = BOOL.False
            };

            // Apply settings and save the diagram
            ApplyHeaderFooterAndSave(inputPath, outputPath, settings);
        }

        /// <summary>
        /// Loads a Visio diagram, applies header/footer settings, and saves it to the specified output path.
        /// </summary>
        /// <param name="inputPath">Path to the source diagram file.</param>
        /// <param name="outputPath">Path where the modified diagram will be saved.</param>
        /// <param name="settings">Header and footer configuration.</param>
        static void ApplyHeaderFooterAndSave(string inputPath, string outputPath, HeaderFooterSettings settings)
        {
            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Apply header text
                    diagram.HeaderFooter.HeaderLeft = settings.HeaderLeft;
                    diagram.HeaderFooter.HeaderCenter = settings.HeaderCenter;
                    diagram.HeaderFooter.HeaderRight = settings.HeaderRight;

                    // Apply footer text
                    diagram.HeaderFooter.FooterLeft = settings.FooterLeft;
                    diagram.HeaderFooter.FooterCenter = settings.FooterCenter;
                    diagram.HeaderFooter.FooterRight = settings.FooterRight;

                    // Apply margins (Margin.Value holds the double value)
                    diagram.HeaderFooter.HeaderMargin.Value = settings.HeaderMargin;
                    diagram.HeaderFooter.FooterMargin.Value = settings.FooterMargin;

                    // Configure the shared header/footer font
                    HeaderFooterFont font = diagram.HeaderFooter.HeaderFooterFont;
                    font.FaceName = settings.FontFaceName;
                    font.Height = settings.FontHeight;
                    font.Weight = settings.FontWeight;
                    font.Italic = settings.Italic;
                    font.Underline = settings.Underline;

                    // Determine save format based on output file extension (default to Vsdx)
                    SaveFileFormat format = SaveFileFormat.Vsdx;
                    string ext = Path.GetExtension(outputPath).ToLowerInvariant();
                    if (ext == ".pdf")
                        format = SaveFileFormat.Pdf;
                    else if (ext == ".png")
                        format = SaveFileFormat.Png;
                    else if (ext == ".html")
                        format = SaveFileFormat.Html;
                    else if (ext == ".svg")
                        format = SaveFileFormat.Svg;
                    // Add more mappings as needed

                    // Save the modified diagram
                    diagram.Save(outputPath, format);
                }

                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}