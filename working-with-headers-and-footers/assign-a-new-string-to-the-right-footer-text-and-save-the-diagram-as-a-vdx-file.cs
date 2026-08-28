using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Assign a new string to the right portion of the footer
        diagram.HeaderFooter.FooterRight = "Confidential - Draft";

        // Save the diagram as a VDX file
        diagram.Save("output.vdx", SaveFileFormat.Vdx);
    }
}
