using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        // Predefined header/footer template
        private const string HeaderLeftText = "Company Name";
        private const string HeaderCenterText = "Confidential";
        private const string HeaderRightText = "Document Title";

        private const string FooterLeftText = "Document ID: 12345";
        private const string FooterCenterText = "Date: &d"; // &d inserts current date
        private const string FooterRightText = "Page: &p"; // &p inserts page number

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: VisioHeaderFooterTool <directoryPath>");
                return;
            }

            string directoryPath = args[0];

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Error: Directory does not exist - {directoryPath}");
                return;
            }

            // Supported Visio file extensions
            string[] supportedExtensions = new[]
            {
                ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst",
                ".vtx", ".vsdm", ".vssm", ".vstm", ".svg", ".pdf", ".png",
                ".jpg", ".jpeg", ".html", ".tiff", ".tif", ".gif", ".swf", ".xaml"
            };

            string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (Array.IndexOf(supportedExtensions, extension) < 0)
                {
                    // Skip non‑Visio files
                    continue;
                }

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(filePath);

                    // Apply header/footer template
                    diagram.HeaderFooter.HeaderLeft = HeaderLeftText;
                    diagram.HeaderFooter.HeaderCenter = HeaderCenterText;
                    diagram.HeaderFooter.HeaderRight = HeaderRightText;

                    diagram.HeaderFooter.FooterLeft = FooterLeftText;
                    diagram.HeaderFooter.FooterCenter = FooterCenterText;
                    diagram.HeaderFooter.FooterRight = FooterRightText;

                    // Optional: set margins (in inches)
                    diagram.HeaderFooter.HeaderMargin.Value = 0.5;
                    diagram.HeaderFooter.FooterMargin.Value = 0.5;

                    // Determine the appropriate SaveFileFormat based on the file extension
                    SaveFileFormat format = GetSaveFileFormat(extension);

                    // Save the diagram back to the original file
                    diagram.Save(filePath, format);

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to process {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }
        }

        // Maps file extensions to the corresponding SaveFileFormat enum values
        private static SaveFileFormat GetSaveFileFormat(string extension)
        {
            return extension switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd" => SaveFileFormat.Vsd,
                ".vdx" => SaveFileFormat.Vdx,
                ".vssx" => SaveFileFormat.Vssx,
                ".vss" => SaveFileFormat.Vss,
                ".vstx" => SaveFileFormat.Vstx,
                ".vst" => SaveFileFormat.Vst,
                ".vtx" => SaveFileFormat.Vtx,
                ".vsdm" => SaveFileFormat.Vsdm,
                ".vssm" => SaveFileFormat.Vssm,
                ".vstm" => SaveFileFormat.Vstm,
                ".svg" => SaveFileFormat.Svg,
                ".pdf" => SaveFileFormat.Pdf,
                ".png" => SaveFileFormat.Png,
                ".jpg" => SaveFileFormat.Jpeg,
                ".jpeg" => SaveFileFormat.Jpeg,
                ".html" => SaveFileFormat.Html,
                ".tiff" => SaveFileFormat.Tiff,
                ".tif" => SaveFileFormat.Tiff,
                ".gif" => SaveFileFormat.Gif,
                ".swf" => SaveFileFormat.Swf,
                ".xaml" => SaveFileFormat.Xaml,
                _ => SaveFileFormat.Vsdx // Default fallback
            };
        }
    }