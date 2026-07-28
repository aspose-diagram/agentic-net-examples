using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // New hyperlink target URL
            string newUrl = "https://www.example.com";

            // Traverse all pages and shapes to locate hyperlinks
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may contain one or more hyperlinks
                    foreach (Hyperlink hyperlink in shape.Hyperlinks)
                    {
                        // Update the address while preserving the description
                        hyperlink.Address.Value = newUrl;
                    }
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
