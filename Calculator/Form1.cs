using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private float? a, b,ms;
        private char? _operationNumber;

        private readonly Dictionary<char?, Func<float?, float?, float?>> _operations = new Dictionary<char?, Func<float?, float?, float?>>
        {
            {'/', (f,f1) => f / f1 },
            {'*', (f, f1) => f * f1 },
            {'-', ((f, f1) => f - f1) },
            {'+', (f, f1) => f + f1 }
        };
        public Form1()
        {
            InitializeComponent();
            label1.Font = new Font("Times New Roman", 12.0f);
        }
        #region numbers
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += 0;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 1;
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 2;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 3;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 5;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 6;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1 && textBox1.Text[0] == '0') textBox1.Text = textBox1.Text.Remove(0, 1);
            textBox1.Text += 9;
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += ',';
        }
        #endregion

        #region operations

        private void button20_Click(object sender, EventArgs e)
        {
            label1.Text = String.Format("Sqrt({0})", textBox1.Text);
            textBox1.Text = Math.Sqrt(Convert.ToDouble(textBox1.Text)).ToString();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text = ms.ToString();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            label1.Text = String.Format("Sqr({0})", textBox1.Text);
            textBox1.Text = Math.Pow(Convert.ToDouble(textBox1.Text), 2).ToString();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Calculate();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ms = float.Parse(textBox1.Text);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            a = null;
            b = null;
            textBox1.Text = "0";
            label1.Text = "";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Insert(0, "-");
        }
        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += '/';
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += '-';
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += '+';
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text += '*';
        }
        private void button16_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void button23_MouseHover(object sender, EventArgs e)
        {
            var t = new ToolTip();
            t.SetToolTip(button23, "Сохранить число в память");
        }

        private void button22_MouseHover(object sender, EventArgs e)
        {
            var t = new ToolTip();
            t.SetToolTip(button22, "Прочитать число с памяти");
        }
        #endregion





        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && _operations.ContainsKey(textBox1.Text.Last()))
            {
                var sym = textBox1.Text.Last();
                if (_operationNumber != null)
                {
                    _operationNumber = sym;
                    label1.Text = label1.Text.Length != 0 ? label1.Text.Remove(label1.Text.Length - 1) : "";
                }
                textBox1.Text=textBox1.Text.Remove(textBox1.Text.Length-1, 1);
                if (a != null)
                {
                    Calculate();
                    label1.Text += sym;
                }
                if (textBox1.Text.Length != 0)
                {
                    label1.Text = textBox1.Text + sym;
                    a = float.Parse(textBox1.Text);
                    textBox1.Clear();
                    _operationNumber = sym;
                }
            }
        }

       

        private void Calculate()
        {
            if (a == null) return;
            b = float.Parse(textBox1.Text);
            var res = _operations[_operationNumber](a, b);
            label1.Text = res.ToString();
            a = null;
            b = null;
            textBox1.Text = res.ToString();
        }

        
    }
}
