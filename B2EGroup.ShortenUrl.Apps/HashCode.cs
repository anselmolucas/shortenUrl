using System;

namespace B2EGroup.ShortenUrl.Apps
{
    public class HashCode
    {
        /// <summary>
        /// criar um código randômico para ser usado com shortUrl
        /// </summary>
        /// <param name="_tamanho"></param>
        /// <returns></returns>
        public static string Random(int _tamanho = 4)
        {            
            string _hash_string = "abcdefghijklmnopqrstuvwxyza0123456789";
            string _hash_code = "";

            Random _random = new Random();

            for (int _i = 1; _i <= _tamanho; _i++)
            {
                int _tamanho_string = _hash_string.Length;
                int _random_num = _random.Next(_tamanho_string);

                string _aux_hash = _hash_string.Substring(_random_num, 1);
                _hash_code += _aux_hash;
            }

            return _hash_code.ToString();
        }
    }
}
