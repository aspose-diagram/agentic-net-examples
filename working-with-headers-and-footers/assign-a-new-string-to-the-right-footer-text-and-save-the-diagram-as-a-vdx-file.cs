using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Set the right footer text
        diagram.HeaderFooter.FooterRight = "My custom right footer text";

        // Save the diagram as VDX using the built‑in Save method with SaveFileFormat
        diagram.Save("output.vdx", SaveFileFormat.Vdx);
    }
}
