using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file containing OLE Excel objects
                string visioPath = "input.vsdx";

                // Folder where extracted CSV files will be saved
                string outputFolder = "ExtractedCsv";
                Directory.CreateDirectory(outputFolder);

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is an OLE foreign object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Basic check for Excel file extensions in the source name
                            string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                            if (sourceName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                                sourceName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                            {
                                // Load the OLE Excel binary into a MemoryStream
                                using (MemoryStream oleStream = new MemoryStream(shape.ForeignData.ObjectData))
                                {
                                    // Load the workbook using Aspose.Cells
                                    Workbook workbook = new Workbook(oleStream);

                                    // Export each worksheet to a separate CSV file
                                    foreach (Worksheet sheet in workbook.Worksheets)
                                    {
                                        // Configure CSV (text) save options
                                        TxtSaveOptions saveOptions = new TxtSaveOptions();
                                        saveOptions.Separator = ',';

                                        // Build a unique CSV file name
                                        string csvFileName = Path.Combine(
                                            outputFolder,
                                            $"Shape_{shape.ID}_Sheet_{sheet.Name}.csv");

                                        // Set the active sheet and save as CSV
                                        workbook.Worksheets.ActiveSheetIndex = sheet.Index;
                                        workbook.Save(csvFileName, saveOptions);
                                    }
                                }
                            }
                        }
                    }
                }

                // Optionally, save the (unchanged) diagram to a new file
                string outputVisio = "output.vsdx";
                diagram.Save(outputVisio, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }