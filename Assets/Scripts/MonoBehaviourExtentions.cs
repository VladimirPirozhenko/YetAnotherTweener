using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

static class MonoBehaviourExtentions
{
    static CancellationToken GetDestroyCancellationToken(this MonoBehaviour value)
    {
        // UNITY_2022_2 and newer
        return value.destroyCancellationToken;

    }

}

