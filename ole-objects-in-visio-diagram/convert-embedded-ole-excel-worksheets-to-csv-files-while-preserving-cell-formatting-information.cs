using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;
using Aspose.Cells.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // Output folder for CSV files
                string outputFolder = "CsvOutput";

                if (!Directory.Exists(outputFolder))
                    Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE foreign object
                        if (shape.Type != TypeValue.Foreign || shape.ForeignData == null)
                            continue;

                        // Ensure the foreign data represents an embedded object
                        if (shape.ForeignData.ForeignType != ForeignType.Object)
                            continue;

                        // Check if the OLE object is an Excel workbook by inspecting the source file name
                        string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                        if (!sourceName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) &&
                            !sourceName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                            continue;

                        // Retrieve the OLE binary data
                        byte[] oleData = shape.ForeignData.ObjectData;
                        if (oleData == null || oleData.Length == 0)
                            continue;

                        // Load the Excel workbook from the OLE data
                        using (MemoryStream excelStream = new MemoryStream(oleData))
                        {
                            Workbook workbook = new Workbook(excelStream);

                            // Export each worksheet to a separate CSV file
                            TxtSaveOptions csvOptions = new TxtSaveOptions();
                            csvOptions.Separator = ',';

                            for (int i = 0; i < workbook.Worksheets.Count; i++)
                            {
                                workbook.Worksheets.ActiveSheetIndex = i;
                                Worksheet sheet = workbook.Worksheets[i];

                                // Build a CSV file name that includes shape identifier and worksheet name
                                string safeShapeName = string.IsNullOrWhiteSpace(shape.NameU) ? $"Shape_{shape.ID}" : shape.NameU;
                                string csvFileName = $"{safeShapeName}_Sheet_{i + 1}_{sheet.Name}.csv";
                                string csvPath = Path.Combine(outputFolder, csvFileName);

                                // Save the active worksheet as CSV
                                workbook.Save(csvPath, csvOptions);
                            }
                        }

                        // No modification to the OLE data is performed; preserve original content
                    }
                }

                // Save the (potentially unchanged) diagram back to a file
                string outputVisioPath = "output.vsdx";
                diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }