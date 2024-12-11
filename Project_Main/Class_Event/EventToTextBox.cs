using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Main.Class_Event
{
    public sealed class EventToTextBox
    {
        private readonly TextBox oTextBox ;
        private readonly Color oColor ;
        private readonly string oValue ;

        /*/////////////////////////////////////////////////////////////////////////////////////////////////*/

        /*Con el parametros de este constructor se llama al metodo (BorderColorTextBox_Paint) */
        public EventToTextBox(TextBox oTextBox, Color oColor)
        {
            this.oTextBox = oTextBox ;
            this.oColor = oColor ;
        }
        
        public void BorderColorTextBox_Paint(object sender, PaintEventArgs e)
        {
            Pen oPen = new Pen(oColor);

            int x = oTextBox.Left - 1;
            int y = oTextBox.Top - 1;
            int width = oTextBox.Width + 1;
            int height = oTextBox.Height + 1;

            e.Graphics.DrawRectangle(oPen,
                new Rectangle(x, y, width, height));
        }
        
        /*/////////////////////////////////////////////////////////////////////////////////////////////////*/

        /*Sin los parametro de este constructor se llama a los siguientes metodos */
        public EventToTextBox() { }

        /*Evento que valida que en la caja de texto solo se ingrese letras*/
        public void InsertOnlyText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) ? false : true ;
        }
        /*Evento que valida que en la caja de texto solo se ingrese numeros*/
        public void InsertOnlyNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) ? false : true ;
        }
        /*Evento que valida que en la caja de texto solo se ngrese numeros y letras */
        public void InsertOnlyNumberAndText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) ? false : true ;
        }
        /*Evento que valida que en la caja de texto no se ingrese nada*/
        public void NotInsert_KeyPress(object sender, KeyPressEventArgs e) 
        {
            e.Handled = char.IsLetter(e.KeyChar) || char.IsSeparator(e.KeyChar)
                || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar)
                || char.IsNumber(e.KeyChar) ? true : false ;
        }

        /*/////////////////////////////////////////////////////////////////////////////////////////////////*/

        /*Con el parametro de este constructor se llama al metodo (AvoidEnteringInitialValue_Texboxt) */
        public EventToTextBox(TextBox oTextBox, string oValue)
        {
            this.oTextBox = oTextBox;
            this.oValue = oValue;
        }

        /*Evento que evita que se ingrese un valor especificado al inicio de la caja de texto*/
        public void AvoidEnteringInitialValue_Texboxt(object sender, EventArgs e)
        {
            if (oTextBox.Text.StartsWith(oValue))
            {
                oTextBox.Text = oTextBox.Text.Substring(1);
            }           
        }


    }
}
