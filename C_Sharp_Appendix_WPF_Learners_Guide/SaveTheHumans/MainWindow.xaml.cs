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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SaveTheHumans
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        private DispatcherTimer _enemyTimer = new DispatcherTimer();
        private DispatcherTimer _targetTimer = new DispatcherTimer();
        private bool _isHumanCaptured = false;

        public MainWindow()
        {
            InitializeComponent();

            _enemyTimer.Tick += _enemyTimer_Tick;
            _enemyTimer.Interval = TimeSpan.FromSeconds(2);

            _targetTimer.Tick += _targetTimer_Tick;
            _targetTimer.Interval = TimeSpan.FromSeconds(3);
        }

        private void _targetTimer_Tick(object sender, EventArgs e)
        {
            progressBar.Value += 1;
            if(progressBar.Value >= progressBar.Maximum)
            {
                EndGame();
            }
        }

        private void _enemyTimer_Tick(object sender, EventArgs e)
        {
            AddEnemy();
        }

        private void playArea_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isHumanCaptured)
            {
                EndGame();
            }
        }

        private void target_MouseEnter(object sender, MouseEventArgs e)
        {
            if(_targetTimer.IsEnabled && _isHumanCaptured)
            {
                progressBar.Value = 0;
                Canvas.SetLeft(target, _random.Next(100, (int)playArea.ActualWidth - 100));
                Canvas.SetTop(target, _random.Next(100, (int)playArea.ActualHeight - 100));

                Canvas.SetLeft(human, _random.Next(100, (int)playArea.ActualWidth - 100));
                Canvas.SetTop(human, _random.Next(100, (int)playArea.ActualHeight - 100));

                _isHumanCaptured = false;
                human.IsHitTestVisible = true;
            }
        }

        private void playArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isHumanCaptured)
            {
                Point pointerPosition = e.GetPosition(null);
                Point relativePosition = grid.TransformToVisual(playArea).Transform(pointerPosition);
                if((Math.Abs(relativePosition.X - Canvas.GetLeft(human)) > human.ActualWidth * 3) 
                || (Math.Abs(relativePosition.Y - Canvas.GetTop(human)) > human.ActualHeight * 3))
                {
                    _isHumanCaptured = false;
                    human.IsHitTestVisible = true;
                }
                else
                {
                    Canvas.SetLeft(human, relativePosition.X - human.ActualWidth / 2);
                    Canvas.SetTop(human, relativePosition.Y - human.ActualHeight / 2);
                }
            }
        }

        private void human_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_enemyTimer.IsEnabled)
            {
                _isHumanCaptured = true;
                human.IsHitTestVisible = false;
            }
        }

        private void Enemy_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_isHumanCaptured)
            {
                EndGame();
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void AddEnemy()
        {
            ContentControl enemy = new ContentControl();
            enemy.Template = Resources["EnemyTemplate"] as ControlTemplate;
            AnimateEnemy(enemy, 0, playArea.ActualWidth - 100, "(Canvas.Left)");
            AnimateEnemy(enemy, _random.Next((int)playArea.ActualHeight - 100), _random.Next((int)playArea.ActualHeight - 100), "(Canvas.Top)");
            playArea.Children.Add(enemy);

            enemy.MouseEnter += Enemy_MouseEnter;
        }

        private void EndGame()
        {
            if (!playArea.Children.Contains(gameOverText))
            {
                _enemyTimer.Stop();
                _targetTimer.Stop();
                _isHumanCaptured = false;
                startButton.Visibility = Visibility.Visible;
                playArea.Children.Add(gameOverText);
            }
        }

        private void StartGame()
        {
            human.IsHitTestVisible = true;
            _isHumanCaptured = false;
            progressBar.Value = 0;
            startButton.Visibility = Visibility.Collapsed;
            playArea.Children.Clear();
            playArea.Children.Add(target);
            playArea.Children.Add(human);
            _enemyTimer.Start();
            _targetTimer.Start();
        }

        private void AnimateEnemy(ContentControl enemy, double from, double to, string propertyToAnimate)
        {
            Storyboard storyboard = new Storyboard() { AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever };
            DoubleAnimation animation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromSeconds(_random.Next(4, 6)))
            };

            Storyboard.SetTarget(animation, enemy);
            Storyboard.SetTargetProperty(animation, new PropertyPath(propertyToAnimate));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
