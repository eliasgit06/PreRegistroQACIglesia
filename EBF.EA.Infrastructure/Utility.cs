using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QACIglesia.Infrastructure
{
    public static class Utility
    {
        public static string FormatearCedulaConGuiones(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return cedula;

            if (!isCedulaValida(cedula))
                return cedula;

            return cedula.Length != 11 ? cedula : cedula.Trim().Replace("-", "").Substring(0, 3) + "-" + cedula.Trim().Replace("-", "").Substring(3, 7) + "-" + cedula.Trim().Replace("-", "").Substring(10, 1);
        }

        public static int CalcularEdadBasadoEnCumpleanos(DateTime fechaNacimiento)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (fechaNacimiento.Year * 100 + fechaNacimiento.Month) * 100 + fechaNacimiento.Day;

            return (a - b) / 10000;
        }

        /// <summary>
        /// Valida que el número de una cédula sea válido, en cuanto
        /// al digito verificador.
        /// </summary>
        /// <param name="cedula">Cedula</param>
        /// <returns>True, si es válido, false en caso contrario</returns>
        public static bool isCedulaValida(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return false;

            cedula = cedula.Replace("-", ""); // Por si tiene guiones

            if ((cedula.Length != 11) || (!Regex.IsMatch(cedula, @"^[0-9]+$"))) // Solo números
                return false;

            string digito_verificador = cedula.Substring(cedula.Length - 1, 1); // Serie
            char[] _digitos = cedula.Substring(0, cedula.Length - 1).ToArray(); // Sin la serie
            int multiplicador = 2;
            int suma = 0;

            for (int idx = _digitos.Length - 1; idx > 0; idx--)
            {
                int _temp = Convert.ToInt32(_digitos[idx].ToString()) * multiplicador;

                if (_temp > 9) //  Más de un digito, la suma debe ser digitos individuales
                {
                    suma += (_temp % 10) + (_temp / 10); // Primer digito y segundo digito
                }
                else
                    suma += _temp;

                multiplicador = (multiplicador == 2) ? 1 : 2;
            }

            int digito_calculado = (10 - (suma % 10)) % 10;

            return ((Convert.ToInt32(digito_verificador) == digito_calculado));
        }
    }
}
