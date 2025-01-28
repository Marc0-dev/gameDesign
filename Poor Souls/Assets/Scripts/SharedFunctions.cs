using System;
using UnityEngine;
namespace SharedNamespace{ 
    public static class SharedFunctions{
        public static String FormatTimeSpan(TimeSpan t){
            return String.Concat(t.Hours,":", t.Minutes, ":", t.Seconds, ":", t.Milliseconds);
        }
        public static int BoolToInt(bool status){
            if(status == false)
                return 0;
            else
                return 1;
        }
        public static bool IntToBool(int status){
            if(status == 0)
                return false;
            else
                return true;
        }
    }
}