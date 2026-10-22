using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [OutputDirectory]");
            return;
        }

        string visioPath = args[0];
        string outputDir = args.Length >= 2 ? args[1] : Path.GetDirectoryName(visioPath);

        // Guard against missing Visio file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
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
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is a foreign (OLE) shape.
                    if (shape.Type != TypeValue.Foreign)
                        continue;

                    // Verify the shape contains foreign data with an embedded object.
                    if (shape.ForeignData == null || shape.ForeignData.ObjectData == null)
                        continue;

                    // Identify Excel OLE objects by checking the source file extension.
                    string sourceName = shape.ForeignData.ObjectSourceFullName?.ToLowerInvariant() ?? string.Empty;
                    bool isExcel = sourceName.EndsWith(".xls") ||
                                   sourceName.EndsWith(".xlsx") ||
                                   sourceName.EndsWith(".xlsm");

                    if (!isExcel)
                        continue; // Skip non‑Excel OLE objects.

                    // Extract the OLE binary data.
                    byte[] oleData = shape.ForeignData.ObjectData;

                    // Load the Excel workbook from the OLE stream.
                    using (MemoryStream oleStream = new MemoryStream(oleData))
                    {
                        Workbook workbook = new Workbook(oleStream);

                        // Export each worksheet to a separate CSV file.
                        for (int i = 0; i < workbook.Worksheets.Count; i++)
                        {
                            // Activate the worksheet to be saved.
                            workbook.Worksheets.ActiveSheetIndex = i;
                            string sheetName = workbook.Worksheets[i].Name;

                            // Build a safe file name for the CSV output.
                            string safeShapeName = string.IsNullOrEmpty(shape.NameU) ? "Shape" : shape.NameU;
                            string csvFileName = Path.Combine(
                                outputDir,
                                $"{safeShapeName}_Sheet{i + 1}_{sheetName}.csv");

                            // Configure CSV (text) save options – use Aspose.Cells.TxtSaveOptions to avoid ambiguity.
                            Aspose.Cells.TxtSaveOptions txtOptions = new Aspose.Cells.TxtSaveOptions
                            {
                                Separator = ','
                            };

                            // Save the worksheet as CSV.
                            workbook.Save(csvFileName, txtOptions);
                            Console.WriteLine($"Exported: {csvFileName}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}