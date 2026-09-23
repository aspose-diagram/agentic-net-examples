using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Name of the page from which hyperlinks will be removed
                string targetPageName = "Page-1";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Remove all hyperlinks from shapes on the specified page
                RemoveHyperlinksFromPage(diagram, targetPageName);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Removes every hyperlink from all shapes on a given page.
        /// </summary>
        /// <param name="diagram">The loaded Diagram instance.</param>
        /// <param name="pageName">The universal name of the page to process.</param>
        private static void RemoveHyperlinksFromPage(Diagram diagram, string pageName)
        {
            // Retrieve the page by its universal name
            Page page = diagram.Pages.GetPage(pageName);
            if (page == null)
            {
                throw new Exception($"Page '{pageName}' not found in the diagram.");
            }

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the Hyperlinks collection exists and contains items
                if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                {
                    // Clear all hyperlinks from the shape
                    shape.Hyperlinks.Clear();
                }
            }
        }
    }