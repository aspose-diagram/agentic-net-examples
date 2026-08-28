using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Define temporary file path
            string filePath = Path.Combine(Path.GetTempPath(), "CheckBoxTest.vsdx");

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Get the active page
            Page page = diagram.ActivePage;

            // Add a CheckBox ActiveX control to the page
            // Parameters: ControlType, PinX, PinY, Width (in inches), Height (in inches)
            long shapeId = page.AddActiveXControl(ControlType.CheckBox, 2.0, 2.0, 1.0, 0.5);

            // Retrieve the shape and cast its ActiveXControl to CheckBoxActiveXControl
            Shape shape = page.Shapes.GetShape(shapeId);
            CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

            // Modify properties
            checkBox.IsChecked = true;
            checkBox.Caption = "UnitTestCheckBox";
            // Also set the Value property explicitly (Checked)
            checkBox.Value = CheckValueType.Checked;

            // Save the diagram to a file
            diagram.Save(filePath, SaveFileFormat.Vsdx);

            // Dispose the original diagram
            diagram.Dispose();

            // Load the diagram from the saved file
            Diagram loadedDiagram = new Diagram(filePath);
            Page loadedPage = loadedDiagram.ActivePage;
            Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);
            CheckBoxActiveXControl loadedCheckBox = (CheckBoxActiveXControl)loadedShape.ActiveXControl;

            // Verify that the properties persisted
            if (loadedCheckBox.IsChecked != true)
                throw new Exception("IsChecked property was not persisted.");

            if (loadedCheckBox.Caption != "UnitTestCheckBox")
                throw new Exception("Caption property was not persisted.");

            if (loadedCheckBox.Value != CheckValueType.Checked)
                throw new Exception("Value property was not persisted.");

            // If all checks pass, output success message
            Console.WriteLine("CheckBox ActiveX control properties persisted successfully.");

            // Clean up
            loadedDiagram.Dispose();
            // Optionally delete the temporary file
            // File.Delete(filePath);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
