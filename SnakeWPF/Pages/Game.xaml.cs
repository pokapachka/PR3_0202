﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common;

namespace SnakeWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public int StepCadr = 0;
        public Game()
        {
            InitializeComponent();
        }
        public void CreateUI()
        {
            Dispatcher.Invoke(() =>
            {
                if (StepCadr == 0) StepCadr = 1;
                else StepCadr = 0;
                canvas.Children.Clear();
                for (int iPoint = MainWindow.mainWindow.ViewModelGames.SnakesPlayers.Points.Count - 1; iPoint >= 0; iPoint--)
                {
                    Snakes.Point SnakePoint = MainWindow.mainWindow.ViewModelGames.SnakesPlayers.Points[iPoint];

                    if (iPoint != 0)
                    {
                        Snakes.Point NextSnakePoint = MainWindow.mainWindow.ViewModelGames.SnakesPlayers.Points[iPoint - 1];
                        if (SnakePoint.X > NextSnakePoint.X || SnakePoint.X < NextSnakePoint.X)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0)
                                    SnakePoint.Y -= 1;
                                else
                                    SnakePoint.Y += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0)
                                    SnakePoint.Y += 1;
                                else
                                    SnakePoint.Y -= 1;
                            }
                        }
                        else if (SnakePoint.Y > NextSnakePoint.Y || SnakePoint.Y < NextSnakePoint.Y)
                        {
                            if (iPoint % 2 == 0)
                            {
                                if (StepCadr % 2 == 0)
                                    SnakePoint.X -= 1;
                                else
                                    SnakePoint.X += 1;
                            }
                            else
                            {
                                if (StepCadr % 2 == 0)
                                    SnakePoint.X += 1;
                                else
                                    SnakePoint.X -= 1;
                            }
                        }
                    }
                    Brush Color;

                    if (iPoint == 0)
                    {
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 127, 14));
                    }
                    else
                    {
                        Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 198, 19));
                    }

                    Ellipse ellipse = new Ellipse()
                    {
                        Width = 20,
                        Height = 20,
                        Margin = new Thickness(SnakePoint.X - 10, SnakePoint.Y - 10, 0, 0),
                        Fill = Color,
                        Stroke = Brushes.Black
                    };
                    canvas.Children.Add(ellipse);
                }

                if (MainWindow.mainWindow.AllViewModelGames != null)
                {
                    for (int i = 0; i < MainWindow.mainWindow.AllViewModelGames.Count; i++)
                    {
                        for (int iPoint = MainWindow.mainWindow.AllViewModelGames[i].SnakesPlayers.Points.Count - 1; iPoint >= 0; iPoint--)
                        {
                            Snakes.Point SnakePoint = MainWindow.mainWindow.AllViewModelGames[i].SnakesPlayers.Points[iPoint];

                            if (iPoint != 0)
                            {
                                Snakes.Point NextSnakePoint = MainWindow.mainWindow.AllViewModelGames[i].SnakesPlayers.Points[iPoint - 1];
                                if (SnakePoint.X > NextSnakePoint.X || SnakePoint.X < NextSnakePoint.X)
                                {
                                    if (iPoint % 2 == 0)
                                    {
                                        if (StepCadr % 2 == 0)
                                            SnakePoint.Y -= 1;
                                        else
                                            SnakePoint.Y += 1;
                                    }
                                    else
                                    {
                                        if (StepCadr % 2 == 0)
                                            SnakePoint.Y += 1;
                                        else
                                            SnakePoint.Y -= 1;
                                    }
                                }
                                else if (SnakePoint.Y > NextSnakePoint.Y || SnakePoint.Y < NextSnakePoint.Y)
                                {
                                    if (iPoint % 2 == 0)
                                    {
                                        if (StepCadr % 2 == 0)
                                            SnakePoint.X -= 1;
                                        else
                                            SnakePoint.X += 1;
                                    }
                                    else
                                    {
                                        if (StepCadr % 2 == 0)
                                            SnakePoint.X += 1;
                                        else
                                            SnakePoint.X -= 1;
                                    }
                                }
                            }
                            Brush Color;

                            if (iPoint == 0)
                            {
                                Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 127, 14));
                            }
                            else
                            {
                                Color = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 198, 19));
                            }

                            Ellipse ellipse = new Ellipse()
                            {
                                Width = 20,
                                Height = 20,
                                Margin = new Thickness(SnakePoint.X - 10, SnakePoint.Y - 10, 0, 0),
                                Fill = Color,
                                Stroke = Brushes.Black
                            };
                            canvas.Children.Add(ellipse);
                        }
                    }
                }
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Image/Apple.png"));
                Ellipse points = new Ellipse()
                {
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(MainWindow.mainWindow.ViewModelGames.Points.X - 20, MainWindow.mainWindow.ViewModelGames.Points.Y - 20, 0, 0),
                    Fill = myBrush
                };
                canvas.Children.Add(points);
            });
        }
    }
}
