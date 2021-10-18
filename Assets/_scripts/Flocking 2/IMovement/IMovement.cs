using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    /// <summary>
    /// Registra en el vector el steering de este movimiento/resultado de movimiento
    /// recibe pesos de forma dinamica
    /// </summary>
    /// <param name="resultVelocity">Movement result</param>
    /// <param name="weights">float[] [w1,w2,w3,...]</param>
    /// <param name="apply">Apply force after movement ended (*check if needed*)</param>
    void Move(ref Vector3 resultVelocity, bool apply, params object[] weights);
    
}
