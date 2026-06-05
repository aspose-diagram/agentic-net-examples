using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}");

                            // Iterate through each character formatting run in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font name used for this character run
                                string fontName = ch.FontName.Value;

                                // Style bitmask (e.g., Bold, Italic, Underline)
                                StyleValue styleMask = ch.Style.Value;

                                // Attempt to retrieve version information from the diagram's font collection
                                string fontVersion = "N/A";
                                foreach (Font font in diagram.Fonts)
                                {
                                    if (font.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase))
                                    {
                                        // Some Font objects expose a Version property; use it if available
                                        // If the property does not exist, the fallback "N/A" will remain
                                        try
                                        {
                                            // Using reflection to safely access the Version property if it exists
                                            var versionProp = typeof(Font).GetProperty("Version");
                                            if (versionProp != null)
                                            {
                                                object versionValue = versionProp.GetValue(font);
                                                if (versionValue != null)
                                                {
                                                    fontVersion = versionValue.ToString();
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            // Ignore any reflection errors and keep "N/A"
                                        }
                                        break;
                                    }
                                }

                                Console.WriteLine($"  Font Name: {fontName}");
                                Console.WriteLine($"  Style Mask: {styleMask}");
                                Console.WriteLine($"  Font Version: {fontVersion}");
                            }

                            Console.WriteLine(); // Blank line for readability
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