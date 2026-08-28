using System.IO;
using System;
using Aspose.Diagram;

class UpdateHyperlinks
{
    static void Main()
    {
        try
        {

            // Define old and new domain strings
            const string oldDomain = "oldexample.com";
            const string newDomain = "newexample.com";

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may contain a collection of hyperlinks
                    foreach (Hyperlink hyperlink in shape.Hyperlinks)
                    {
                        // The Address property returns a Str2Value object; its Value holds the URL string
                        string address = hyperlink.Address?.Value;

                        // If the address contains the old domain, replace it with the new domain
                        if (!string.IsNullOrEmpty(address) && address.Contains(oldDomain))
                        {
                            string updatedAddress = address.Replace(oldDomain, newDomain);
                            // Update the hyperlink's address
                            hyperlink.Address.Value = updatedAddress;
                        }
                    }
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
