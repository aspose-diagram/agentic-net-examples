using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to audit
            string visioPath = "input.vsdx";
            // Path where the audit CSV will be saved
            string csvPath = "page_audit.csv";

            // Load the diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Prepare CSV writer
                using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("PageIndex,Orientation,ScaleX,ScaleY");

                    int pageIndex = 0;
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve orientation
                        string orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value.ToString();

                        // Retrieve scaling factors
                        double scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                        double scaleY = page.PageSheet.PrintProps.ScaleY.Value;

                        // Write CSV line
                        writer.WriteLine($"{pageIndex},{orientation},{scaleX},{scaleY}");

                        pageIndex++;
                    }
                }

                // Optionally, save the diagram (not required for the CSV audit)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Page audit CSV has been generated at: " + Path.GetFullPath(csvPath));

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
