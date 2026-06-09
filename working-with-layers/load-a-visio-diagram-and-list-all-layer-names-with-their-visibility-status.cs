using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through each layer on the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Retrieve layer name
                        string layerName = layer.Name.Value;

                        // Retrieve visibility status (BOOL enum)
                        BOOL visibility = layer.Visible.Value;

                        // Convert BOOL to readable string
                        string visibilityStatus = visibility == BOOL.True ? "Visible" : "Hidden";

                        Console.WriteLine($"  Layer: {layerName}, Visibility: {visibilityStatus}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }