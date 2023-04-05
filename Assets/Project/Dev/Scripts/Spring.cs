using UnityEngine;

public class Spring
{
    // public class tDampedSpringMotionParams
    // {
    //     // newPos = posPosCoef * oldPos + posVelCoef * oldVel
    //     public float m_posPosCoef, m_posVelCoef;
    //     
    //     // newVel = velPosCoef * oldPos + velVelCoef * oldVel
    //     public float m_velPosCoef, m_velVelCoef;
    // }
    
    public static void CalcDampedSpringMotionParams(ref Vector3 position, float velocity, float angularFrequency, float dampingRatio)
    {
        const float epsilon = 0.0001f;
                                                            
        if (dampingRatio < 0.0f) dampingRatio = 0.0f;   
        if (angularFrequency < 0.0f) angularFrequency = 0.0f;

        if (angularFrequency < epsilon)
        {
            position.x = 1.0f; velocity = 0.0f;
            return;
        }

        if (dampingRatio > 1.0f + epsilon)
        {
            var za = -angularFrequency * dampingRatio;
            var zb = angularFrequency * Mathf.Sqrt(dampingRatio * dampingRatio - 1.0f);
            var z1 = za - zb;
            var z2 = za + zb;

            var e1 = Mathf.Exp(z1 * Time.deltaTime);
            var e2 = Mathf.Exp(z2 * Time.deltaTime);

            var invTwoZb = 1.0f / (2.0f * zb);
                
            var e1_Over_TwoZb = e1 * invTwoZb;
            var e2_Over_TwoZb = e2 * invTwoZb;

            var z1e1_Over_TwoZb = z1 * e1_Over_TwoZb;
            var z2e2_Over_TwoZb = z2 * e2_Over_TwoZb;

            position.x =  e1_Over_TwoZb * z2 - z2e2_Over_TwoZb + e2;
            velocity = -e1_Over_TwoZb + e2_Over_TwoZb;

            // pOutParams.m_velPosCoef = (z1e1_Over_TwoZb - z2e2_Over_TwoZb + e2) * z2;
            // pOutParams.m_velVelCoef = -z1e1_Over_TwoZb + z2e2_Over_TwoZb;
        }
        else if (dampingRatio < 1.0f - epsilon)
        {
            var omegaZeta = angularFrequency * dampingRatio;
            var alpha = angularFrequency * Mathf.Sqrt(1.0f - dampingRatio * dampingRatio);

            var expTerm = Mathf.Exp(-omegaZeta * Time.deltaTime);
            var cosTerm = Mathf.Cos(alpha * Time.deltaTime);
            var sinTerm = Mathf.Sin(alpha * Time.deltaTime);
                
            var invAlpha = 1.0f / alpha;

            var expSin = expTerm * sinTerm;
            var expCos = expTerm * cosTerm;
            var expOmegaZetaSin_Over_Alpha = expTerm*omegaZeta * sinTerm * invAlpha;

            position.x = expCos + expOmegaZetaSin_Over_Alpha;
            velocity = expSin * invAlpha;
            
            // pOutParams.m_velPosCoef = -expSin * alpha - omegaZeta*expOmegaZetaSin_Over_Alpha;
            // pOutParams.m_velVelCoef =  expCos - expOmegaZetaSin_Over_Alpha;
        }
        else
        {
            var expTerm = Mathf.Exp(-angularFrequency * Time.deltaTime);
            var timeExp = Time.deltaTime * expTerm;
            var timeExpFreq = timeExp * angularFrequency;

            position.x = timeExpFreq + expTerm;
            velocity = timeExp;

            // pOutParams.m_velPosCoef = -angularFrequency * timeExpFreq;
            // pOutParams.m_velVelCoef = -timeExpFreq + expTerm;
        }
    }
    
    // public static void UpdateDampedSpringMotion
    //     (ref float pPos,                                // position value to update
    //     ref float pVel,                                 // velocity value to update
    //     float equilibriumPos,                           // position to approach
    //     in tDampedSpringMotionParams springParams)      // motion parameters to use
    // {		
    //     var oldPos = pPos - equilibriumPos;
    //     var oldVel = pVel;
    //
    //     pPos = oldPos * springParams.m_posPosCoef + oldVel * springParams.m_posVelCoef + equilibriumPos;
    //     // pVel = oldPos * springParams.m_velPosCoef + oldVel*springParams.m_velVelCoef;
    // }
}
