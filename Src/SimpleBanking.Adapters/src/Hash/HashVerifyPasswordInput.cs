namespace SimpleBanking.Adapters.Hash;

public class HashVerifyPasswordInput
{
    public required HashPassword StoredPassword { get; set; }

    public required HashPasswordInput PasswordInput { get; set; }
}
