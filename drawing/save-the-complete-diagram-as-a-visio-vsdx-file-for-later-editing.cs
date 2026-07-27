using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one)
        Diagram diagram = new Diagram();

        // TODO: add pages, shapes, etc., if needed

        // Save the diagram as a Visio VSDX file for later editing
        diagram.Save("MyDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
