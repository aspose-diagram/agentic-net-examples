using System;
using System.IO;
using Aspose.Diagram;

class RetrieveShapeExample
{
    public void Execute()
    {
        // Load an existing Visio diagram (replace with actual path)
        Diagram diagram = new Diagram("input.vsdx");

        // Specify the page index (0‑based) and the unique shape ID you want to retrieve
        int pageIndex = 0;          // e.g., first page
        long shapeId = 12345;       // replace with the actual shape ID

        // Get the page from the diagram
        Page page = diagram.Pages.GetPage(pageIndex);

        // Retrieve the shape by its unique ID from the page's shape collection
        Shape shape = page.Shapes.GetShape(shapeId);

        // 'shape' now holds the requested shape and can be used for further processing
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new RetrieveShapeExample();
            obj.Execute();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
