using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Project_Main.Class_Event;


namespace Project_Main
{
    public partial class Form_Calculator : Form
    {
        public Form_Calculator()
        {
            InitializeComponent();  
            TextBox_Operations.BorderStyle = BorderStyle.None;
            
            EventToTextBox oEventToTextBox ;     

            oEventToTextBox = new EventToTextBox(TextBox_Concatenate,"0");
            TextBox_Concatenate.TextChanged += oEventToTextBox.AvoidEnteringInitialValue_Texboxt ;

            oEventToTextBox = new EventToTextBox(TextBox_Concatenate, Color.Green);
            Paint += oEventToTextBox.BorderColorTextBox_Paint ;

            oEventToTextBox = new EventToTextBox(TextBox_Operations,Color.Green);
            Paint += oEventToTextBox.BorderColorTextBox_Paint ;

            EventToButton oEventButton = null ;
        
            foreach (Button oButton in Controls.OfType<Button>())
            {
                oEventButton = new EventToButton(TextBox_Concatenate,TextBox_Operations);
                oEventButton.GetCharacterAssign(oButton);
                oEventButton.DeleteCharacterAssign(oButton);
            }
                  
            Button oButtonEqual = Controls.OfType<Button>().FirstOrDefault(oButton => oButton.Text == "=");
            
            if (oButtonEqual != null && oEventButton != null)
            {
                oEventButton.CalculateOperation(oButtonEqual);
            }
           
        }       
    }
}
