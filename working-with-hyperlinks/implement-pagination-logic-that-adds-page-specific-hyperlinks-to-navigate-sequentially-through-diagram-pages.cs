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

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Determine target pages for navigation
                Page nextPage = (i < diagram.Pages.Count - 1) ? diagram.Pages[i + 1] : null;
                Page prevPage = (i > 0) ? diagram.Pages[i - 1] : null;

                // Add a "Next" navigation shape if there is a subsequent page
                if (nextPage != null)
                {
                    // Add a rectangle shape (master name "Rectangle") at a fixed position
                    long nextShapeId = page.AddShape(1.0, 1.0, "Rectangle");
                    Shape nextShape = page.Shapes.GetShape(nextShapeId);

                    // Set the shape's text
                    nextShape.Text.Value.Clear();
                    nextShape.Text.Value.Add(new Txt("Next Page"));

                    // Create and assign a hyperlink that points to the next page
                    Hyperlink nextLink = new Hyperlink();
                    nextLink.Name = "NextLink";
                    // SubAddress refers to the target page name
                    nextLink.SubAddress.Value = nextPage.Name;
                    nextShape.Hyperlinks.Add(nextLink);
                }

                // Add a "Previous" navigation shape if there is a preceding page
                if (prevPage != null)
                {
                    // Add a rectangle shape at a different position
                    long prevShapeId = page.AddShape(5.0, 1.0, "Rectangle");
                    Shape prevShape = page.Shapes.GetShape(prevShapeId);

                    // Set the shape's text
                    prevShape.Text.Value.Clear();
                    prevShape.Text.Value.Add(new Txt("Previous Page"));

                    // Create and assign a hyperlink that points to the previous page
                    Hyperlink prevLink = new Hyperlink();
                    prevLink.Name = "PrevLink";
                    prevLink.SubAddress.Value = prevPage.Name;
                    prevShape.Hyperlinks.Add(prevLink);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
