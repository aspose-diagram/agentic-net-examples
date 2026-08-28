using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        try
                        {
                            // Attempt to read page width and height from PageProps
                            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                            Console.WriteLine($"Page ID {page.ID}: Width = {pageWidth} inches, Height = {pageHeight} inches");
                        }
                        catch (Exception ex)
                        {
                            // Handle cases where PageProps or its properties are inaccessible
                            Console.WriteLine($"Error accessing PageProps for page ID {page.ID}: {ex.Message}");
                        }
                    }

                    // Example modification: set a default size if properties were inaccessible
                    // (Here we simply set a standard size for all pages)
                    foreach (Page page in diagram.Pages)
                    {
                        try
                        {
                            page.PageSheet.PageProps.PageWidth.Value = 8.5;   // Width in inches
                            page.PageSheet.PageProps.PageHeight.Value = 11;   // Height in inches
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error setting PageProps for page ID {page.ID}: {ex.Message}");
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved successfully.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }