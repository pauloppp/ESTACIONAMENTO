using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESTACIONAMENTO.Extensoes
{
    public static partial class PublicExtensions
    {
        public static string ToFechada(this string varF)
        {
            return varF = "Fechada";
        }

        public static string ToRetorno(this string varR)
        {
            return varR = "Retorno";
        }

    }
}
