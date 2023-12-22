using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    
    public class SnakeGame
    {
        private const int Width = 30;
        private const int Height = 15; 
        private const char SnakeSymbol = 'O'; 
        private const char FoodSymbol = '+'; 
        private const char WallSymbol = '*'; 

        private bool _gameOver;
        private int _score; 
        private int _snakeX; 
        private int _snakeY; 
        private int _foodX; 
        private int _foodY; 
        private int _directionX; 
        private int _directionY; 
        private List<int> _snakeBodyX; 
        private List<int> _snakeBodyY; 

      
        public void Start()
        {
            InitializeGame();
            DrawGame();

            while (!_gameOver)
            {
                if (Console.KeyAvailable)
                {
                    ProcessInput(Console.ReadKey(true).Key);
                }

                MoveSnake();
                CheckCollision();
                DrawGame();

                Thread.Sleep(130); 
            }

            Console.WriteLine("Game Over! Your score: " + _score);
        }

      
        private void InitializeGame()
        {
            _gameOver = false;
            _score = 0;
            _snakeX = Width / 2;
            _snakeY = Height / 2;
            _foodX = new Random().Next(1, Width - 1);
            _foodY = new Random().Next(1, Height - 1);
            _directionX = 0;
            _directionY = 0;
            _snakeBodyX = new List<int>();
            _snakeBodyY = new List<int>();
        }

       
        private void DrawGame()
        {
            Console.Clear();

            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(WallSymbol);
                Console.SetCursorPosition(i, Height - 1);
                Console.Write(WallSymbol);
            }
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(WallSymbol);
                Console.SetCursorPosition(Width - 1, i);
                Console.Write(WallSymbol);
            }

            
            Console.SetCursorPosition(_snakeX, _snakeY);
            Console.Write(SnakeSymbol);

            
            Console.SetCursorPosition(_foodX, _foodY);
            Console.Write(FoodSymbol);

           
            Console.SetCursorPosition(0, Height);
            Console.Write("Score: " + _score);
        }

        
        private void ProcessInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    _directionX = 0;
                    _directionY = -1;
                    break;
                case ConsoleKey.A:
                    _directionX = -1;
                    _directionY = 0;
                    break;
                case ConsoleKey.S:
                    _directionX = 0;
                    _directionY = 1;
                    break;
                case ConsoleKey.D:
                    _directionX = 1;
                    _directionY = 0;
                    break;
            }
        }

       
        private void MoveSnake()
        {
            
            _snakeBodyX.Insert(0, _snakeX);
            _snakeBodyY.Insert(0, _snakeY);

            
            _snakeX += _directionX;
            _snakeY += _directionY;

           
            if (_snakeBodyX.Count > _score)
            {
                _snakeBodyX.RemoveAt(_score);
                _snakeBodyY.RemoveAt(_score);
            }
        }
     


        private void CheckCollision()
        {
            
            if (_snakeX == 0 || _snakeX == Width - 1 || _snakeY == 0 || _snakeY == Height - 1)
            {
                _gameOver = true;
                return;
            }

            
            if (_snakeX == _foodX && _snakeY == _foodY)
            {
                _score++;
                _foodX = new Random().Next(1, Width - 1);
                _foodY = new Random().Next(1, Height - 1);
            }

            
            for (int i = 0; i < _snakeBodyX.Count; i++)
            {
                if (_snakeX == _snakeBodyX[i] && _snakeY == _snakeBodyY[i])
                {
                    _gameOver = true;
                    return;
                }
            }
        }
    }
  

    public class Program
    {
        public static void Main()
        {
            var game = new SnakeGame();
            game.Start();
        }
    }
}