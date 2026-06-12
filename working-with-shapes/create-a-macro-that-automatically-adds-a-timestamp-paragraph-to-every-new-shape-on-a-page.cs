using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the VBA project of the diagram
                VbaProject vbaProject = diagram.VbaProject;

                // Add a new procedural VBA module named "AutoTimestamp"
                int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "AutoTimestamp");
                VbaModule timestampModule = vbaProject.Modules[moduleIndex];

                // VBA code that runs whenever a new shape is added to any page.
                // It appends a timestamp paragraph to the shape's text.
                timestampModule.Codes =
@"Option Explicit
Sub Document_ShapeAdded(ByVal Shape As IVShape)
    Dim ts As String
    ts = Format(Now, ""yyyy-mm-dd hh:nn:ss"")
    Shape.Text = Shape.Text & vbCrLf & ts
End Sub";

                // Save the diagram in a macro‑enabled format (VSDM)
                diagram.Save("DiagramWithTimestampMacro.vsdm", SaveFileFormat.Vsdm);
            }

            Console.WriteLine("Diagram created with timestamp macro.");
        }
    }