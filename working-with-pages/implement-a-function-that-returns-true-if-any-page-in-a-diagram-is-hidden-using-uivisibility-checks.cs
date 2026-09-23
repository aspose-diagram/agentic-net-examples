using System;
using Aspose.Diagram;

namespace DiagramUtilities
{
    public class PageVisibilityChecker
    {
        /// <summary>
        /// Returns true if any page in the provided diagram is hidden (UIVisibility set to Hidden).
        /// </summary>
        /// <param name="diagram">The Aspose.Diagram.Diagram instance to inspect.</param>
        /// <returns>True when at least one page is hidden; otherwise false.</returns>
        public static bool AnyPageHidden(Diagram diagram)
        {
            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // UIVisibility.Value indicates the visibility state of the page.
                // UIVisibilityValue.Visible means the page is shown.
                // Any other value (e.g., UIVisibilityValue.Hidden) means the page is hidden.
                if (page.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Visible)
                {
                    return true;
                }
            }

            // No hidden pages were found.
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Example usage: load a diagram file and check for hidden pages.
                // Replace "sample.vsdx" with the path to your Visio file.
                string diagramPath = "sample.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Check if any page is hidden.
                bool hasHiddenPage = PageVisibilityChecker.AnyPageHidden(diagram);

                // Output the result.
                Console.WriteLine(hasHiddenPage
                    ? "The diagram contains at least one hidden page."
                    : "All pages in the diagram are visible.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}