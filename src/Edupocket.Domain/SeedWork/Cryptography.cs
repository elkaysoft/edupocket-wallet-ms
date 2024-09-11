using Edupocket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Edupocket.Domain.SeedWork
{
    public class Cryptography
    {
        public class CharGenerator
        {
            private static readonly List<string> templates = new List<string> { "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ", "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", "1234567890", "1234567890abcdef", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", "abcdefghijklmnopqrstuvwxyz" };
            public static string genID(int counts, characterSet type = characterSet.NUMERIC)
            {
                char[] generated = new char[counts];
                char[] characters = templates[(int)type].ToCharArray();
                var random = new Random();
                int sampleLength = characters.Length - 1;
                for(int i = 0;i < counts; i++)
                {
                    int index = random.Next(0, sampleLength);
                    generated[i] = characters[index];
                }
                return new string(generated);
            }

            /// <summary>
            /// Define the length of characters to generate
            /// </summary>
            /// <param name="length"></param>
            /// <returns></returns>
            public static string GenerateRandomNumber(int length)
            {
                string result = string.Empty;
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                string sTempChars = string.Empty;

                Random random = new Random();

                for (int i = 0; i < length; i++)
                {
                    int p = random.Next(0, saAllowedCharacters.Length);
                    sTempChars = saAllowedCharacters[random.Next(0, saAllowedCharacters.Length)];
                    result += sTempChars;
                }

                return result;
            }



        }


    }
}
