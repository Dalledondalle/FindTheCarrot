using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FindTheCarrot
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        GameManager GameManager;
        int CarrotLocation = 0;
        Image Carrot;
        public Game()
        {
            InitializeComponent();
            GameManager = new GameManager();
            GameManager.State = GameManager.GameState.Choosing;
            GameText.Text = "Chose who is who\nand then player 2\nlooks away";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                switch (GameManager.State)
                {
                    case GameManager.GameState.Choosing:
                        SetupCarrot();
                        GameManager.State = GameManager.GameState.Looking;
                        GameText.Text = "You, Player 1,\nremember where the carrot is";
                        Carrot.Visibility = Visibility.Visible;
                        return;

                    case GameManager.GameState.Looking:
                        GameManager.State = GameManager.GameState.Deciding;
                        GameText.Text = "Now Player 2 can look back.\nPlayer 2, Guess who has the carrot!";
                        Carrot.Visibility = Visibility.Hidden;
                        return;

                    case GameManager.GameState.Deciding:
                        GameManager.State = GameManager.GameState.Finished;
                        GameText.Text = $"Player {CarrotLocation.ToString()} have the carrot";
                        Carrot.Visibility = Visibility.Visible;
                        return;

                    case GameManager.GameState.Finished:
                        GameManager.State = GameManager.GameState.Choosing;
                        GameText.Text = "Continue to play again";
                        Carrot.Visibility = Visibility.Hidden;
                        SetupCarrot();
                        return;
                }
            }
            if (e.Key == Key.Escape)
            {
                Window gameWindow = new MainWindow();
                gameWindow.Show();
                this.Close();
            }
        }
        private void SetupCarrot()
        {
            Random rnd = new Random();
            if (rnd.Next(2) == 0)
            {
                CarrotLocation = 1;
                Carrot = CarrotOne;
            }
            else
            {
                CarrotLocation = 2;
                Carrot = CarrotTwo;
            }
        }
    }
}
