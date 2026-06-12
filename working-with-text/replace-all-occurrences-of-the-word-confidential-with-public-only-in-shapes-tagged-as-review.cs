using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is tagged as "Review"
                    // Assuming the tag is stored in the Data1 property
                    if (!string.IsNullOrEmpty(shape.Data1) && shape.Data1.Equals("Review", StringComparison.OrdinalIgnoreCase))
                    {
                        // Replace the word "Confidential" with "Public" in the shape's text
                        // Using ReplaceText method which updates the shape's text content
                        shape.ReplaceText("Confidential", "Public");

                        // Refresh shape data to recalculate geometry after text change
                        shape.RefreshData();
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
