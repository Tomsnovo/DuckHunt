using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DuckHuntGame
{
    public partial class MainWindow : Window
    {
        private int score;
        private Random random;
        private DispatcherTimer timer;
        public int winw;
        public int winh;

        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
            score = 0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            Loaded += Window_Loaded;
        }

        private void GenerateTarget()
        {
            int size = random.Next(40, 100);            
            int x = random.Next(0,winw - size);
            int y = random.Next(0, winh - size);
            Ellipse target = new Ellipse();
            target.Fill = Brushes.Red;
            target.Width = size;
            target.Height = size;
            Canvas.SetLeft(target, x);
            Canvas.SetTop(target, y);
            canvas.Children.Add(target);
        }       

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(canvas);
            UIElement target = canvas.InputHitTest(mousePos) as UIElement;

            if (target != null && target is Ellipse)
            {
                canvas.Children.Remove(target);
                score++;
                scoreText.Text = "Score: " + score;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GenerateTarget();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            winh = (int)canvas.ActualHeight;
            winw = (int)canvas.ActualWidth;
        }
    }
}