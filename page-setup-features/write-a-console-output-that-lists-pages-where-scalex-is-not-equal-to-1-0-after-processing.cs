using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string filePath = "input.vsdx";

                // Load the diagram from the specified file
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the ScaleX value from the page's print properties
                    double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                    // List pages where ScaleX is not equal to 1.0
                    if (scaleX != 1.0)
                    {
                        Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, ScaleX: {scaleX}");
                    }
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