﻿using System;
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
                if (StepCadr == 0) 
                {
                    StepCadr = 1;
                }
                else
                {
                    StepCadr = 0;
                }
                canvas.Children.Clear();
                for (int iPoint = MainWindow.mainWindow.ViewModelGames.SnakesPlayers.Points.Count - 1; iPoint >= 0; iPoint--)
                {
                    Snakes.Point NextSnakePoint = MainWindow.mainWindow.ViewModelGames.SnakesPlayers.Points[iPoint - 1];
                    if (SnakePoint.X > NextSnakePoint.X || SnakePoint.X < NextSnakePoint.X)
                    {
                        if (iPoint % 2 == 0)
                        {
                            if (StepCadr % 2 == 0)
                            {
                                SnakePoint.Y -= 1;
                            }
                            else
                            {
                                SnakePoint.Y += 1;
                            }
                        }
                        else
                        {
                            if (StepCadr % 2 == 0)
                            {
                                SnakePoint.Y += 1;
                            }
                            else
                            {
                                SnakePoint.Y -= 1;
                            }
                        }
                    }
                    else if (SnakePoint.Y > NextSnakePoint.Y || SnakePoint.Y < NextSnakePoint.Y)
                    {
                        if (iPoint % 2 == 0)
                        {
                            if (StepCadr % 2 == 0)
                            {
                                SnakePoint.X -= 1;
                            }
                            else
                            {
                                SnakePoint.X += 1;
                            }
                        }
                        else
                        {
                            if (StepCadr % 2 == 0)
                            {
                                SnakePoint.X += 1;
                            }
                            else
                            {
                                SnakePoint.X -= 1;
                            }
                        }
                    }
                }
            });
        }
    }
}
