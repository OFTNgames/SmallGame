using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanShakeTheCamera 
{
     event System.Action<float, float> ShakeTheCamera;
}
