using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using x = System.Windows.Threading;

namespace Relogio24h
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            x.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            //dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            dispatcherTimer.Start();

            size = canvas.Width / 2;

            hipotenusaHora = size * 0.45;
            hipotenusaMinuto = size * 0.85;
            hipotenusaSegundo = size * 0.85;

            for (int i = 0; i < 24; i++)
            {
                mostrahoras(i);
            }

            for (int i = 0; i < 60; i++)
            {
                mostraminutos(i);
            }

            PonteiroHoras.X1 = size; //centro x horas
            PonteiroHoras.Y1 = size; //centro y horas

            PonteiroMinutos.X1 = size; //centro x minutos
            PonteiroMinutos.Y1 = size; //centro y minutos

            PonteiroSegundos.X1 = size; //centro segundos
            PonteiroSegundos.Y1 = size; // centro segundos

            //setTime(0, 0, 0);
            setNow();
        }
        private double size;        
        private double hipotenusaHora;
        private double hipotenusaMinuto;
        private double hipotenusaSegundo;

        private int hora = 0;
        private int minuto = 0;
        private int segundo = 0;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //segundo+=10;
            //if (segundo > 59)
            //{
            //    segundo = 0;
            //    minuto++;
            //    if (minuto > 59)
            //    {
            //        minuto = 0;
            //        hora++;
            //        if (hora > 23) hora = 0;
            //    }
            //}
            //
            //setTime(hora, minuto, segundo);

            setNow();
        }

        private void setNow()
        {
            var now = DateTime.Now;
            setTime(now.Hour, now.Minute, now.Second);
        }

        private void setTime(int hora, int minuto, int segundo)
        {
            setsegundos(segundo);
            sethoras(hora);
            setminutos(minuto);
        }

        private void mostrahoras(int digito)
        {
            int angulo = digito * (360 / 24);
            var ponto = getPoint(angulo, size*1);

            var numero = new Label();

            numero.FontSize = 24;
            numero.FontWeight = FontWeights.Bold;
            numero.Width = numero.Height = 40;            
            numero.VerticalContentAlignment = VerticalAlignment.Center;
            numero.HorizontalContentAlignment = HorizontalAlignment.Center;

            if (digito == 0)
            {
                numero.Content = "24";
            }
            else
            {
                numero.Content = digito.ToString();
            }
            
            var x = (ponto.X + size) - numero.Width / 2;
            var y = (size - ponto.Y) - numero.Height / 2;

            Canvas.SetLeft(numero, x);
            Canvas.SetTop(numero, y);

            canvas.Children.Add(numero);
        }

        private void mostraminutos(int digito)
        {
            int angulo = digito * (360 / 60);
            var ponto = getPoint(angulo, size*0.9);

            var numero = new Label();

            numero.FontSize = 16;
            numero.FontWeight = FontWeights.Regular;
            numero.Width = numero.Height = 40;
            numero.VerticalContentAlignment = VerticalAlignment.Center;
            numero.HorizontalContentAlignment = HorizontalAlignment.Center;

            if (digito == 0)
            {
                numero.Content = "0";
            }
            else
            {
                numero.Content = digito.ToString();
            }

            var x = (ponto.X + size) - numero.Width / 2;
            var y = (size - ponto.Y) - numero.Height / 2;

            Canvas.SetLeft(numero, x);
            Canvas.SetTop(numero, y);

            canvas.Children.Add(numero);
        }

        private void sethoras(int hora)
        {
            int angulo = hora * (360/24);
            var ponto = getPoint(angulo, hipotenusaHora);
            PonteiroHoras.X2 = ponto.X + size;
            PonteiroHoras.Y2 = size - ponto.Y;
        }

        private void setminutos(int minuto)
        {
            int angulo = minuto * (360/60);
            var ponto = getPoint(angulo, hipotenusaMinuto);
            PonteiroMinutos.X2 = ponto.X + size;
            PonteiroMinutos.Y2 = size - ponto.Y;
        }

        private void setsegundos(int segundo)
        {
            int angulo = segundo * (360 / 60);
            var ponto = getPoint(angulo, hipotenusaSegundo);
            PonteiroSegundos.X2 = ponto.X + size;
            PonteiroSegundos.Y2 = size - ponto.Y;
        }

        private Point getPoint(int angulo, double hipotenusa)
        {
            double cateto1, cateto2;
            cateto1 = Math.Sin(angulo * Math.PI / 180) * hipotenusa;
            cateto2 = Math.Cos(angulo * Math.PI / 180) * hipotenusa;
            return new Point(cateto1, cateto2);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
