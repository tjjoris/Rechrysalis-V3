using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Rechrysalis.Controller
{
    public class CausesPushBack : MonoBehaviour
    {

        public Action<Vector2, float> playerPushBack;
        public void PushBack(float _y)
        {
                playerPushBack?.Invoke(transform.position, _y);
        }
    }
}
