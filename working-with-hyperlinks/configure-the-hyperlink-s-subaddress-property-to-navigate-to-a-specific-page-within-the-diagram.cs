using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page in the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (replace with your own logic if needed)
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    targetShape = shp;
                    break;
                }

                if (targetShape == null)
                {
                    throw new Exception("No shape found on the first page.");
                }

                // Create a new hyperlink and set its subaddress to navigate to a specific page
                Hyperlink hyperlink = new Hyperlink();
                // The subaddress should match the target page name (e.g., "Page-2")
                hyperlink.SubAddress.Value = "Page-2";
                // Optional: provide a description that appears as a tooltip
                hyperlink.Description.Value = "Go to Page-2";

                // Add the hyperlink to the shape's Hyperlinks collection
                targetShape.Hyperlinks.Add(hyperlink);

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }