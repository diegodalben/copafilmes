using System;

namespace CopaFilmes.BizLogic.Extensions
{
    public static class IntExtension
    {
        public static string ToSequenceChar(this int value)
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Número deve ser maior do que 0");

            var dividend = value;
            var column = string.Empty;

            while (dividend > 0)
            {
                var module = (dividend - 1) % 26;
                column = Convert.ToChar(65 + module) + column;
                dividend = (dividend - module) / 26;
            }

            return column;
        }
    }
}