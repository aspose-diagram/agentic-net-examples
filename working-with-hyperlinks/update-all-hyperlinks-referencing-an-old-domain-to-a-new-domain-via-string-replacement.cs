using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Domain strings for replacement
                string oldDomain = "oldexample.com";
                string newDomain = "newexample.com";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection
                        if (shape.Hyperlinks != null)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Verify the Address cell exists and contains the old domain
                                if (link.Address != null && link.Address.Value != null &&
                                    link.Address.Value.Contains(oldDomain))
                                {
                                    // Replace the old domain with the new domain
                                    link.Address.Value = link.Address.Value.Replace(oldDomain, newDomain);
                                }
                            }
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }