using UnityEngine;

public class Spring
{
    public class DampedSpringMotionParams
    {
        public float PosPosCoef, PosVelCoef;
        
        public float VelPosCoef, VelVelCoef;
    }
    
    public static void CalcDampedSpringMotionParams(ref DampedSpringMotionParams outParams, float angularFrequency, float dampingRatio)
    {
        const float epsilon = 0.0001f;
                                                            
        if (dampingRatio < 0.0f) dampingRatio = 0.0f;
        if (angularFrequency < 0.0f) angularFrequency = 0.0f;

        if ( angularFrequency < epsilon )
        {
            outParams.PosPosCoef = 1.0f; outParams.PosVelCoef = 0.0f;
            outParams.VelPosCoef = 0.0f; outParams.VelVelCoef = 1.0f;
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
                
            var e1OverTwoZb = e1 * invTwoZb;
            var e2OverTwoZb = e2 * invTwoZb;

            var z1e1OverTwoZb = z1 * e1OverTwoZb;
            var z2e2OverTwoZb = z2 * e2OverTwoZb;

            outParams.PosPosCoef =  e1OverTwoZb * z2 - z2e2OverTwoZb + e2;
            outParams.PosVelCoef = -e1OverTwoZb + e2OverTwoZb;

            outParams.VelPosCoef = (z1e1OverTwoZb - z2e2OverTwoZb + e2) * z2;
            outParams.VelVelCoef = -z1e1OverTwoZb + z2e2OverTwoZb;
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
            var expOmegaZetaSinOverAlpha = expTerm*omegaZeta * sinTerm * invAlpha;

            outParams.PosPosCoef = expCos + expOmegaZetaSinOverAlpha;
            outParams.PosVelCoef = expSin * invAlpha;
            
            outParams.VelPosCoef = -expSin * alpha - omegaZeta*expOmegaZetaSinOverAlpha;
            outParams.VelVelCoef =  expCos - expOmegaZetaSinOverAlpha;
        }
        else
        {
            var expTerm = Mathf.Exp(-angularFrequency * Time.deltaTime);
            var timeExp = Time.deltaTime * expTerm;
            var timeExpFreq = timeExp * angularFrequency;

            outParams.PosPosCoef = timeExpFreq + expTerm;
            outParams.PosVelCoef = timeExp;

            outParams.VelPosCoef = -angularFrequency * timeExpFreq;
            outParams.VelVelCoef = -timeExpFreq + expTerm;
        }
    }
    
    public static void UpdateDampedSpringMotion(ref float position, ref float velocity, float equilibriumPos, in DampedSpringMotionParams springParams)
    {		
        var oldPos = position - equilibriumPos;
        var oldVel = velocity;
    
        position = oldPos * springParams.PosPosCoef + oldVel * springParams.PosVelCoef + equilibriumPos;
        velocity = oldPos * springParams.VelPosCoef + oldVel * springParams.VelVelCoef;
    }
}
