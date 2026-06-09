using System.IO;
using System;
using Aspose.Diagram;

class ReplaceHyperlinkAddress
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define the new URL you want to set
            string newUrl = "https://www.newaddress.com";

            // Optional: define a description to match a specific hyperlink
            // If you want to replace all hyperlinks, remove the condition check
            string targetDescription = "Original hyperlink description";

            // Iterate through all pages, shapes, and their hyperlinks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Hyperlinks collection may be null; check before iterating
                    if (shape.Hyperlinks != null)
                    {
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Preserve the description; only replace the address if it matches the target description
                            if (link.Description != null && link.Description.Value == targetDescription)
                            {
                                // Set the new address (URL) while keeping the description unchanged
                                if (link.Address != null)
                                {
                                    link.Address.Value = newUrl;
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
