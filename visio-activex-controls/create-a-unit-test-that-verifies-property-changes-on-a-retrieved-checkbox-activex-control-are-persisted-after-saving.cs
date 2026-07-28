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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a CheckBox ActiveX control to the active page
            // Parameters: ControlType, pinX, pinY, width, height (in inches)
            long shapeId = diagram.ActivePage.AddActiveXControl(ControlType.CheckBox, 2.0, 2.0, 1.0, 0.5);

            // Retrieve the shape and cast its ActiveXControl to CheckBoxActiveXControl
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);
            CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

            // Set properties on the CheckBox
            checkBox.IsChecked = true;
            checkBox.Caption = "UnitTestCheckBox";
            checkBox.IsEnabled = true;

            // Define a temporary file path for saving the diagram
            string tempFile = Path.Combine(Path.GetTempPath(), "CheckBoxTest.vsdx");

            // Save the diagram using a valid overload (file path + SaveFileFormat)
            diagram.Save(tempFile, SaveFileFormat.Vsdx);

            // Load the diagram back from the saved file
            Diagram loadedDiagram = new Diagram(tempFile);

            // Retrieve the same shape from the loaded diagram
            Shape loadedShape = loadedDiagram.ActivePage.Shapes.GetShape(shapeId);
            CheckBoxActiveXControl loadedCheckBox = (CheckBoxActiveXControl)loadedShape.ActiveXControl;

            // Verify that the properties persisted after saving
            if (loadedCheckBox.IsChecked != true)
                throw new Exception("IsChecked property was not persisted.");

            if (loadedCheckBox.Caption != "UnitTestCheckBox")
                throw new Exception("Caption property was not persisted.");

            if (loadedCheckBox.IsEnabled != true)
                throw new Exception("IsEnabled property was not persisted.");

            Console.WriteLine("All CheckBox ActiveX control properties persisted successfully.");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
