using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace leason_9
{
    public partial class Form1 : Form
    {
        TextBox tb;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int IndexOfTb = 0, x = 12, y = 12;
            StreamReader sr = new StreamReader("files.txt");
            List<string> AllNumbers = new List<string>();
            while(!sr.EndOfStream)
            {
                string[] data = sr.ReadLine().Split(' ');
                for(int i = 0; i < data.Length; i++)
                {
                    AllNumbers.Add(data[i]);
                }
            }
            for(int i = 0; i < 9; i++)
            {
                x = 12;
                for (int j = 0; j < 9; j++)
                {
                    if (AllNumbers[IndexOfTb] == "*")
                    {
                        tb = new TextBox()
                        {
                            Name = "textbox" + IndexOfTb,
                            Size = new Size(30, 29),
                            Location = new Point(x, y),
                            TextAlign = HorizontalAlignment.Center,
                            Multiline = true,
                            Font = new Font("Microsoft Sans Serif", 18),
                            Text = "",
                            ForeColor = Color.Red

                        };
                    }
                    else
                    {
                        tb = new TextBox()
                        {
                            Name = "textbox" + IndexOfTb,
                            Size = new Size(30, 29),
                            Location = new Point(x, y),
                            TextAlign = HorizontalAlignment.Center,
                            Multiline = true,
                            BorderStyle = BorderStyle.None,
                            Font = new Font("Microsoft Sans Serif", 18),
                            Text = AllNumbers[IndexOfTb],
                            Enabled = false
                        };
                    }
                    
                    tb.TextChanged += tb_TextChanged;
                    tb.KeyPress += TextBox81_KeyPress;
                    Controls.Add(tb);
                    IndexOfTb++;
                    if ((j + 1) % 3 == 0)
                        x += 34;
                    else
                        x += 31;

                }
                if ((i + 1) % 3 == 0)
                    y += 33;
                else
                    y += 30;
            }
            pictureBox1.SendToBack();
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            tb = sender as TextBox;
            if(tb.Text.Length > 1)
            {
                tb.Text = tb.Text.Substring(0, 1);
            }
        }
        private void TextBox81_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> NumbersAll = new List<int>();
            try
            {
                for(int i = 0; i < 81; i++)
                {
                    NumbersAll.Add(Convert.ToInt32(Controls["textbox" + i.ToString()].Text));
                    
                }
                int left = 0, right = 9, StopFor = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (StopFor != 0)
                        break;
                    for (int j = left; j < right - 1; j++)
                    {
                        if (StopFor != 0)
                            break;
                        for (int n = j + 1; n < right; n++)
                        {
                            if(NumbersAll[j] == NumbersAll[n])
                            {
                                StopFor++;
                                MessageBox.Show("В " + (i + 1) + "строке присутствует ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }
                    left += 9; right += 9;
                }
                left = 0; right = 72; StopFor = 0;
                for(int i = 0; i < 9; i++)
                {
                    if (StopFor != 0)
                        break;
                    for(int j = left; j < right; j += 9)
                    {
                        if (StopFor != 0)
                            break;
                        for (int n = j + 9; n <= right; n += 9)
                        {
                            if (NumbersAll[j] == NumbersAll[n])
                            {
                                StopFor++;
                                MessageBox.Show("В " + (i + 1) + "столбце присутствует ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }
                    left++; right++;
                }
                left = 0; right = 21; StopFor = 0;
                int[] kvadrat = new int[9];
                int shag = 1, IndexOfKvadrat = 0;
                for(int i = 1; i < 10; i++) 
                {
                    IndexOfKvadrat = 0;
                    for(int j = left; j < right; j++)
                    {
                        kvadrat[IndexOfKvadrat] = NumbersAll[j];
                        IndexOfKvadrat++;
                        if (shag %3 == 0)
                        {
                            j += 6;
                        }
                        shag++;
                    }
                    for(int k = 0; k < 8; k++)
                    {
                        if (StopFor != 0)
                            break;
                        for (int m = k + 1; m < 9; m++)
                        {
                            if (StopFor != 0)
                                break;
                            if(kvadrat[k] == kvadrat[m])
                            {
                                StopFor++;
                                MessageBox.Show("В " + i.ToString() + " квадрате 3x3 присутствует ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }                                                
                        }
                    }
                    if (StopFor != 0) break;
                    if (i % 3 == 0)
                    {
                        left += 21; right += 21;
                    }
                    else
                    {
                        left += 3; right += 3;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не все ячейки заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
