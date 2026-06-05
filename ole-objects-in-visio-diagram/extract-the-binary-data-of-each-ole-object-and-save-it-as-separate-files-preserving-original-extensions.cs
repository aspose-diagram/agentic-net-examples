using System;
using System.IO;
using Aspose.Diagram;

class Program
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
                    // Each shape may contain ForeignData (OLE object, image, etc.)
                    ForeignData foreignData = shape.ForeignData;
                    if (foreignData == null)
                        continue;

                    // ----- Embedded OLE object -----
                    // ObjectData holds the raw bytes of the embedded OLE object.
                    if (foreignData.ObjectData != null && foreignData.ObjectData.Length > 0)
                    {
                        string extension = DetermineExtension(foreignData);
                        string fileName = $"OleObject_{oleCounter}{extension}";
                        File.WriteAllBytes(fileName, foreignData.ObjectData);
                        oleCounter++;
                        continue;
                    }

                    // ----- Linked OLE object -----
                    // ObjectSourceFullName contains the original file path of the linked object.
                    if (!string.IsNullOrEmpty(foreignData.ObjectSourceFullName))
                    {
                        string sourcePath = foreignData.ObjectSourceFullName;
                        string extension = Path.GetExtension(sourcePath);
                        string fileName = $"OleObject_{oleCounter}{extension}";

                        // If the source file exists, copy it; otherwise create an empty placeholder.
                        if (File.Exists(sourcePath))
                            File.Copy(sourcePath, fileName, true);
                        else
                            File.WriteAllBytes(fileName, new byte[0]);

                        oleCounter++;
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to infer a file extension for an embedded OLE object.
    // If the source name is available, its extension is used; otherwise default to .bin.
    static string DetermineExtension(ForeignData foreignData)
    {
        if (!string.IsNullOrEmpty(foreignData.ObjectSourceFullName))
        {
            string ext = Path.GetExtension(foreignData.ObjectSourceFullName);
            if (!string.IsNullOrEmpty(ext))
                return ext;
        }
        // Fallback when no source name is present.
        return ".bin";
    }
}
