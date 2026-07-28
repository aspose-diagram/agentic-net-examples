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

                // -------------------------------------------------
                // 1. Add a class module that handles the ShapeAdded event
                // -------------------------------------------------
                int handlerClassIndex = vbaProject.Modules.Add(VbaModuleType.Class, "EventHandlerClass");
                VbaModule handlerClass = vbaProject.Modules[handlerClassIndex];
                handlerClass.Codes = @"
Option Explicit

Private WithEvents vsoDoc As Visio.Document

Private Sub Class_Initialize()
    Set vsoDoc = Application.ActiveDocument
End Sub

Private Sub vsoDoc_ShapeAdded(ByVal Shape As Visio.Shape)
    Dim para As Visio.Paragraph
    Set para = Shape.Characters.Paragraphs.Add
    para.Text = ""Timestamp: "" & Format(Now, ""yyyy-mm-dd hh:nn:ss"")
End Sub
";

                // -------------------------------------------------
                // 2. Add a standard module that creates an instance of the handler class
                // -------------------------------------------------
                int initModuleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "InitModule");
                VbaModule initModule = vbaProject.Modules[initModuleIndex];
                initModule.Codes = @"
Option Explicit

Public Sub InitializeShapeAddedHandler()
    Dim handler As New EventHandlerClass
End Sub
";

                // -------------------------------------------------
                // 3. Add the ThisDocument class module to hook into document events
                // -------------------------------------------------
                int thisDocIndex = vbaProject.Modules.Add(VbaModuleType.Class, "ThisDocument");
                VbaModule thisDocModule = vbaProject.Modules[thisDocIndex];
                thisDocModule.Codes = @"
Option Explicit

Private Sub Document_DocumentOpened(ByVal doc As IVDocument)
    InitializeShapeAddedHandler
End Sub

Private Sub Document_DocumentCreated(ByVal doc As IVDocument)
    InitializeShapeAddedHandler
End Sub
";

                // Save the diagram as a macro‑enabled Visio file
                diagram.Save("DiagramWithTimestampMacro.vsdm", SaveFileFormat.Vsdm);
            }
        }
    }