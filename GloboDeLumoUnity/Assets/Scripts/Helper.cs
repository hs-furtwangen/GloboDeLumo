using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class Helper
    {
    [Flags]
    public enum ColorStates
    {
        HasRed = 0x02,
        HasBlue = 0x04,
        HasGreen = 0x08
    }
}