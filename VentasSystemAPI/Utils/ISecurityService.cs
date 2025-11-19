namespace VentasSystemAPI.Utils
{
    public interface ISecurityService
    {
        string GeneratePass();
        string HashPassword(string text);
        string CreateToken(int userId);
    }
}
