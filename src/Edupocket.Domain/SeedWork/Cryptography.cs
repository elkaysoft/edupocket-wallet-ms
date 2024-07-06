using Edupocket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
