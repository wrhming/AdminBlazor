using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace AdminBlazor.Authentication
{
    /// <summary>
    /// PBKDF2
    /// DK = PBKDF2(PRF, Password, Salt, c, dkLen)
    /// PRF 是一个伪随机函数，可以简单的理解为 Hash 函数。
    /// Password 表示口令 。
    /// Salt 表示盐值，一个随机数。
    /// c 表示迭代次数。
    /// dkLen 表示最后输出的密钥长度。
    /// </summary>
    public class PasswordHasher
    {
        private static string HashPassword(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password: value,//密码
                salt: Encoding.UTF8.GetBytes(salt),//盐
                prf: KeyDerivationPrf.HMACSHA512,//伪随机函数，这里是SHA-512
                iterationCount: 10000,//迭代次数
                numBytesRequested: 256 / 8);//最后输出的秘钥长度

            return Convert.ToBase64String(valueBytes);
        }

        public static string HashPassword(string password)
        {
            //取使用“点”进行拼接存储，即使用salt.hash的形式，相应的，取值校验，再以“点”进行拆分
            var salt = GenerateSalt();
            var hash = HashPassword(password, salt);
            var result = $"{salt}.{hash}";
            Console.WriteLine("hash result:{0}", result);
            return result;
        }

        /// <summary>
        /// 验密码是否正确
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static bool Validate(string password, string salt, string hash)
            => HashPassword(password, salt) == hash;

        public static bool VerifyHashedPassword(string password, string storePassword)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrEmpty(storePassword))
            {
                throw new ArgumentNullException(nameof(storePassword));
            }

            var parts = storePassword.Split('.');
            var salt = parts[0];
            var hash = parts[1];

            return Validate(password, salt, hash); ;
        }

        /// <summary>
        /// 随机盐生成的方法
        /// </summary>
        /// <returns></returns>
        private static string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
