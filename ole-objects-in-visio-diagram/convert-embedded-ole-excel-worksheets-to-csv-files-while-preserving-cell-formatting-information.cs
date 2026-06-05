using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Cells;
using Aspose.Cells.Saving;

class Program
{
    static void Main(string[] args)
    {
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(diagramPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        byte[] oleData = shape.ForeignData.ObjectData;

                        if (oleData == null || oleData.Length == 0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} has no OLE data.");
                            continue;
                        }

                        try
                        {
                            using (MemoryStream oleStream = new MemoryStream(oleData))
                            {
                                var formatInfo = Aspose.Cells.FileFormatUtil.DetectFileFormat(oleStream);
                                bool isExcel = formatInfo.FileFormatType == Aspose.Cells.FileFormatType.Xlsx ||
                                               formatInfo.FileFormatType == Aspose.Cells.FileFormatType.Xlsb ||
                                               formatInfo.FileFormatType == Aspose.Cells.FileFormatType.Xlsm;

                                if (!isExcel)
                                {
                                    Console.WriteLine($"Shape ID {shape.ID} does not contain an Excel OLE object.");
                                    continue;
                                }

                                oleStream.Position = 0;
                                Workbook workbook = new Workbook(oleStream);

                                for (int i = 0; i < workbook.Worksheets.Count; i++)
                                {
                                    Worksheet worksheet = workbook.Worksheets[i];
                                    string csvFileName = $"OleShape_{shape.ID}_Sheet_{i + 1}_{worksheet.Name}.csv";

                                    TxtSaveOptions saveOptions = new TxtSaveOptions(SaveFormat.CSV);
                                    saveOptions.Separator = ',';

                                    workbook.Save(csvFileName, saveOptions);
                                    Console.WriteLine($"Exported worksheet '{worksheet.Name}' from shape ID {shape.ID} to {csvFileName}");
                                }
                            }
                        }
                        catch (Exception exShape)
                        {
                            Console.Error.WriteLine($"Error processing OLE shape ID {shape.ID}: {exShape.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}