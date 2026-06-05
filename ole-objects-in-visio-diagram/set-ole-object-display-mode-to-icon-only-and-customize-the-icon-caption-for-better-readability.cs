using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                    {
                        if (shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            shape.ForeignData.ShowAsIcon = BOOL.True;

                            // Set custom caption by updating the shape's text
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt("My Custom OLE Icon"));
                        }
                    }
                }
            }

            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}