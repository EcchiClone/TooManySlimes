using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;
    public BattleSceneManager Battle;
    public DataManager Data;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Instance.Battle = Instance.GetComponent<BattleSceneManager>();
        Instance.Data = Instance.GetComponent<DataManager>();
    }
}