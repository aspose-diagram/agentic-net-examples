using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new Visio diagram instance
        Diagram diagram = new Diagram();

        // (Optional) Add pages, masters, shapes, etc. to the diagram here

        // Save the diagram in VSDX format for later editing
        diagram.Save("MyDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
