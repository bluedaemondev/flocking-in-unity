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
    void Move(ref Vector3 resultVelocity);
    
}
