using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramActiveXAudit <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages and shapes to find ActiveX controls
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Check if the shape contains an ActiveX control
                if (shape.ActiveXControl == null)
                    continue;

                // Determine the control type and cast accordingly
                switch (shape.ActiveXControl.Type)
                {
                    case ControlType.CommandButton:
                        HandleCommandButton(shape);
                        break;

                    case ControlType.TextBox:
                        HandleTextBox(shape);
                        break;

                    case ControlType.CheckBox:
                        HandleCheckBox(shape);
                        break;

                    case ControlType.SpinButton:
                        HandleSpinButton(shape);
                        break;

                    // Add handling for other control types as needed
                    default:
                        Console.WriteLine($"Unsupported ActiveX control type: {shape.ActiveXControl.Type}");
                        break;
                }
            }
        }

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine("Diagram saved with audited changes.");
    }

    // Logs a property change with previous and new values
    static void LogChange(string controlName, string propertyName, object oldValue, object newValue)
    {
        Console.WriteLine($"[{DateTime.Now}] {controlName} - Property '{propertyName}' changed from '{oldValue}' to '{newValue}'.");
    }

    static void HandleCommandButton(Shape shape)
    {
        var button = (CommandButtonActiveXControl)shape.ActiveXControl;

        // Example: Change the Caption
        string oldCaption = button.Caption;
        string newCaption = oldCaption + " (Updated)";
        button.Caption = newCaption;
        LogChange("CommandButton", "Caption", oldCaption, newCaption);

        // Example: Change Width
        double oldWidth = button.Width;
        double newWidth = oldWidth + 0.5; // increase by 0.5 inches
        button.Width = newWidth;
        LogChange("CommandButton", "Width", oldWidth, newWidth);

        // Example: Change Height
        double oldHeight = button.Height;
        double newHeight = oldHeight + 0.2;
        button.Height = newHeight;
        LogChange("CommandButton", "Height", oldHeight, newHeight);
    }

    static void HandleTextBox(Shape shape)
    {
        var textBox = (TextBoxActiveXControl)shape.ActiveXControl;

        // Example: Change the Text content
        string oldText = textBox.Text;
        string newText = oldText + " (Edited)";
        textBox.Text = newText;
        LogChange("TextBox", "Text", oldText, newText);
    }

    static void HandleCheckBox(Shape shape)
    {
        var checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

        // Example: Toggle the check state
        CheckValueType oldValue = checkBox.Value;
        CheckValueType newValue = oldValue == CheckValueType.Checked ? (CheckValueType)0 : CheckValueType.Checked;
        checkBox.Value = newValue;
        LogChange("CheckBox", "Value", oldValue, newValue);
    }

    static void HandleSpinButton(Shape shape)
    {
        var spinButton = (SpinButtonActiveXControl)shape.ActiveXControl;

        // Example: Change the Position (current numeric value)
        int oldPosition = spinButton.Position;
        int newPosition = oldPosition + 10;
        spinButton.Position = newPosition;
        LogChange("SpinButton", "Position", oldPosition, newPosition);
    }
}
