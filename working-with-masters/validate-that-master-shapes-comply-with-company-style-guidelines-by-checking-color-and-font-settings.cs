using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Drawing.Text;

class Program
    {
        // Expected style guidelines
        private const string ExpectedFillColor = "#FF0000"; // Red fill foreground
        private const string ExpectedFontName = "Calibri";

        static void Main()
        {
            try
            {

                // Path to the Visio file to validate
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Verify that the expected font is installed on the system
                ValidateFontInstalled(ExpectedFontName);

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Iterate through each shape within the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Validate fill foreground color
                        string fillColor = shape.Fill.FillForegnd.Value;
                        if (!string.Equals(fillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Master '{master.Name}' Shape ID {shape.ID} has invalid fill color '{fillColor}'. Expected: '{ExpectedFillColor}'.");
                            // Optionally throw to stop processing
                            // throw new Exception("Fill color validation failed.");
                        }

                        // Validate font name for each character run
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            string fontName = ch.FontName.Value;
                            if (!string.Equals(fontName, ExpectedFontName, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"Master '{master.Name}' Shape ID {shape.ID} has invalid font '{fontName}'. Expected: '{ExpectedFontName}'.");
                                // Optionally throw to stop processing
                                // throw new Exception("Font validation failed.");
                            }
                        }
                    }
                }

                Console.WriteLine("Master shape validation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Ensures the required font is available on the system using Aspose.Drawing.Text
        private static void ValidateFontInstalled(string fontName)
        {
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool found = fontCollection.Families.Any(f => string.Equals(f.Name, fontName, StringComparison.OrdinalIgnoreCase));

            if (!found)
            {
                Console.WriteLine($"Required font '{fontName}' is not installed on the system.");
                // Optionally throw to halt execution
                // throw new Exception($"Missing required font: {fontName}");
            }
        }
    }