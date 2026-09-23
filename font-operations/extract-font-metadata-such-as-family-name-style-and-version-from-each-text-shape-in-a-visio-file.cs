using System;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare system font collection for version lookup
                InstalledFontCollection systemFonts = new InstalledFontCollection();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains any text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                            // Iterate through each character formatting run
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font family name used by this character run
                                string fontName = ch.FontName.Value;

                                // Determine style (Bold, Italic, Underline) from StyleValue flags
                                StyleValue styleFlags = ch.Style.Value;
                                string styleDesc = "";
                                if ((styleFlags & StyleValue.Bold) != 0) styleDesc += "Bold ";
                                if ((styleFlags & StyleValue.Italic) != 0) styleDesc += "Italic ";
                                if ((styleFlags & StyleValue.Underline) != 0) styleDesc += "Underline ";
                                if (string.IsNullOrWhiteSpace(styleDesc)) styleDesc = "Regular";

                                // Attempt to retrieve version information from the installed system fonts
                                string fontVersion = "N/A";
                                foreach (var sysFont in systemFonts.Families)
                                {
                                    // The exact property names may vary; use common ones
                                    // Assume sysFont.Name provides the family name and sysFont.Version provides version info
                                    if (sysFont.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase))
                                    {
                                        try
                                        {
                                            var versionProp = sysFont.GetType().GetProperty("Version");
                                            if (versionProp != null)
                                            {
                                                object verObj = versionProp.GetValue(sysFont);
                                                fontVersion = verObj?.ToString() ?? "N/A";
                                            }
                                        }
                                        catch
                                        {
                                            // If reflection fails, keep version as N/A
                                        }
                                        break;
                                    }
                                }

                                Console.WriteLine($"  Font: {fontName}");
                                Console.WriteLine($"  Style: {styleDesc.Trim()}");
                                Console.WriteLine($"  Version: {fontVersion}");
                            }

                            Console.WriteLine(); // Blank line between shapes
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }