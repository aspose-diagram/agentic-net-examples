using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file that contains a CheckBox ActiveX control.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output file where the modified diagram will be saved.
        string outputPath = "output.vsdx";

        // Variables to keep track of the CheckBox shape across the save/load cycle.
        long checkBoxShapeId = 0;
        int checkBoxPageIndex = -1;

        // -----------------------------------------------------------------
        // Load the diagram, locate the CheckBox ActiveX control, modify it,
        // and save the diagram.
        // -----------------------------------------------------------------
        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Search for the first shape that contains a CheckBox ActiveX control.
            bool found = false;
            for (int p = 0; p < diagram.Pages.Count && !found; p++)
            {
                Page page = diagram.Pages[p];
                for (int s = 0; s < page.Shapes.Count && !found; s++)
                {
                    Shape shape = page.Shapes[s];
                    // Ensure the shape actually has an ActiveX control.
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CheckBox)
                    {
                        // Store identifiers for later verification.
                        checkBoxShapeId = shape.ID;
                        checkBoxPageIndex = p;
                        // Cast to the specific CheckBox control type.
                        CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                        // Set the checkbox to checked.
                        checkBox.Value = CheckValueType.Checked;
                        // Disable the control. // Changed from BOOL.False to false (bool)
                        checkBox.IsEnabled = false;
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.Error.WriteLine("No CheckBox ActiveX control found in the diagram.");
                return;
            }

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during modification and saving: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Reload the saved diagram and verify that the property changes
        // on the CheckBox ActiveX control persisted.
        // -----------------------------------------------------------------
        try
        {
            // Reload the diagram from the output file.
            Diagram reloadedDiagram = new Diagram(outputPath);

            // Validate stored page index.
            if (checkBoxPageIndex < 0 || checkBoxPageIndex >= reloadedDiagram.Pages.Count)
                throw new Exception("Invalid page index after reload.");

            // Retrieve the page and shape by their stored identifiers.
            Page reloadedPage = reloadedDiagram.Pages[checkBoxPageIndex];
            Shape reloadedShape = reloadedPage.Shapes.GetShape(checkBoxShapeId);

            // Ensure the shape still contains a CheckBox ActiveX control.
            if (reloadedShape.ActiveXControl == null || reloadedShape.ActiveXControl.Type != ControlType.CheckBox)
                throw new Exception("CheckBox ActiveX control not found after reload.");

            // Cast to the specific CheckBox control type.
            CheckBoxActiveXControl reloadedCheckBox = (CheckBoxActiveXControl)reloadedShape.ActiveXControl;

            // Verify that the Value property persisted as Checked.
            if (reloadedCheckBox.Value != CheckValueType.Checked)
                throw new Exception("CheckBox Value property was not persisted correctly.");

            // Verify that the IsEnabled property persisted as False. // Changed comparison to false (bool)
            if (reloadedCheckBox.IsEnabled != false)
                throw new Exception("CheckBox IsEnabled property was not persisted correctly.");

            Console.WriteLine("Property changes on CheckBox ActiveX control were persisted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification failed: {ex.Message}");
        }
    }
}
