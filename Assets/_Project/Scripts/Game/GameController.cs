using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameController
{
    private static GameSettings _gameSettings;
    private static int _currentLevelNumber;

    private static void SetNextLevel(int level)
    {
        if (level < 0 || level >= _gameSettings.colorList.Count)
        {
            _currentLevelNumber = 0;
        }
        else
        {
            _currentLevelNumber = level;
        }
    }

    public static void StartNextLevel()
    {
        int nextLevel = _currentLevelNumber + 1;
        SetNextLevel(nextLevel);
        RestartLevel();
    }

    public static void GameInit(GameSettings list)
    {
        if (_gameSettings == null)
        {
            _gameSettings = list;
            _currentLevelNumber = 0;
        }
    }

    public static Color GetLevelColor() => _gameSettings.colorList[_currentLevelNumber];

    public static void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}