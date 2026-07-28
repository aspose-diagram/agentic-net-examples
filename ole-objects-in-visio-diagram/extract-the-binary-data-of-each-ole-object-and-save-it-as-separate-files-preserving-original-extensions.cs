using Aspose.Diagram;
using System;
using System.IO;

class ExtractOleObjects
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            int oleCounter = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shapes that contain OLE data expose ForeignData
                    if (shape.ForeignData != null)
                    {
                        ForeignData foreign = shape.ForeignData;

                        // Embedded OLE objects store their binary in ObjectData
                        if (foreign.ObjectData != null && foreign.ObjectData.Length > 0)
                        {
                            // Try to preserve the original file extension
                            string extension = ".bin";
                            if (!string.IsNullOrEmpty(foreign.ObjectSourceFullName))
                            {
                                string extFromSource = Path.GetExtension(foreign.ObjectSourceFullName);
                                if (!string.IsNullOrEmpty(extFromSource))
                                    extension = extFromSource;
                            }

                            string outputFile = $"OleObject_{oleCounter}{extension}";
                            File.WriteAllBytes(outputFile, foreign.ObjectData);
                            oleCounter++;
                        }
                    }
                }
            }

            // No diagram saving required for extraction; saving would use the provided Save methods.

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
