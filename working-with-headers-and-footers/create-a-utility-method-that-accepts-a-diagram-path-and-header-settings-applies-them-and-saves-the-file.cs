using System;
using Aspose.Diagram;

namespace DiagramHeaderUtility
{
    // Represents header and footer configuration.
    public class HeaderSettings
    {
        public string HeaderLeft { get; set; } = string.Empty;
        public string HeaderCenter { get; set; } = string.Empty;
        public string HeaderRight { get; set; } = string.Empty;

        public string FooterLeft { get; set; } = string.Empty;
        public string FooterCenter { get; set; } = string.Empty;
        public string FooterRight { get; set; } = string.Empty;

        // Margins are in inches.
        public double HeaderMargin { get; set; } = 0.0;
        public double FooterMargin { get; set; } = 0.0;

        // Font settings for header/footer text.
        public string FontFaceName { get; set; } = "Arial";
        public int FontWeight { get; set; } = 400; // 400 = normal, 700 = bold
        public int FontHeight { get; set; } = 12;   // Height property is an int (point size * -1.333 conversion handled internally)
    }

    public static class DiagramHeaderHelper
    {
        /// <summary>
        /// Loads a Visio diagram, applies header/footer settings, and saves the diagram.
        /// </summary>
        /// <param name="inputPath">Path to the source diagram file.</param>
        /// <param name="settings">Header and footer configuration.</param>
        /// <param name="outputPath">Path where the modified diagram will be saved.</param>
        public static void ApplyHeaderAndSave(string inputPath, HeaderSettings settings, string outputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                throw new ArgumentException("Input path must be a valid file path.", nameof(inputPath));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("Output path must be a valid file path.", nameof(outputPath));

            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Apply header text.
            diagram.HeaderFooter.HeaderLeft = settings.HeaderLeft;
            diagram.HeaderFooter.HeaderCenter = settings.HeaderCenter;
            diagram.HeaderFooter.HeaderRight = settings.HeaderRight;

            // Apply footer text.
            diagram.HeaderFooter.FooterLeft = settings.FooterLeft;
            diagram.HeaderFooter.FooterCenter = settings.FooterCenter;
            diagram.HeaderFooter.FooterRight = settings.FooterRight;

            // Apply margins (use .Value to set the double value).
            diagram.HeaderFooter.HeaderMargin.Value = settings.HeaderMargin;
            diagram.HeaderFooter.FooterMargin.Value = settings.FooterMargin;

            // Configure typography for header/footer.
            var font = diagram.HeaderFooter.HeaderFooterFont;
            font.FaceName = settings.FontFaceName;
            font.Weight = settings.FontWeight;
            font.Height = settings.FontHeight;

            // Save the modified diagram. Using Vsdx as a common editable format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }

    // Example usage (optional entry point).
    class Program
    {
        static void Main()
        {
            try
            {

                var settings = new HeaderSettings
                {
                    HeaderLeft = "Company Confidential",
                    HeaderCenter = "Project XYZ",
                    HeaderRight = "Date: &d",
                    FooterLeft = "Prepared by: John Doe",
                    FooterCenter = "",
                    FooterRight = "Page: &p",
                    HeaderMargin = 0.25,
                    FooterMargin = 0.25,
                    FontFaceName = "Calibri",
                    FontWeight = 700, // Bold
                    FontHeight = -16   // Corresponds to 12pt (12 * -1.333 ≈ -16)
                };

                string inputFile = @"C:\Diagrams\sample.vsdx";
                string outputFile = @"C:\Diagrams\sample_modified.vsdx";

                DiagramHeaderHelper.ApplyHeaderAndSave(inputFile, settings, outputFile);

                Console.WriteLine("Header/footer applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}