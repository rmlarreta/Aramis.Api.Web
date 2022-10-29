namespace Aramis.Api.SecurityService.Extensions
{
    public static class ExtensionMethods
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("El valor no puede ser sólo una cadena vacía.", nameof(password));
            }

            using System.Security.Cryptography.HMACSHA512? hmac = new();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("El valor no puede ser sólo una cadena vacía.", nameof(password));
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Longitud inválida del Password.", nameof(password));
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Longitud ínválida del Password.", nameof(storedSalt));
            }

            using (System.Security.Cryptography.HMACSHA512? hmac = new(storedSalt))
            {
                byte[]? computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
