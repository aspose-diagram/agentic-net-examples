using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file (must be a format that supports VBA, e.g., .vsdx)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Add a new VBA procedural module named "FlipFillInheritance"
                int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "FlipFillInheritance");
                VbaModule module = diagram.VbaProject.Modules[moduleIndex];

                // VBA code that iterates over the current selection and toggles fill inheritance
                module.Codes = 
                @"Sub FlipFillInheritance()
                Dim shp As Visio.Shape
                For Each shp In Visio.ActiveWindow.Selection
                ' Toggle between using the inherited fill color and a local fill color (set to no fill)
                If shp.CellsU(""FillForegnd"").ResultIU = shp.CellsU(""InheritFill!FillForegnd"").ResultIU Then
                shp.CellsU(""FillForegnd"").FormulaU = ""0""
                Else
                shp.CellsU(""FillForegnd"").FormulaU = ""=InheritFill!FillForegnd""
                End If
                Next shp
                End Sub";

                // Save the diagram as a macro-enabled Visio file
                diagram.Save("output.vsdm", SaveFileFormat.Vsdm);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }