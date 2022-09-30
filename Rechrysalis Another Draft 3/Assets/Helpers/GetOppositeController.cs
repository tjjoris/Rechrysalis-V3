using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rechrysalis
{
    public class GetOppositeController 
    {
        public static int ReturnOppositeController(int _controllerGiven)
        {
            if (_controllerGiven == 1)
            {
            return 0;
            }
            return 1;
        }
    }
}
