public static class PlayerDatabase
{
    public static void SavePlayerData(string name, int record)
    {
        PlayerData.Name = name;
        PlayerData.Record = record;
        //FirebaseManager.SaveUserInfo();
    }

    public static void GetPlayerData()
    {
        //FirebaseManager.GetUserInfo();
    }
}