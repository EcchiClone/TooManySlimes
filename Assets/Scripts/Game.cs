using System.Collections.Generic;

public static class Game
{
    public static BattleSceneManager Battle => Managers.Instance.Battle;
    public static DataManager Data => Managers.Instance.Data;
}
