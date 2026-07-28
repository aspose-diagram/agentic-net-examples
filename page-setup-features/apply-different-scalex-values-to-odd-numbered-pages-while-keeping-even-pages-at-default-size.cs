using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through all pages and set ScaleX for odd-numbered pages
                    int pageIndex = 0; // zero‑based index
                    foreach (Page page in diagram.Pages)
                    {
                        // Visio page numbers are 1‑based; odd pages have (index + 1) % 2 == 1
                        if ((pageIndex + 1) % 2 == 1)
                        {
                            // Apply a custom horizontal scale (e.g., 50% of original size)
                            page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                        }
                        else
                        {
                            // Keep even pages at default scale (100%)
                            page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                        }

                        pageIndex++;
                    }

                    // Save the modified diagram to a new file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }