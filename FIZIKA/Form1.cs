using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIZIKA
{
    public partial class Form1 : Form
    {
        private const double Gravity = 9.81; // Ускорение свободного падения (м/с²)
        private const double ThreadLength = 100.0; // Длина нити (м)
        private const double TimeStep = 0.01; // Шаг по времени (с)

        private double theta = Math.PI / 2; // Угол отклонения (радиан)
        private double omega = 0.0; // Угловая скорость (радиан/с)

        private Pen pen;
        private Bitmap bitmap;
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            // Создаем перо для рисования
            pen = new Pen(Color.Black, 1);
            // Создаем растровое изображение и получаем его графический контекст
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            graphics = Graphics.FromImage(bitmap);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // ...

            // Запускаем таймер
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            graphics = Graphics.FromImage(bitmap);
            // Вычисляем угловое ускорение
            double alpha = -Gravity * Math.Sin(theta) * 100 / ThreadLength;

            // Вычисляем новую угловую скорость и угол отклонения
            omega += alpha * TimeStep;
            theta += omega * TimeStep;

            // Отрисовываем маятник в растровое изображение
            DrawPendulum();

            // Копируем растровое изображение на экран
            graphics = CreateGraphics();
            graphics.DrawImage(bitmap, 0, 0);
        }

        private void DrawPendulum()
        {
            // Очищаем область рисования в растровом изображении
            graphics.Clear(BackColor);

            // Вычисляем координаты центра подвеса и конца маятника
            int cx = ClientSize.Width / 2;
            int cy = ClientSize.Height / 4;
            int x = (int)(cx + ThreadLength * Math.Sin(theta));
            int y = (int)(cy + ThreadLength * Math.Cos(theta));

            // Рисуем нить и маятник
            graphics.DrawLine(pen, cx, cy, x, y);
            graphics.FillEllipse(new SolidBrush(Color.Red), x - 5, y - 5, 10, 10);
        }
    }
}
