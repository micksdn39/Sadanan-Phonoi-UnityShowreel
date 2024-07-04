namespace Script.Player
{
    [System.Serializable]
    public class PlayerInfo
    { 
        public string playerName;
        public int profileId;

        public PlayerInfo()
        {
            playerName = "";
            profileId = 0;
        }
    }
}
