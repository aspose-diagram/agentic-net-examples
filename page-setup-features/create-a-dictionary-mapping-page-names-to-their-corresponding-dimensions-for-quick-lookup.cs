using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string diagramPath = "example.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Dictionary to map page names to their dimensions (width, height) in inches
                var pageDimensions = new Dictionary<string, (double Width, double Height)>();

                // Iterate through each page and store its dimensions
                foreach (Page page in diagram.Pages)
                {
                    string name = page.Name;
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    pageDimensions[name] = (width, height);
                }

                // Output the collected dimensions for verification
                foreach (KeyValuePair<string, (double Width, double Height)> kvp in pageDimensions)
                {
                    Console.WriteLine($"Page \"{kvp.Key}\": Width = {kvp.Value.Width} in, Height = {kvp.Value.Height} in");
                }

                // Clean up resources
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }