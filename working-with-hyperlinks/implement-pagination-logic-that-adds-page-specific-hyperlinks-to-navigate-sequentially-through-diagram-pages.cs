using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Define the number of pages to create
            int totalPages = 5;

            // Create additional pages (the diagram already contains one page)
            for (int i = 1; i < totalPages; i++)
            {
                // Add a new blank page with a unique name
                Page newPage = new Page();
                newPage.Name = $"Page-{i + 1}";
                diagram.Pages.Add(newPage);
            }

            // Iterate through each page and add navigation shapes with hyperlinks
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                page.Name = $"Page-{i + 1}";

                // Position and size for the navigation rectangle (in inches)
                double pinX = 2.0;   // center X
                double pinY = 2.0;   // center Y
                double width = 1.5;  // width
                double height = 0.5; // height

                // Draw a rectangle shape on the current page
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);
                Shape navShape = page.Shapes.GetShape((int)shapeId);

                // Set the shape's text to indicate navigation direction
                navShape.Text.Value.Clear();
                navShape.Text.Value.Add(new Txt(i == diagram.Pages.Count - 1 ? "First Page" : "Next Page"));

                // Create a hyperlink that points to the target page
                Hyperlink link = new Hyperlink();
                link.Name = "PageNavLink";
                link.Address.Value = ""; // Empty address for internal navigation
                // Determine target page name (next page or first page for the last page)
                string targetPageName = i == diagram.Pages.Count - 1
                    ? diagram.Pages[0].Name   // loop back to first page
                    : diagram.Pages[i + 1].Name;
                link.SubAddress.Value = targetPageName;
                link.Description.Value = $"Navigate to {targetPageName}";

                // Add the hyperlink to the shape
                navShape.Hyperlinks.Add(link);
            }

            // Save the diagram to a VSDX file
            string outputPath = "PaginatedDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }