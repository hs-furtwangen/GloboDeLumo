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
        None = 0x01,
        HasRed = 0x02,
        HasBlue = 0x04,
        HasGreen = 0x08
    }
}