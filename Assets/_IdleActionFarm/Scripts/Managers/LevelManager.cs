using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int _currentNumber;

    private void Start()
    {
        _currentNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(_currentNumber);
    }
}
