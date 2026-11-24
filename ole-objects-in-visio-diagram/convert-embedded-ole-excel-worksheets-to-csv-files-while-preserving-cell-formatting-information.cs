using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;
using Aspose.Cells.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <VisioFilePath> <OutputDirectory>");
            return;
        }

        string visioPath = args[0];
        string outputDir = args[1];

        // Guard against missing Visio file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            int pageNumber = 0;
            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                pageNumber++;

                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null &&
                        shape.ForeignData.Value != null &&
                        shape.ForeignData.Value.Length > 0)
                    {
                        // Identify Excel OLE objects by their source file name extension.
                        string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                        if (sourceName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                            sourceName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) ||
                            sourceName.EndsWith(".xlsm", StringComparison.OrdinalIgnoreCase))
                        {
                            // Extract the OLE binary data.
                            byte[] oleData = shape.ForeignData.Value;

                            using (MemoryStream oleStream = new MemoryStream(oleData))
                            {
                                // Load the Excel workbook from the OLE stream.
                                Workbook workbook = new Workbook(oleStream);

                                // Export each worksheet to a separate CSV file.
                                for (int wsIndex = 0; wsIndex < workbook.Worksheets.Count; wsIndex++)
                                {
                                    Worksheet sheet = workbook.Worksheets[wsIndex];

                                    // Build a safe file name for the CSV output.
                                    string safeSheetName = MakeSafeFileName(sheet.Name);
                                    string csvFileName = $"Page{pageNumber}_Shape{shape.ID}_Sheet_{safeSheetName}.csv";
                                    string csvPath = Path.Combine(outputDir, csvFileName);

                                    // Configure CSV (Txt) save options.
                                    TxtSaveOptions csvOptions = new TxtSaveOptions(SaveFormat.Csv);
                                    csvOptions.Separator = ',';
                                    // Preserve cell formatting if the option exists.
                                    // (If the property is not available in the referenced version, this line can be omitted.)
                                    // csvOptions.IncludeCellFormatting = true;

                                    // Set the active sheet and save to CSV.
                                    workbook.Worksheets.ActiveSheetIndex = wsIndex;
                                    workbook.Save(csvPath, csvOptions);

                                    Console.WriteLine($"Exported worksheet '{sheet.Name}' to '{csvPath}'.");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper method to replace invalid filename characters.
    private static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}