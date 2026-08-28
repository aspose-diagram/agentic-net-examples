using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be processed
                string diagramPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Retrieve the ScaleX value from the page's PrintProps
                        double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                        // List pages where ScaleX is not equal to 1.0
                        if (scaleX != 1.0)
                        {
                            Console.WriteLine($"Page '{page.Name}' (ID: {page.ID}) has ScaleX = {scaleX}");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }