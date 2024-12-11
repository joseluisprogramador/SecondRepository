using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Main.Class_Event
{
    public sealed class EventToButton
    {

        private readonly TextBox oTextBoxConcatenate;
        private readonly TextBox oTextBoxOperation;
        public EventToButton() { }
        public EventToButton(TextBox TextBoxConcatenate, TextBox TextBoxOperation = null)
        {
            this.oTextBoxConcatenate = TextBoxConcatenate;
            this.oTextBoxOperation = TextBoxOperation;
        }

        public void GetCharacterAssign(Button oButton)
        {
            oButton.Click += AssignCharacter_Click;
        }

        private void AssignCharacter_Click(object sender, EventArgs e)
        {
            Button oButton = (Button)sender;

            string ButtonText = oButton.Text;

            foreach ((string, string) Symbol in Symbols.AllSymbols())
            {
                bool Contains = Symbol.Item1.Contains(ButtonText);
                if (Contains)
                {
                    InsertTextAtCursor(Symbol.Item2);
                    break ;
                }
            }       
        }

        private void InsertTextAtCursor(string Text)
        {
            int SelectionIndex = oTextBoxConcatenate.SelectionStart;
            oTextBoxConcatenate.Text = oTextBoxConcatenate.Text.Insert(SelectionIndex, Text);
            oTextBoxConcatenate.SelectionStart = SelectionIndex + Text.Length;
        }

        public void DeleteCharacterAssign(Button oButton)
        {
            oButton.Click += DeleteCharacter_Click;
        }
        private void DeleteCharacter_Click(object sender, EventArgs e)
        {
            try
            {
                Button oButton = (Button)sender;
                foreach (string GetSymbol in Symbols.ClearSymbols)
                {
                    if (oButton.Text == GetSymbol)
                    {
                        switch (oButton.Text)
                        {
                            case "Ac":
                                oTextBoxConcatenate.Text = "";
                                oTextBoxOperation.Text = "";
                                break;
                            case "Del":
                                int SelectionIndex = oTextBoxConcatenate.SelectionStart;
                                if (SelectionIndex > 0)
                                {
                                    oTextBoxConcatenate.Text = oTextBoxConcatenate.Text.Remove(SelectionIndex - 1, 1);
                                    oTextBoxConcatenate.SelectionStart = SelectionIndex - 1;
                                }
                                break;
                        }

                    }
                }

            }
            catch (Exception oException)
            {
                 oTextBoxOperation.Text = oException.Message ;
            }
         
        }

        public void CalculateOperation(Button oButton)
        {
            oButton.Click += BtnEqual_Click;         
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string Expression = oTextBoxConcatenate.Text;
                Expression = ReplaceRootSymbol(Expression);
                Expression = ReplaceExponentSymbol(Expression);
                object Calculate = new DataTable().Compute(Expression, null);
                oTextBoxOperation.Text = Calculate.ToString();
            }
            catch (Exception oException)
            {
                oTextBoxOperation.Text = oException.Message;
            }
        }

        private string ReplaceRootSymbol(string Expression)
        {
            while (Expression.Contains("√"))
            {
                int index = Expression.IndexOf("√");
                int endIndex = index + 1;
                while (endIndex < Expression.Length && (char.IsDigit(Expression[endIndex]) || Expression[endIndex] == '.'))
                {
                    endIndex++;
                }

                string number = Expression.Substring(index + 1, endIndex - index - 1);
                double rootResult = Math.Sqrt(Convert.ToDouble(number));
                Expression = Expression.Replace($"√{number}", rootResult.ToString());
            }
            return Expression;
        }

        private string ReplaceExponentSymbol(string expression)
        {
            while (expression.Contains("^"))
            {
                int index = expression.IndexOf("^");

                // Find the base number
                int baseStartIndex = index - 1;
                while (baseStartIndex >= 0 && (char.IsDigit(expression[baseStartIndex]) || expression[baseStartIndex] == '.'))
                {
                    baseStartIndex--;
                }
                baseStartIndex++;

                string baseNumber = expression.Substring(baseStartIndex, index - baseStartIndex);

                // Find the exponent number
                int exponentEndIndex = index + 1;
                while (exponentEndIndex < expression.Length && (char.IsDigit(expression[exponentEndIndex]) || expression[exponentEndIndex] == '.'))
                {
                    exponentEndIndex++;
                }

                string exponentNumber = expression.Substring(index + 1, exponentEndIndex - index - 1);

                // Calculate the power
                double baseValue = Convert.ToDouble(baseNumber);
                double exponentValue = Convert.ToDouble(exponentNumber);
                double powerResult = Math.Pow(baseValue, exponentValue);

                string toReplace = $"{baseNumber}^{exponentNumber}";
                expression = expression.Replace(toReplace, powerResult.ToString());
            }
            return expression;
        }


    }
}
