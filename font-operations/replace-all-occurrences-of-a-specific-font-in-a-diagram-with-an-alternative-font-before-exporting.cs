using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: FontReplacementExample <inputVisio> <outputFile> <oldFontName> <newFontName>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string oldFont = args[2];
            string newFont = args[3];

            // Verify that the replacement font is installed on the system
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool newFontInstalled = false;
            foreach (var family in fontCollection.Families)
            {
                if (string.Equals(family.Name, newFont, StringComparison.OrdinalIgnoreCase))
                {
                    newFontInstalled = true;
                    break;
                }
            }

            if (!newFontInstalled)
            {
                Console.WriteLine($"Warning: The replacement font \"{newFont}\" is not installed on this machine.");
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages, shapes, and character runs to replace the font
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (string.Equals(ch.FontName.Value, oldFont, StringComparison.OrdinalIgnoreCase))
                            {
                                ch.FontName.Value = newFont;
                            }
                        }
                    }
                }

                // Save the modified diagram as PDF, specifying the default font for fallback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = newFont;
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram saved to \"{outputPath}\" with font \"{oldFont}\" replaced by \"{newFont}\".");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }