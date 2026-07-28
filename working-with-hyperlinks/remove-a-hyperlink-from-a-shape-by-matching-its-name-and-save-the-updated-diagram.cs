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

            // Define the shape name and hyperlink name to be removed
            string targetShapeName = "MyShape";
            string targetHyperlinkName = "MyHyperlink";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape name matches the target shape
                    if (shape.Name == targetShapeName)
                    {
                        // Find the hyperlink with the specified name
                        Hyperlink hyperlinkToRemove = null;
                        foreach (Hyperlink hl in shape.Hyperlinks)
                        {
                            if (hl.Name == targetHyperlinkName)
                            {
                                hyperlinkToRemove = hl;
                                break;
                            }
                        }

                        // If the hyperlink was found, remove it from the collection
                        if (hyperlinkToRemove != null)
                        {
                            shape.Hyperlinks.Remove(hyperlinkToRemove);
                            // Optionally, you can set the Del flag instead of removing:
                            // hyperlinkToRemove.Del = 1;
                        }
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
