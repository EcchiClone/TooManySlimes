using System.Collections.Generic;

public static class Game
{
    public static DataManager Data => Managers.Instance.Data;
    public static LobbySceneManager Lobby => Managers.Instance.Lobby;
    public static BattleSceneManager Battle => Managers.Instance.Battle;
}
