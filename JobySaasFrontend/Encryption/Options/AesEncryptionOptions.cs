namespace JobySaasFrontend.Encryption.Options;

public class AesEncryptionOptions
{
    public const string Position = "AesEncryption";
    public string Key { get; set; }
    public string Iv { get; set; }
    public string Salt { get; set; }
    public int Iterations { get; set; }

    public byte[] KeyHash { get; private set; }

    public void HashKey(Func<int, string, byte[]> hasher)
    {
        KeyHash = hasher.Invoke(32, Key);
    }
}