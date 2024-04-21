using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Wpf.Ui.Controls;

namespace AcrylicFixBackground_WPF
{
    public partial class MainWindow : FluentWindow
    {
        public Color MWColorBgroundActive = Color.FromRgb(0, 0, 0); //цвет фона при активном состоянии окна
        public Color MWColorBgroundInactive = Color.FromRgb(255, 192, 203); //цвет фона при неактивном состоянии окна
        public double TransparencyActive = 0.0; //прозрачность фона при активном состоянии окна
        public double TransparencyInactive = 1; //прозрачность фона при неактивном состоянии окна
        public TimeSpan ColorSwitchTimeActive = TimeSpan.FromMilliseconds(150); //время смены цвета на активное состояния окна
        public TimeSpan ColorSwitchTimeInactive = TimeSpan.FromMilliseconds(270); //время смены цвета на неактивное состояние окна
        public MainWindow()
        {
            InitializeComponent();
            Activate(); // На тот случай если окно при запуске будет неактивным 
        }
        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            try{ if (e.ChangedButton == MouseButton.Left) { this.DragMove(); } }
            catch{}
        } //Для перетаскивания окна

        private void ChangeBackgroundColor(Brush targetBrush, TimeSpan duration)
        {
            if (targetBrush is SolidColorBrush solidColorBrush)
            {
                ColorAnimation colorAnimation = new ColorAnimation();
                colorAnimation.To = solidColorBrush.Color;
                colorAnimation.Duration = duration;

                Background = new SolidColorBrush((Color)Background.GetValue(SolidColorBrush.ColorProperty));
                Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            }
        } //Плавная смена цвета фона окна на входяшие параметры

        private void ChangeBackgroundColor(Color targetColor, TimeSpan duration)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = targetColor;
            colorAnimation.Duration = duration;
            SolidColorBrush brush = new SolidColorBrush((Color)Background.GetValue(SolidColorBrush.ColorProperty));
            brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            Background = brush;
        } //На случай если цвет в Color, а не Brush [сейчас неиспользуется]

        public static Brush ChangeOpacity(Color color, double opacity)
        {
            color.A = (byte)(opacity * 255);
            return new SolidColorBrush(color);
        } //Изменение прозрачности цвета на необходимую
        public static Brush ChangeOpacity(Brush brush, double opacity)
        {
            if (brush is SolidColorBrush solidColorBrush)
            {
                return new SolidColorBrush(Color.FromArgb((byte)(opacity * 255), solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B));
            }
            return brush;
        } //На случай если цвет в Brush, а не Color [сейчас неиспользуется]

        private void FluentWindow_Activated(object sender, EventArgs e)
        {
            if (sender is FluentWindow)
            {
                ChangeBackgroundColor(ChangeOpacity(MWColorBgroundActive, TransparencyActive), ColorSwitchTimeActive);
            }
        }

        private void FluentWindow_Deactivated(object sender, EventArgs e)
        {
            if (sender is FluentWindow)
            {
                ChangeBackgroundColor(ChangeOpacity(MWColorBgroundInactive, TransparencyInactive), ColorSwitchTimeInactive);
            }
        }
    }
}