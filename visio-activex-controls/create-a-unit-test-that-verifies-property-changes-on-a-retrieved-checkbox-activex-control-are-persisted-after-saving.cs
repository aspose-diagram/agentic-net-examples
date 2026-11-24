using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Resolve input and output file paths (allow command‑line overrides)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Variables that need to survive across the two phases
        long targetShapeId = 0;
        CheckValueType expectedValue = CheckValueType.Checked;
        string expectedCaption = "UnitTestCheckBox";
        double expectedWidth = 2.0;
        double expectedHeight = 1.0;

        // -----------------------------------------------------------------
        // Phase 1: Load diagram, modify CheckBox properties and save
        // -----------------------------------------------------------------
        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the first CheckBox ActiveX control
            Shape? targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CheckBox)
                    {
                        targetShape = shape;
                        targetShapeId = shape.ID; // Preserve the shape ID for later lookup
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No CheckBox ActiveX control found in the diagram.");
                return;
            }

            // Cast to the specific CheckBox control type
            CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)targetShape.ActiveXControl;

            // Modify properties
            checkBox.Value = expectedValue;          // Set checked state
            checkBox.Caption = expectedCaption;      // Set caption text
            checkBox.Width = expectedWidth;          // Set width (in inches)
            checkBox.Height = expectedHeight;        // Set height (in inches)

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification/saving: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Phase 2: Reload diagram and verify that changes persisted
        // -----------------------------------------------------------------
        try
        {
            // Reload the saved diagram
            Diagram reloadedDiagram = new Diagram(outputPath);

            // Locate the shape by the previously stored ID
            Shape? reloadedShape = null;
            foreach (Page page in reloadedDiagram.Pages)
            {
                // GetShape returns null if the ID is not present on this page
                reloadedShape = page.Shapes.GetShape(targetShapeId);
                if (reloadedShape != null) break;
            }

            if (reloadedShape == null)
            {
                Console.Error.WriteLine("Failed to locate the CheckBox shape after reload.");
                return;
            }

            // Cast to CheckBox control again
            CheckBoxActiveXControl reloadedCheckBox = (CheckBoxActiveXControl)reloadedShape.ActiveXControl;

            // Verify each property; throw if any mismatch
            if (reloadedCheckBox.Value != expectedValue)
                throw new Exception("Value property was not persisted correctly.");

            if (reloadedCheckBox.Caption != expectedCaption)
                throw new Exception("Caption property was not persisted correctly.");

            if (Math.Abs(reloadedCheckBox.Width - expectedWidth) > 0.001)
                throw new Exception("Width property was not persisted correctly.");

            if (Math.Abs(reloadedCheckBox.Height - expectedHeight) > 0.001)
                throw new Exception("Height property was not persisted correctly.");

            // All checks passed
            Console.WriteLine("CheckBox ActiveX control properties persisted successfully after saving.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }
}