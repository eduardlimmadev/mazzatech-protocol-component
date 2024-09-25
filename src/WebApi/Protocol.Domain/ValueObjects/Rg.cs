using System.Text.RegularExpressions;

namespace Protocol.Domain.ValueObjects
{
    public class Rg
    {
        public string Number { get; private set; }

        public Rg(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("RG não pode ser vazio.", nameof(number));

            number = Regex.Replace(number, @"[^\d]", "");

            if (!IsValidRg(number))
                throw new ArgumentException("RG inválido.", nameof(number));

            Number = number;
        }

        private bool IsValidRg(string rg)
            => rg.Length >= 7 && rg.Length <= 11;

        public override bool Equals(object obj)
            => (obj is Rg other) ? Number == other.Number : false;

        public override int GetHashCode()
            => Number.GetHashCode();

        public override string ToString()
            => Number;
    }
}
