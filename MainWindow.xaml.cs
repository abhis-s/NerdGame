using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;
using System.Xml.Serialization;

namespace NerdGame
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int timeLeft;
        private Random random = new Random();
        private byte targetValue;
        private SoundPlayer tickSound = new SoundPlayer("Sounds/tick.wav");
        private SoundPlayer winSound = new SoundPlayer("Sounds/won.wav");
        private SoundPlayer loseSound = new SoundPlayer("Sounds/lost.wav");


        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer((DispatcherPriority.Render));
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            TimeLabel.Content = "Time: 10";
            BinaryInput.ValueChanged += BinaryInput_ValueChanged;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            tickSound.Play();
            timeLeft--;
            TimeLabel.Content = $"Time: {timeLeft}";

            if (timeLeft <= 0)
            {
                timer.Stop();
                BinaryInput.IsEnabled = false;
                GameStateLabel.Content = "Looser 💀";
                loseSound.Play();
            }
        }

        private void BinaryInput_ValueChanged(byte newValue)
        {
            if (timeLeft > 0 && newValue == targetValue)
            {
                timer.Stop();
                GameStateLabel.Content = "Winner 🎉";
                BinaryInput.IsEnabled = false;
                winSound.Play();
            }
        }


        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            timeLeft = 10;
            GameStateLabel.Content = "Game running!";
            TimeLabel.Content = $"Time: {timeLeft}";

            BinaryInput.Mode = HexRadio.IsChecked == true ? DisplayMode.Hex : DisplayMode.Decimal;
            BinaryInput.ClearAllBits();
            BinaryInput.IsEnabled = true;

            targetValue = (byte)random.Next(0, 256);
            UpdateTargetDisplay();


            timer.Start();
        }
        private void UpdateTargetDisplay()
        {
            if (HexRadio.IsChecked == true)
            {
                TargetValueLabel.Content = $"0x{targetValue.ToString("X2")}";
            }
            else
            {
                TargetValueLabel.Content = targetValue.ToString();
            }
        }
        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ThemeSelector.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selected == "Dark")
                SetTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
            else
                SetTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
        }

        private void SetTheme(Uri resourceUri)
        {
            var existing = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Themes/"));
            if (existing != null)
                Application.Current.Resources.MergedDictionaries.Remove(existing);

            var newTheme = new ResourceDictionary() { Source = resourceUri };
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }


    }
}
