using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate backwards over the shapes collection to allow safe removal.
                    for (int i = page.Shapes.Count - 1; i >= 0; i--)
                    {
                        // Retrieve the shape at the current index.
                        Aspose.Diagram.Shape shape = page.Shapes[i];

                        // Verify the shape is a foreign (OLE) shape and contains object data.
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Detect the embedded object's file format using Aspose.Diagram utilities.
                            // NOTE: DetectFileFormat expects a Stream, so wrap the byte[] in a MemoryStream.
                            Aspose.Diagram.FileFormatInfo formatInfo =
                                Aspose.Diagram.FileFormatUtil.DetectFileFormat(new MemoryStream(shape.ForeignData.ObjectData));

                            // Determine if the OLE object is an Excel worksheet.
                            bool isExcel = formatInfo.FileFormatType == Aspose.Diagram.FileFormatType.Xlsx ||
                                           formatInfo.FileFormatType == Aspose.Diagram.FileFormatType.Excel97To2003 ||
                                           formatInfo.FileFormatType == Aspose.Diagram.FileFormatType.Xlsm;

                            // If it is an Excel OLE object, remove the shape from the page.
                            if (isExcel)
                            {
                                page.Shapes.Remove(shape);
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
