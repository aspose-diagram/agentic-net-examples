using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file
                string inputPath = "input.vsdx";
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Define shape names and the target page names they should link to
                    // Example: Shape named "StartShape" will link to page "StartPage"
                    var shapeToPageMap = new System.Collections.Generic.Dictionary<string, string>
                    {
                        { "StartShape", "StartPage" },
                        { "ProcessShape", "ProcessPage" },
                        { "EndShape", "EndPage" }
                    };

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape's universal name matches any entry in the map
                            if (shapeToPageMap.TryGetValue(shape.NameU, out string targetPageName))
                            {
                                // Ensure the Hyperlinks collection is not null
                                if (shape.Hyperlinks != null)
                                {
                                    // Create a new hyperlink instance
                                    Hyperlink link = new Hyperlink();
                                    // Optional: give the hyperlink a name (identifier)
                                    link.Name = "LinkTo" + targetPageName;
                                    // For internal page navigation, set SubAddress to the target page name
                                    link.SubAddress.Value = targetPageName;
                                    // Add the hyperlink to the shape's collection
                                    shape.Hyperlinks.Add(link);
                                }
                            }
                        }
                    }

                    // Save the modified diagram to a new file
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Hyperlinks have been assigned and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }