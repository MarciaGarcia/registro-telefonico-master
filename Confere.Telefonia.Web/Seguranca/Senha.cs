using System;
using System.Security.Cryptography;
using System.Text;

namespace Confere.Telefonia.Web.Seguranca
{
    /// <summary>
    /// Representa uma senha no sistema, além de métodos para gerar seu hash e validá-la.
    /// </summary>
    public class Senha
    {
        private byte[] byteHashedPassword;
        private string _textoPuro;
        private HashAlgorithm algoritmoHash;
        private string ToHex(bool uppercase)
        {
            if (byteHashedPassword == null)
            {
                return string.Empty;
            }
            StringBuilder result = new StringBuilder(byteHashedPassword.Length * 2);
            for (int i = 0; i < byteHashedPassword.Length; i++)
                result.Append(byteHashedPassword[i].ToString(uppercase ? "X2" : "x2"));
            return result.ToString();
        }
        public Senha()
        {
            algoritmoHash = new SHA1Managed();
        }
        public string TextoPuro
        {
            get { return _textoPuro; }
            set
            {
                _textoPuro = value;
                if (!string.IsNullOrEmpty(_textoPuro))
                {
                    byte[] bytePassword = Encoding.UTF8.GetBytes(_textoPuro);
                    byteHashedPassword = algoritmoHash.ComputeHash(bytePassword);
                }
            }
        }
        public string TextoHash
        {
            get { return ToHex(false); }
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var s = obj as Senha;
            if (s == null) return false;
            return this.TextoHash == s.TextoHash;
        }
        public override int GetHashCode()
        {
            return this.TextoHash.GetHashCode();
        }
    }
}