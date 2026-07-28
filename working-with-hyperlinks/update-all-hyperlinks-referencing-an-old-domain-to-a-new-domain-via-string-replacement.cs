using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input file, output file, old domain and new domain.
                // Adjust these paths/values as needed.
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";
                string oldDomain = "oldexample.com";
                string newDomain = "newexample.com";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate through each hyperlink.
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Guard against null Address.
                                if (link.Address != null && link.Address.Value != null)
                                {
                                    // Replace old domain with new domain if present.
                                    if (link.Address.Value.Contains(oldDomain))
                                    {
                                        link.Address.Value = link.Address.Value.Replace(oldDomain, newDomain);
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }