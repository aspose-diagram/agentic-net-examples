using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define the new URL you want to set for the hyperlink
            string newUrl = "https://www.example.com/newpage";

            // Iterate through all pages, shapes, and their hyperlinks
            foreach (var page in diagram.Pages)
            {
                foreach (var shape in page.Shapes)
                {
                    // Hyperlinks collection may be null; check before iterating
                    if (shape.Hyperlinks != null)
                    {
                        foreach (var hyperlink in shape.Hyperlinks)
                        {
                            // Preserve the existing description (no change needed)
                            // Replace only the address (URL) of the hyperlink
                            if (hyperlink.Address != null)
                            {
                                // Str2Value holds the actual string in its Value property
                                hyperlink.Address.Value = newUrl;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
