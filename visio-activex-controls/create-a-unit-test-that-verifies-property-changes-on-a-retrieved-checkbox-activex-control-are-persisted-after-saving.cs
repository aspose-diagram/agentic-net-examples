using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Define temporary file path for the test diagram
        string tempFile = Path.Combine(Path.GetTempPath(), "CheckBoxTest.vsdx");

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Retrieve the first (default) page
            Page page = diagram.Pages[0];

            // Add a CheckBox ActiveX control; method returns the shape ID
            long shapeId = page.AddActiveXControl(ControlType.CheckBox, 2.0, 2.0, 1.0, 0.5);

            // Retrieve the shape object using the returned ID
            Shape checkBoxShape = page.Shapes.GetShape(shapeId);

            // Cast the ActiveXControl to CheckBoxActiveXControl and set its value to Checked
            CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)checkBoxShape.ActiveXControl;
            checkBox.Value = CheckValueType.Checked;

            // Save the diagram to a VSDX file
            diagram.Save(tempFile, SaveFileFormat.Vsdx);

            // Verify the file was created before attempting to load it
            if (!File.Exists(tempFile))
            {
                Console.Error.WriteLine($"File not found after save: {tempFile}");
                return;
            }

            // Load the diagram back from the saved file
            Diagram loadedDiagram = new Diagram(tempFile);

            // Retrieve the same page and shape by the stored ID
            Page loadedPage = loadedDiagram.Pages[0];
            Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

            // Ensure the shape still contains a CheckBox ActiveX control
            if (loadedShape.ActiveXControl == null || loadedShape.ActiveXControl.Type != ControlType.CheckBox)
            {
                throw new Exception("The loaded shape does not contain a CheckBox ActiveX control.");
            }

            // Cast to CheckBoxActiveXControl and verify the persisted value
            CheckBoxActiveXControl loadedCheckBox = (CheckBoxActiveXControl)loadedShape.ActiveXControl;
            if (loadedCheckBox.Value != CheckValueType.Checked)
            {
                throw new Exception("CheckBox value was not persisted after saving.");
            }

            // If we reach this point, the test passed
            Console.WriteLine("CheckBox ActiveX control property persisted successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}