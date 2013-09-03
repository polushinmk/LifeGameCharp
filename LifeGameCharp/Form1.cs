using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LifeGameCharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //разрисовка панели-поля
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = Graphics.FromHwnd(panel1.Handle);
            for(int i=0;i<=500;i+=10)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Gray)), i, 0, i, 500);
            }
            for (int i = 0; i <= 500; i += 10)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Gray)), 0, i, 500, i);
            }
            panredraw();
        }
        //2 массива для прорисовки и для логики игры
        byte[,] coords = new byte[500, 500];
        byte[,] life = new byte[500, 500];
        int pokolenie = 0;
        //файл для сохранения настроек
        FileInfo f = new FileInfo("temp.dat");
        //рисовать или удалить клетку
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int x=0;
            int y=0;
            x=(e.X/10)*10;
            y=(e.Y/10)*10;
            Graphics g = Graphics.FromHwnd(panel1.Handle);
            if (coords[x, y] == 1)
            {
                g.FillRectangle(new SolidBrush(Color.White), x + 1, y + 1, 8, 8);
                coords[x, y] = 0;
                life[x, y] = 0;
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.Black), x + 1, y + 1, 8, 8);
                coords[x, y] = 1;
                life[x, y] = 1;
            }
        }
        //перерисовка панели
        private void panredraw()
        {
            Graphics g = Graphics.FromHwnd(panel1.Handle);
            for (int j = 0; j < 500; j+=10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    if (coords[i, j] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), i+1, j+1, 8, 8);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.White), i+1, j+1, 8, 8);
                    }
                }
            }
        }
        //логика игры, включается таймер и всё повторяется снова и снова до его выключения…
        public void life_engine()
        {
                timer1.Start();
                timer1.Tick += new EventHandler(timer1_Tick);
        }
        //вот сама функция происходящая после реакции таймера, реализация логики по правилам
        public void timer1_Tick(object sender, EventArgs e)
        {
            byte n = 0;
            Graphics g = Graphics.FromHwnd(panel1.Handle);
            for (int j = 0; j < 500; j += 10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    n = 0;
                    //для обычных клеток
                    if (i > 0 && j > 0 && i < 490 && j < 490)
                    {
                        if (life[i - 10, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j] == 1)
                        {
                            n++;
                        }
                    }
                    //для клеток имеющих меньше 8 соседей
                    else if (i == 0 && j == 0)
                    {
                        if (life[i+10,j]==1)
                        {
                            n++;
                        }
                        if (life[i + 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j + 10] == 1)
                        {
                            n++;
                        }
                    }
                    else if (i == 490 && j == 490)
                    {
                        if (life[i - 10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j-10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j-10] == 1)
                        {
                            n++;
                        }
                    }
                    else if (i == 0 && j == 490)
                    {
                        if (life[i, j-10] == 1)
                        {
                            n++;
                        }
                        if (life[i+10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j-10] == 1)
                        {
                            n++;
                        }
                    }
                    else if (i == 490 && j == 0)
                    {
                        if (life[i - 10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j+10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j+10] == 1)
                        {
                            n++;
                        }
                    }
                    else if ((i == 0 && (j > 10 && j < 490)))
                    {
                        if (life[i, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j] == 1)
                        {
                            n++;
                        }
                    }
                    else if ((i==490 && (j > 10 && j < 490)))
                    {
                        if (life[i - 10, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j + 10] == 1)
                        {
                            n++;
                        }
                    }
                    else if ((j == 0 && (i > 10 && i < 490)))
                    {
                        if (life[i - 10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j + 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j] == 1)
                        {
                            n++;
                        }
                    }
                    else if ((j == 490 && (i > 10 && i < 490)))
                    {
                        if (life[i - 10, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j - 10] == 1)
                        {
                            n++;
                        }
                        if (life[i - 10, j] == 1)
                        {
                            n++;
                        }
                        if (life[i + 10, j] == 1)
                        {
                            n++;
                        }
                    }
                    //проверка жива или нет клетка…вот весь этот цикл и жрёт кучу процессорного времени, когда размер поля небольшой, как тут 200*200 то ничего не чувствуется, но вот потом…
                        if (life[i, j] == 0 && n == 3)
                        {
                            coords[i, j] = 1;
                        }
                        if ((life[i, j] == 1 && n < 2) || (life[i,j] == 1 && n > 3))
                        {
                            coords[i,j]=0;
                        }
                }
            }
            //перерисовать панель с учётом оживших и умерших клеток
            panredraw();
            update_life();
            pokolenie++;
            label2.Text = "Поколение " + Convert.ToString(pokolenie);
        }
        //маленькая функция для синхронизации 2 массивов…может быть я туплю, ноиначе никак не сделать, иначе логика работы нарушиться
        public void update_life()
        {
            for (int j = 0; j < 500; j += 10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    life[i, j] = coords[i, j];
                }
            }
        }
        //для сохранения настроек по нажатию кнопки «начать» (так как нужно сохранение именно начальной конфигурации а не конечной)
        public void save_sett()
        {
            StreamWriter sw = f.CreateText();
            for (int j = 0; j < 500; j += 10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    sw.Write(coords[i, j]);
                }
            }
            sw.Close();
        }
        //для загрузки настроек
        public void load_sett()
        {
            openFileDialog1.ShowDialog();
            openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
            string n = openFileDialog1.FileName;
            StreamReader sr = File.OpenText(n);
            string input = sr.ReadToEnd();
            int tmp=0;
            for (int j = 0; j < 500; j += 10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    coords[i, j] = Convert.ToByte(input.Substring(tmp, 1));
                    tmp++;
                }
            }
            sr.Close();
            update_life();
            panredraw();
        }

        void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string n = openFileDialog1.FileName;
            StreamReader sr = File.OpenText(n);
            string input = sr.ReadToEnd();
            int tmp = 0;
            for (int j = 0; j < 500; j += 10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    coords[i, j] = Convert.ToByte(input.Substring(tmp, 1));
                    tmp++;
                }
            }
            sr.Close();
            update_life();
            panredraw();
        }
        //полное сохранение настроек по нажатию кнопки «сохранить»
        public void save_sett_full()
        {
            saveFileDialog1.ShowDialog();
            string n = saveFileDialog1.FileName;
            try
            {
                f.MoveTo(n);
            }
            catch (Exception ex)
            {
                f.Replace(n, n + ".bak");
            }
        }
        //запуск
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(numericUpDown1.Value);
            life_engine();
            button3.Visible = true;
            button1.Visible = false;
            button4.Enabled = false;
            numericUpDown1.Enabled = false;
            save_sett();
            button5.Enabled = false;
            button6.Enabled = false;
        }
        //остановить
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            button4.Enabled = true;
            numericUpDown1.Enabled = true;
            button5.Enabled = true;
        }
        //продолжить
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(numericUpDown1.Value);
            life_engine();
            button4.Enabled = false;
            numericUpDown1.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        }
        //очистить
        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button1.Visible = true;
            for (int j = 0; j < 500; j += 10)
            {
                for (int i = 0; i < 500; i += 10)
                {
                    life[i, j] = 0;
                    coords[i, j] = 0;
                }
            }
            Graphics g = Graphics.FromHwnd(panel1.Handle);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, 500, 500);
            for (int i = 0; i <= 500; i += 10)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Gray)), i, 0, i, 500);
            }
            for (int i = 0; i <= 500; i += 10)
            {
                g.DrawLine(new Pen(new SolidBrush(Color.Gray)), 0, i, 500, i);
            }
            button5.Enabled = false;
            button6.Enabled = true;
            pokolenie = 0;
            label2.Text = "Поколение " + Convert.ToString(pokolenie);
        }
        //загрузка
        private void button6_Click(object sender, EventArgs e)
        {
            load_sett();
        }
        //сохранить
        private void button5_Click(object sender, EventArgs e)
        {
            save_sett_full();
        }

        public void habrahabr()
        {
            throw new System.NotImplementedException();
        }
    }
}
