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
                // Parameters: ControlType, PinX, PinY, Width, Height (in inches)
                long shapeId = page.AddActiveXControl(ControlType.CheckBox, 2.0, 2.0, 1.0, 0.5);

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Cast the ActiveXControl to CheckBoxActiveXControl
                CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                // Set properties
                checkBox.IsChecked = true;
                checkBox.Caption = "UnitTestCheckBox";
                checkBox.Value = CheckValueType.Checked;

                // Save the diagram to file using Vsdx format
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                // Dispose the original diagram
                diagram.Dispose();

                // Load the diagram back from file
                Diagram loadedDiagram = new Diagram(filePath);
                Page loadedPage = loadedDiagram.ActivePage;
                Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);
                CheckBoxActiveXControl loadedCheckBox = (CheckBoxActiveXControl)loadedShape.ActiveXControl;

                // Verify persisted properties
                if (loadedCheckBox.IsChecked != true)
                    throw new Exception("IsChecked property was not persisted correctly.");

                if (loadedCheckBox.Caption != "UnitTestCheckBox")
                    throw new Exception("Caption property was not persisted correctly.");

                if (loadedCheckBox.Value != CheckValueType.Checked)
                    throw new Exception("Value property was not persisted correctly.");

                Console.WriteLine("All CheckBox ActiveX control properties persisted successfully.");

                // Clean up
                loadedDiagram.Dispose();
                if (File.Exists(filePath))
                    File.Delete(filePath);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }