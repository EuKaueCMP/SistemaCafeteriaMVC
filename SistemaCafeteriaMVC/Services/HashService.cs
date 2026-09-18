namespace SistemaCafeteriaMVC.Services
{
    public static class HashService
    {
        public static byte[] Hash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                return sha256.ComputeHash(bytes);
            }
        }
    }
}
