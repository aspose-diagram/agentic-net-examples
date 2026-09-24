using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to set text block background transparency
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Set the text block background transparency to 50%
                    shape.TextBlock.TextBkgndTrans.Value = 50;
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
