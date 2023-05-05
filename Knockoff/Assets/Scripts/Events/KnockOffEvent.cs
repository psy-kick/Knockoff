using ExitGames.Client.Photon;
using System;

public class KnockOffEvent
{
    public int playerId;
    public float expForce;
    public float radius;

    public delegate byte[] SerializeMethod(object customObject);
    public delegate object DeserializeMethod(byte[] serializedCustomObject);

    public KnockOffEvent(int playerId, float expForce, float radius)
    {
        this.playerId = playerId;
        this.expForce = expForce;
        this.radius = radius;
    }

    public static object Deserialize(byte[] data)
    {
        int playerId = BitConverter.ToInt32(data, 0);
        float expForce = BitConverter.ToSingle(data, 4);
        float radius = BitConverter.ToSingle(data, 8);
        var result = new KnockOffEvent(playerId, expForce, radius);

        return result;
    }

    public static byte[] Serialize(object customType)
    {
        var c = (KnockOffEvent)customType;
        byte[] playerIdBytes = BitConverter.GetBytes(c.playerId);
        byte[] expForceBytes = BitConverter.GetBytes(c.expForce);
        byte[] radiusBytes = BitConverter.GetBytes(c.radius);

        byte[] result = new byte[12];
        playerIdBytes.CopyTo(result, 0);
        expForceBytes.CopyTo(result, 4);
        radiusBytes.CopyTo(result, 8);

        return result;
    }

    public static void Register()
    {
        PhotonPeer.RegisterType(typeof(KnockOffEvent), (byte)'K', Serialize, Deserialize);
    }
}
