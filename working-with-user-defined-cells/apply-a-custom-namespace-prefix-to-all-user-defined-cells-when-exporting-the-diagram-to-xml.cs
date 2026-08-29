using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (replace with actual path)
            string inputPath = "input.vsdx";
            // Output Visio XML file (VDX format)
            string outputPath = "output.vdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all user‑defined cells of the shape
                        foreach (User userCell in shape.Users)
                        {
                            // Apply custom namespace prefix if not already present
                            const string prefix = "MyNS.";
                            if (!userCell.Name.StartsWith(prefix))
                            {
                                userCell.Name = prefix + userCell.Name;
                            }
                            if (!userCell.NameU.StartsWith(prefix))
                            {
                                userCell.NameU = prefix + userCell.NameU;
                            }
                        }
                    }
                }

                // Save the modified diagram as XML (VDX)
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }

            Console.WriteLine("Diagram exported to XML with prefixed user‑defined cells.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
