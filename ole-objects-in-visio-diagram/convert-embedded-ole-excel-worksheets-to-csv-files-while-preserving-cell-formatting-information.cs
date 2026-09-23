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

                // Path to the Visio file containing OLE Excel objects
                string diagramPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape is a foreign OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Extract the OLE binary data
                            byte[] oleData = shape.ForeignData.ObjectData;

                            // Load the OLE data as an Excel workbook
                            using (MemoryStream oleStream = new MemoryStream(oleData))
                            {
                                Workbook workbook = new Workbook(oleStream);

                                // Export each worksheet to a separate CSV file
                                for (int i = 0; i < workbook.Worksheets.Count; i++)
                                {
                                    workbook.Worksheets.ActiveSheetIndex = i;
                                    string sheetName = workbook.Worksheets[i].Name;

                                    // Build a CSV file name that includes the shape ID and worksheet name
                                    string csvFileName = $"{Path.GetFileNameWithoutExtension(diagramPath)}_Shape{shape.ID}_Sheet{sheetName}.csv";

                                    // Configure CSV (text) save options
                                    TxtSaveOptions csvOptions = new TxtSaveOptions();
                                    csvOptions.Separator = ',';

                                    // Save the worksheet as CSV
                                    workbook.Save(csvFileName, csvOptions);
                                }
                            }
                        }
                    }
                }

                // Optionally, save the diagram (unchanged) to a new file
                string outputDiagramPath = "output.vsdx";
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }