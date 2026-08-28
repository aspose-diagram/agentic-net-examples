using System;
using Aspose.Diagram;

public class HyperlinkRemover
    {
        /// <summary>
        /// Removes all hyperlinks from every shape on the specified page,
        /// as well as any page‑level hyperlinks.
        /// </summary>
        /// <param name="diagram">The loaded Aspose.Diagram Diagram instance.</param>
        /// <param name="pageName">The name of the page from which hyperlinks should be removed.</param>
        public static void RemoveAllShapeHyperlinks(Diagram diagram, string pageName)
        {
            // Locate the target page by its name.
            Page targetPage = null;
            foreach (Page page in diagram.Pages)
            {
                if (string.Equals(page.Name, pageName, StringComparison.OrdinalIgnoreCase))
                {
                    targetPage = page;
                    break;
                }
            }

            if (targetPage == null)
                throw new ArgumentException($"Page \"{pageName}\" not found in the diagram.");

            // Remove page‑level hyperlinks, if any.
            if (targetPage.PageSheet != null && targetPage.PageSheet.Hyperlinks != null)
            {
                targetPage.PageSheet.Hyperlinks.Clear();
            }

            // Iterate through all shapes on the page and clear their hyperlink collections.
            foreach (Shape shape in targetPage.Shapes)
            {
                if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                {
                    shape.Hyperlinks.Clear();
                }
            }
        }

        // Example usage:
        public static void Main()
        {
            try
            {

                // Load the diagram (replace with your actual file path).
                Diagram diagram = new Diagram("input.vsdx");

                // Remove hyperlinks from the page named "Page-1".
                RemoveAllShapeHyperlinks(diagram, "Page-1");

                // Save the modified diagram (replace with your desired output path).
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }