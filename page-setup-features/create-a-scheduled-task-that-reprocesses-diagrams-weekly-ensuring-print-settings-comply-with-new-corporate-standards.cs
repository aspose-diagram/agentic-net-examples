using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing diagrams; can be passed as a command‑line argument
        string folderPath = args.Length > 0 ? args[0] : "Diagrams";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process all Visio files (VSDX, VDX, VSD) recursively
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vdx" && ext != ".vsd")
                continue;

            try
            {
                // Load diagram
                using (Diagram diagram = new Diagram(file))
                {
                    // Update print settings for each page to meet corporate standards
                    foreach (Page page in diagram.Pages)
                    {
                        if (page.PageSheet?.PrintProps == null)
                        {
                            Console.WriteLine($"Page '{page.Name}' missing PrintProps; skipping.");
                            continue;
                        }

                        // Orientation: Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Scaling: 100%
                        page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                        page.PageSheet.PrintProps.ScaleY.Value = 1.0;

                        // Fit to a single sheet
                        page.PageSheet.PrintProps.OnPage.Value = BOOL.True;
                        page.PageSheet.PrintProps.PagesX.Value = 1;
                        page.PageSheet.PrintProps.PagesY.Value = 1;

                        // Margins: 0.5 inches on all sides
                        double marginInches = 0.5;
                        page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;
                        page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                        page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;
                    }

                    // Overwrite the original file with updated settings
                    diagram.Save(file, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {file}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{file}': {ex.Message}");
            }
        }

        Console.WriteLine("Weekly diagram reprocessing completed.");
    }
}
