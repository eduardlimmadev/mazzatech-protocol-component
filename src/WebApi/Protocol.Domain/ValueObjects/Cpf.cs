using System.Text.RegularExpressions;

namespace Protocol.Domain.ValueObjects
{
    public class Cpf
    {
        public string Number { get; private set; }

        public Cpf(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("CPF não pode ser vazio.", nameof(number));

            number = Regex.Replace(number, @"[^\d]", "");

            if (!IsValidCpf(number))
                throw new ArgumentException("CPF inválido.", nameof(number));

            Number = number;
        }

        private bool IsValidCpf(string cpf)
        {
            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
                return false;

            int[] multiplicatorsOne = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicatorsTwo = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicatorsOne[i];
            int rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            string digit = rest.ToString();
            tempCpf += digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicatorsTwo[i];
            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit += rest.ToString();
            return cpf.EndsWith(digit);
        }

        public override bool Equals(object obj)
            => (obj is Cpf other) ? Number == other.Number : false;

        public override int GetHashCode()
            => Number.GetHashCode();

        public override string ToString()
            => Number;
    }
}
