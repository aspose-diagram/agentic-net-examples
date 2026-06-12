using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input Visio file and output XML (VDX) file
            string inputPath = "input.vsdx";
            string outputPath = "output.vdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define the custom namespace prefix to apply
            const string prefix = "MyNS.";

            // Iterate through all pages, shapes, and user-defined cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        // Apply the prefix if it hasn't been added already
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

            // Export the modified diagram to XML format (VDX)
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
