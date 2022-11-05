
using Aramis.Api.Commons.ModelsDto.Customers;

namespace Aramis.Api.CustomersService.Extensions
{
    public static class ExtensionMethods
    {
        public static string ConformaCui(OpClienteInsert clienteDto, string genderName)
        {
            if (clienteDto.Cui.Length == 11)
            {
                clienteDto.Cui = clienteDto.Cui[2..^1];
            }

            string? dni = clienteDto.Cui;
            bool isNumeric = int.TryParse(clienteDto.Cui, out _);
            if (!isNumeric)
            {
                return "0";
            }

            if (clienteDto.Cui.Length < 7 || (clienteDto.Cui.Length > 8 && clienteDto.Cui.Length < 11))
            {
                return "0";
            }

            if (clienteDto.Cui.Length == 7)
            {
                clienteDto.Cui = '0' + clienteDto.Cui;
            }

            switch (genderName)
            {
                case "MASCULINO": clienteDto.Cui = "20" + clienteDto.Cui; break;
                case "FEMENINO": clienteDto.Cui = "27" + clienteDto.Cui; break;
                case "JURÍDICA": clienteDto.Cui = "30" + clienteDto.Cui; break;
                    //FALTA EXTRANJERO
            }
            int XA = Convert.ToInt32(clienteDto.Cui[..1]) * 5;
            int XB = Convert.ToInt32(clienteDto.Cui.Substring(1, 1)) * 4;
            int XC = Convert.ToInt32(clienteDto.Cui.Substring(2, 1)) * 3;
            int XD = Convert.ToInt32(clienteDto.Cui.Substring(3, 1)) * 2;
            int XE = Convert.ToInt32(clienteDto.Cui.Substring(4, 1)) * 7;
            int XF = Convert.ToInt32(clienteDto.Cui.Substring(5, 1)) * 6;
            int XG = Convert.ToInt32(clienteDto.Cui.Substring(6, 1)) * 5;
            int XH = Convert.ToInt32(clienteDto.Cui.Substring(7, 1)) * 4;
            int XI = Convert.ToInt32(clienteDto.Cui.Substring(8, 1)) * 3;
            int XJ = Convert.ToInt32(clienteDto.Cui.Substring(9, 1)) * 2;

            int X = XA + XB + XC + XD + XE + XF + XG + XH + XI + XJ;

            int Control = (11 - (X % 11)) % 11;
            if (Control == 10)
            {
                clienteDto.Cui = "23" + dni;
                XA = 10;
                XB = Convert.ToInt32(clienteDto.Cui[..1]) * 4;
                XC = Convert.ToInt32(clienteDto.Cui.Substring(2, 1)) * 3;
                XD = Convert.ToInt32(clienteDto.Cui.Substring(3, 1)) * 2;
                XE = Convert.ToInt32(clienteDto.Cui.Substring(4, 1)) * 7;
                XF = Convert.ToInt32(clienteDto.Cui.Substring(5, 1)) * 6;
                XG = Convert.ToInt32(clienteDto.Cui.Substring(6, 1)) * 5;
                XH = Convert.ToInt32(clienteDto.Cui.Substring(7, 1)) * 4;
                XI = Convert.ToInt32(clienteDto.Cui.Substring(8, 1)) * 3;
                XJ = Convert.ToInt32(clienteDto.Cui.Substring(9, 1)) * 2;
                X = XA + XB + XC + XD + XE + XF + XG + XH + XI + XJ;
                Control = (11 - (X % 11)) % 11;
            }
            clienteDto.Cui += Control.ToString();
            if (clienteDto.Cui.Length > 11)
            {
                return "0";
            }

            return clienteDto.Cui;
        }
    }
}
