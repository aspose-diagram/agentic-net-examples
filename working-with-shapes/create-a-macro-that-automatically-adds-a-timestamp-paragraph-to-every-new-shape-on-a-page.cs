using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Create a new blank diagram
            using (Diagram diagram = new Diagram())
            {
                // Add a VBA module (ThisDocument) to handle the ShapeAdded event
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "ThisDocument");
                VbaModule module = diagram.VbaProject.Modules[moduleIndex];

                // VBA code that adds a timestamp paragraph to every newly added shape
                string vbaCode = @"
Option Explicit

Private Sub Document_ShapeAdded(ByVal Shape As IVShape)
    Dim timestamp As String
    timestamp = Format(Now, ""yyyy-mm-dd hh:nn:ss"")
    ' Append a new paragraph with the timestamp
    Shape.Text = Shape.Text & vbCrLf & timestamp
End Sub
";

                module.Codes = vbaCode;

                // Save the diagram as a macro-enabled Visio file
                string outputPath = "TimestampMacro.vsdm";
                diagram.Save(outputPath, SaveFileFormat.Vsdm);
            }

            Console.WriteLine("Diagram with timestamp macro has been created and saved.");
        }
    }