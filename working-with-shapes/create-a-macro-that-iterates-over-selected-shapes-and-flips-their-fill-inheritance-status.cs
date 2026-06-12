using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project (read‑only property)
            VbaProject vba = diagram.VbaProject;

            // Add a new procedural VBA module named "FlipFillInheritance"
            int moduleIndex = vba.Modules.Add(VbaModuleType.Procedural, "FlipFillInheritance");

            // Retrieve the module and assign the VBA macro code
            VbaModule module = vba.Modules[moduleIndex];
            module.Codes = @"
            Sub FlipFillInheritance()
            Dim shp As Visio.Shape
            For Each shp In Visio.ActiveWindow.Selection
            ' Determine if the shape currently inherits its fill color
            If shp.CellsU(""FillForegnd"").FormulaU = shp.CellsU(""InheritFillForegnd"").FormulaU Then
                ' Shape is inheriting – set a custom fill color (red)
                shp.CellsU(""FillForegnd"").FormulaU = ""RGB(255,0,0)""
            Else
                ' Shape has a custom fill – revert to inherited fill color
                shp.CellsU(""FillForegnd"").FormulaU = shp.CellsU(""InheritFillForegnd"").FormulaU
            End If
            Next shp
            End Sub
            ";

            // Save the diagram as a macro‑enabled Visio file
            diagram.Save("output.vsdm", SaveFileFormat.Vsdm);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
