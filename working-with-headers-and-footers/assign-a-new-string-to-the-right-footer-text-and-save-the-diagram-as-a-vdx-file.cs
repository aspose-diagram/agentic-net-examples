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
        diagram.HeaderFooter.FooterRight = "Confidential – Draft Version";

        // Save the diagram as VDX using the built‑in Save method
        diagram.Save("OutputDiagram.vdx", SaveFileFormat.Vdx);
    }
}
