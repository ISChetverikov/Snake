﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using SnakeOnlineClient;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace Snake
{
    class ObservableRectangle : Rectangle
    {
        public Brush Color { get; set; }
    }
    
    class MainWindowViewModel : INotifyPropertyChanged
    {
        const int width = 800;
        const int height = 550;
        int _step = 0;
        int _roundNumber = 0;
        
        public int _width = 0;
        ObservableCollection<ObservableRectangle> _rectangles;
        ObservableCollection<ObservableRectangle> _wallsRectangles;
        Model _model = new Model();

        int _height = 0;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand DirectionCommand { get; set; }
        public ICommand ShowLoginWindow { get; set; }

        public ObservableCollection<ObservableRectangle> Rectangles
        {
            get
            {
                return _rectangles;
            }
            set
            {
                _rectangles = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Rectangles)));
            }
        }

        public int CellWidth
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CellWidth)));
            }
        }

        public int CellHeight
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CellHeight)));
            }
        }
        
        public MainWindowViewModel()
        {
            
            _rectangles = new ObservableCollection<ObservableRectangle>();
            DirectionCommand = new SendDirectionRequest(_model);
            ShowLoginWindow = new ShowLoginWindow();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(GetGameState);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            dispatcherTimer.Start();
            
        }

        private void GetGameState(object sender, EventArgs e)
        {
            
            var g = _model.GetGameStateResponseAsync().ContinueWith((antecedent) =>
            {
                
                var gameState = antecedent.Result;
                CellWidth = gameState.TimeUntilNextTurnMilliseconds;
                
                if (gameState.RoundNumber != _roundNumber)
                {
                    _wallsRectangles = new ObservableCollection<ObservableRectangle>();

                    var widthSteps = gameState.GameBoardSize.Width + 2;
                    var heightSteps = gameState.GameBoardSize.Height + 2;
                    _step = Math.Min(width / widthSteps, height / heightSteps);
                    var brush = Brushes.SteelBlue;
                    _wallsRectangles.Add(new ObservableRectangle
                    {
                        Width = _step,
                        Height = heightSteps * _step,
                        X = 0,
                        Y = 0,
                        Color = brush
                    });

                    _wallsRectangles.Add(new ObservableRectangle
                    {
                        Width = widthSteps * _step,
                        Height = _step,
                        X = 0,
                        Y = 0,
                        Color = brush
                    });

                    _wallsRectangles.Add(new ObservableRectangle
                    {
                        Width = _step,
                        Height = heightSteps * _step,
                        X = (widthSteps - 1) * _step,
                        Y = 0,
                        Color = brush
                    });

                    _wallsRectangles.Add(new ObservableRectangle
                    {
                        Width = widthSteps * _step,
                        Height = _step,
                        X = 0,
                        Y = (heightSteps - 1) * _step,
                        Color = brush
                    });


                    foreach (var wall in gameState.Walls)
                    {
                        _wallsRectangles.Add(new ObservableRectangle
                        {
                            Width = wall.Width * _step,
                            Height = wall.Height * _step,
                            X = (wall.X + 1) * _step,
                            Y = (wall.Y + 1) * _step,
                            Color = brush
                        });
                    }


                }
                var r = new ObservableCollection<ObservableRectangle>(_wallsRectangles);

                foreach (PlayerState player in gameState.Players)
                {
                    foreach (var block in player.Snake ?? new List<Point>())
                    {
                        var brush = Brushes.Red;
                        if (player.IsSpawnProtected)
                            brush = Brushes.White;
                        if (player.Name == _model.Name)
                            brush = Brushes.Aqua;

                        r.Add(new ObservableRectangle
                        {
                            Width = _step,
                            Height = _step,
                            X = (block.X + 1) * _step,
                            Y = (block.Y + 1) * _step,
                            Color = brush
                        });
                    }
                }

                foreach (var food in gameState.Food)
                {
                    r.Add(new ObservableRectangle
                    {
                        Width = _step,
                        Height = _step,
                        X = (food.X + 1) * _step,
                        Y = (food.Y + 1) * _step,
                        Color = Brushes.Chocolate
                    });
                }

                Rectangles = r;

                return;
            });
        }
        
        
    }
}
