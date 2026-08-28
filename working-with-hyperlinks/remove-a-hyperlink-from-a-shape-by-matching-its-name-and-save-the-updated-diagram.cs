using System.IO;
using System;
using Aspose.Diagram;

class RemoveHyperlinkExample
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the shape that contains the hyperlink to be removed
            string targetShapeName = "MyShape";

            // Name of the hyperlink to remove
            string targetHyperlinkName = "MyHyperlink";

            // Iterate through all pages (adjust if you know the specific page)
            foreach (Page page in diagram.Pages)
            {
                // Search for the shape with the specified name
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Name == targetShapeName)
                    {
                        // Find the hyperlink with the matching name
                        Hyperlink hyperlinkToRemove = null;
                        foreach (Hyperlink hl in shape.Hyperlinks)
                        {
                            if (hl.Name == targetHyperlinkName)
                            {
                                hyperlinkToRemove = hl;
                                break;
                            }
                        }

                        // Remove the hyperlink if it was found
                        if (hyperlinkToRemove != null)
                        {
                            shape.Hyperlinks.Remove(hyperlinkToRemove);
                        }

                        // Shape found, no need to continue searching
                        break;
                    }
                }
            }

            // Save the updated diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
