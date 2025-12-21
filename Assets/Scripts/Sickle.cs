using SOContent.SickleContent;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    [SerializeField] private SickleConfig _config;

    private int _currentLevel = 1;

    public int CurrentLevel => _currentLevel;
    public int MaxLevel => _config.Levels.Length;

    public float CutRadius => _config.Levels[_currentLevel - 1].CutRadius;
    public float SwingSpeed => _config.Levels[_currentLevel - 1].SwingSpeed;

    public bool CanLevelUp()
    {
        return _currentLevel < MaxLevel;
    }

    public SickleLevel GetNextLevel()
    {
        return _config.Levels[_currentLevel];
    }

    public void ApplyUpgrade()
    {
        if (!CanLevelUp()) return;

        _currentLevel++;
    }
}