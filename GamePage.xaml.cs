using MathGame.Maui.Data;
using MathGame.Maui.Models;

namespace MathGame.Maui;

public partial class GamePage : ContentPage
{
    public string GameType { get; set; }
    int _firstNumber = 0;
    int _secondNumber = 0;
    private int _score = 0;
    const int TotalQuestions = 2;
    int _gamesLeft = TotalQuestions;
    public GamePage(string gameType)
	{
        InitializeComponent();
        GameType = gameType;
        BindingContext = this;

        CreateNewQuestion();
    }

    private void CreateNewQuestion()
    {
        var gameOp = GameType switch
        {
            "Addition" => "+",
            "Subtraction" => "-",
            "Multiplication" => "*",
            "Division" => "/",
            _ => ""
        };

        var random = new Random();

        _firstNumber = GameType != "Division" ? random.Next(1, 9) : random.Next(1, 99);
        _secondNumber = GameType != "Division" ? random.Next(1, 9) : random.Next(1, 99);

        if (GameType == "Division")
            while (_firstNumber < _secondNumber || _firstNumber % _secondNumber != 0)
            {
                _firstNumber = random.Next(1, 99);
                _secondNumber = random.Next(1, 99);
            }

        QuestionLabel.Text = $"{_firstNumber} {gameOp} {_secondNumber}";
    }

    
    private void OnAnswerSubmitted(object sender, EventArgs e)
    {
        var answer = int.Parse(AnswerEntry.Text);
        var isCorrect = false;
        

        switch (GameType)
        {
            case "Addition":
                isCorrect = answer == _firstNumber + _secondNumber;
                break;
            case "Multiplication":
                isCorrect = answer == _firstNumber * _secondNumber;
                break;
            case "Subtraction":
                isCorrect = answer == _firstNumber - _secondNumber;
                break;
            case "Division":
                isCorrect = answer == _firstNumber / _secondNumber;
                break;
        }
        ProcessAnswer(isCorrect);
        _gamesLeft--;
        AnswerEntry.Text = "";

        if (_gamesLeft > 0)
            CreateNewQuestion();
        else
            GameOver();
    }

    private void GameOver()
    {
        GameOperation gameOperation = GameType switch
        {
            "Addition" => GameOperation.Addition,
            "Subtraction" => GameOperation.Subtraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division
        };

        QuestionsArea.IsVisible = false;
        BackToMenuBtn.IsVisible = true;
        GameOverLabel.Text = $"Game Over! You got {_score} out of {TotalQuestions} right";
        
        App.GameRepository.CreateTable();
        App.GameRepository.Add(new Game
        {
            DatePlayed = DateTime.Now,
            Type = gameOperation,
            Score = _score
        });
    }

    private void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect)
            _score++;

        AnswerLabel.Text = isCorrect ? "Correct" : "Incorrect";

    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}
