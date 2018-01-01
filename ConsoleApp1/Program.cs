using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// Representa uma senha no sistema, além de métodos para gerar seu hash e validá-la.
    /// </summary>
    public class Senha
    {
        private HashAlgorithm algoritmoHash;
        /// <summary>
        /// Representa o valor da senha em texto puro.
        /// </summary>
        public string Valor { get; set; }

        public Senha(HashAlgorithm hash)
        {
            if (hash == null)
            {
                throw new ArgumentNullException("Algoritmo de hash é obrigatório.");
            }
            algoritmoHash = hash;
        }

        public bool Valida(string senhaComHash)
        {
            if (string.IsNullOrEmpty(senhaComHash))
            {
                return false;
            }

            string hashedPassword = GeraValorHash(this.Valor);
            if (senhaComHash.Equals(hashedPassword, StringComparison.Ordinal))
                return true;

            return false;
        }

        public string GeraValorHash(string textoPuro)
        {
            byte[] hashValue;

            //Create a new instance of the UnicodeEncoding class to 
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] senhaEmBytes = UE.GetBytes(textoPuro);

            //Create the hash value from the array of bytes.
            hashValue = algoritmoHash.ComputeHash(senhaEmBytes);

            return UE.GetString(hashValue);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var textoPuro = "Marcia Garcia";
            var senha = new Senha(new SHA1Managed()) { Valor = textoPuro };
            Console.WriteLine($"Senha em texto puro: {senha.Valor}");
            var hash = senha.GeraValorHash(textoPuro);
            Console.WriteLine($"Senha hash: {hash}");
            Console.WriteLine($"São iguais? {senha.Valida(hash)}");
            Console.ReadKey();
        }
    }
}
