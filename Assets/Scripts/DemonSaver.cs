using ExitGames.Client.Photon;
using Photon.Pun;

public class DemonSaver : MonoBehaviourPunCallbacks
{
    public static string DemonName = "";

    public static void UpdateDemonName(string newValue)
    {
        DemonName = newValue;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "DemonName", DemonName } });
        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("DemonName"))
        {
            DemonName = (string)propertiesThatChanged["DemonName"];
        }
    }
}
