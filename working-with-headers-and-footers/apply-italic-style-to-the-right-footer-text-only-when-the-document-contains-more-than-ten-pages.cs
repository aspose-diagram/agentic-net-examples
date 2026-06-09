using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram from file.
                // Replace "input.vsdx" with the actual path to your diagram.
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Check if the document has more than ten pages.
                    if (diagram.Pages.Count > 10)
                    {
                        // Set the right footer text.
                        diagram.HeaderFooter.FooterRight = "Confidential";

                        // Apply italic style to the footer font.
                        // This font setting affects all header/footer text,
                        // but we set it only when the condition is met.
                        diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;
                    }

                    // Optionally save the modified diagram.
                    // Replace "output.vsdx" with the desired output path.
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }