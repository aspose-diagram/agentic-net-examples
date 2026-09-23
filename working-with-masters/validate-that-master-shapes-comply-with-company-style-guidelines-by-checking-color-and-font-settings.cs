using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram to validate
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Company style guidelines
                const string AllowedFillColor = "#FF0000"; // Example: red fill
                const string AllowedLineColor = "#0000FF"; // Example: blue line
                const string AllowedFontName = "Calibri";

                // Verify that the required font is installed on the system
                var fontCollection = new InstalledFontCollection();
                bool fontInstalled = false;
                foreach (var family in fontCollection.Families)
                {
                    // family.Name contains the font name
                    if (string.Equals(family.Name, AllowedFontName, StringComparison.OrdinalIgnoreCase))
                    {
                        fontInstalled = true;
                        break;
                    }
                }

                if (!fontInstalled)
                {
                    Console.WriteLine($"Warning: Required font '{AllowedFontName}' is not installed on this machine.");
                }

                bool hasErrors = false;

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Iterate through each shape defined in the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // Validate fill foreground color
                        string fillColor = shape.Fill.FillForegnd.Value;
                        if (!string.Equals(fillColor, AllowedFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Master '{master.Name}' Shape ID {shape.ID}: Fill color '{fillColor}' does not match allowed '{AllowedFillColor}'.");
                            hasErrors = true;
                        }

                        // Validate line color
                        string lineColor = shape.Line.LineColor.Value;
                        if (!string.Equals(lineColor, AllowedLineColor, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Master '{master.Name}' Shape ID {shape.ID}: Line color '{lineColor}' does not match allowed '{AllowedLineColor}'.");
                            hasErrors = true;
                        }

                        // Validate font name for each character run in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            string fontName = ch.FontName.Value;
                            if (!string.Equals(fontName, AllowedFontName, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Master '{master.Name}' Shape ID {shape.ID}: Font '{fontName}' does not match allowed '{AllowedFontName}'.");
                                hasErrors = true;
                            }
                        }
                    }
                }

                if (hasErrors)
                {
                    throw new Exception("Master shape style validation failed. Review the messages above.");
                }
                else
                {
                    Console.WriteLine("All master shapes comply with the company style guidelines.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }