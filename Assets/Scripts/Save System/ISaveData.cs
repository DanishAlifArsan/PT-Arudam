public interface ISaveData
{
    void NewGame(ref GameData data);
    void LoadGame(GameData data);
    void SaveGame(ref GameData data);
}
