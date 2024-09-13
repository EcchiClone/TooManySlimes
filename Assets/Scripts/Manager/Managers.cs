using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;
    public DataManager Data;
    public LobbySceneManager Lobby;
    public BattleSceneManager Battle;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Instance.Data = Instance.GetComponent<DataManager>();
        Instance.Lobby = Instance.GetComponent<LobbySceneManager>();
        Instance.Battle = Instance.GetComponent<BattleSceneManager>();

        Instance.Data.InitSetting();
    }
}