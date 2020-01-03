using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactantScript : MonoBehaviour
{   //use this to define our color and to react objects
    public abstract void React(int color);
    public abstract void Deact();
}
