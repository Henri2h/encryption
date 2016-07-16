using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public class Method
    {
        /// <summary>
        /// DIfférents méthods supported to encrypt code
        /// </summary>
        public enum CryptMethod
        {
            custom,
            AES,
            clear,
            //morse
            // -> not yet, current planning to integer
        }
        /// <summary>
        /// Specify if the we must crypt the file or the line content
        /// </summary>
        public enum CryptFileMethod
        {
            file = 0,
            content = 1
        }
    }
}
