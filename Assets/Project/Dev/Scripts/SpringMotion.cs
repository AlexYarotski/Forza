using UnityEngine;

public static class SpringMotion
{
	public struct DampedSpringMotionParams
	{
		public float posPosCoef, posVelCoef;
		public float velPosCoef, velVelCoef;
	}

	private static DampedSpringMotionParams CalcDampedSpringMotionParams(
		float deltaTime,
		float angularFrequency,
		float dampingRatio)
	{
		const float epsilon = 0.0001f;
		DampedSpringMotionParams pOutParams;

		if (dampingRatio < 0.0f) dampingRatio = 0.0f;
		if (angularFrequency < 0.0f) angularFrequency = 0.0f;

		if (angularFrequency < epsilon)
		{
			pOutParams.posPosCoef = 1.0f;
			pOutParams.posVelCoef = 0.0f;
			pOutParams.velPosCoef = 0.0f;
			pOutParams.velVelCoef = 1.0f;
			return pOutParams;
		}

		if (dampingRatio > 1.0f + epsilon)
		{
			float za = -angularFrequency * dampingRatio;
			float zb = angularFrequency * Mathf.Sqrt(dampingRatio * dampingRatio - 1.0f);
			float z1 = za - zb;
			float z2 = za + zb;
			float e1 = Mathf.Exp(z1 * deltaTime);
			float e2 = Mathf.Exp(z2 * deltaTime);

			float invTwoZb = 1.0f / (2.0f * zb);

			float e1_Over_TwoZb = e1 * invTwoZb;
			float e2_Over_TwoZb = e2 * invTwoZb;

			float z1e1_Over_TwoZb = z1 * e1_Over_TwoZb;
			float z2e2_Over_TwoZb = z2 * e2_Over_TwoZb;

			pOutParams.posPosCoef = e1_Over_TwoZb * z2 - z2e2_Over_TwoZb + e2;
			pOutParams.posVelCoef = -e1_Over_TwoZb + e2_Over_TwoZb;

			pOutParams.velPosCoef = (z1e1_Over_TwoZb - z2e2_Over_TwoZb + e2) * z2;
			pOutParams.velVelCoef = -z1e1_Over_TwoZb + z2e2_Over_TwoZb;
		}
		else if (dampingRatio < 1.0f - epsilon)
		{
			float omegaZeta = angularFrequency * dampingRatio;
			float alpha = angularFrequency * Mathf.Sqrt(1.0f - dampingRatio * dampingRatio);

			float expTerm = Mathf.Exp(-omegaZeta * deltaTime);
			float cosTerm = Mathf.Cos(alpha * deltaTime);
			float sinTerm = Mathf.Sin(alpha * deltaTime);

			float invAlpha = 1.0f / alpha;

			float expSin = expTerm * sinTerm;
			float expCos = expTerm * cosTerm;
			float expOmegaZetaSin_Over_Alpha = expTerm * omegaZeta * sinTerm * invAlpha;

			pOutParams.posPosCoef = expCos + expOmegaZetaSin_Over_Alpha;
			pOutParams.posVelCoef = expSin * invAlpha;

			pOutParams.velPosCoef = -expSin * alpha - omegaZeta * expOmegaZetaSin_Over_Alpha;
			pOutParams.velVelCoef = expCos - expOmegaZetaSin_Over_Alpha;
		}
		else
		{
			float expTerm = Mathf.Exp(-angularFrequency * deltaTime);
			float timeExp = deltaTime * expTerm;
			float timeExpFreq = timeExp * angularFrequency;

			pOutParams.posPosCoef = timeExpFreq + expTerm;
			pOutParams.posVelCoef = timeExp;

			pOutParams.velPosCoef = -angularFrequency * timeExpFreq;
			pOutParams.velVelCoef = -timeExpFreq + expTerm;
		}

		return pOutParams;
	}

	private static void UpdateDampedSpringMotion(ref float pPos, ref float pVel, float equilibriumPos, 
		DampedSpringMotionParams parameters)
	{
		float oldPos = pPos - equilibriumPos;
		float oldVel = pVel;

		pPos = oldPos * parameters.posPosCoef + oldVel * parameters.posVelCoef + equilibriumPos;
		pVel = oldPos * parameters.velPosCoef + oldVel * parameters.velVelCoef;
	}

	public static void CalcDampedSimpleHarmonicMotion(ref float position, ref float velocity,
		float equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
	{
		var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
		UpdateDampedSpringMotion(ref position, ref velocity, equilibriumPosition, motionParams);
	}

	public static void CalcDampedSimpleHarmonicMotion(ref Vector2 position, ref Vector2 velocity,
		Vector2 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
	{
		var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
		UpdateDampedSpringMotion(ref position.x, ref velocity.x, equilibriumPosition.x, motionParams);
		UpdateDampedSpringMotion(ref position.y, ref velocity.y, equilibriumPosition.y, motionParams);
	}

	public static void CalcDampedSimpleHarmonicMotion(ref Vector3 position, ref Vector3 velocity,
		Vector3 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
	{
		var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
		UpdateDampedSpringMotion(ref position.x, ref velocity.x, equilibriumPosition.x, motionParams);
		UpdateDampedSpringMotion(ref position.y, ref velocity.y, equilibriumPosition.y, motionParams);
		UpdateDampedSpringMotion(ref position.z, ref velocity.z, equilibriumPosition.z, motionParams);
	}

	public static void CalcDampedSimpleHarmonicMotionFast(ref float position, ref float velocity,
		float equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
	{
		float x = position - equilibriumPosition;
		velocity += (-dampingRatio * velocity) - (angularFrequency * x);
		position += velocity * deltaTime;
	}

	public static void CalcDampedSimpleHarmonicMotionFast(ref Vector2 position, ref Vector2 velocity,
		Vector2 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
	{
		Vector2 x = position - equilibriumPosition;
		velocity += (-dampingRatio * velocity) - (angularFrequency * x);
		position += velocity * deltaTime;
	}

	public static void CalcDampedSimpleHarmonicMotionFast(ref Vector3 position, ref Vector3 velocity,
		Vector3 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
	{
		Vector3 x = position - equilibriumPosition;
		velocity += (-dampingRatio * velocity) - (angularFrequency * x);
		position += velocity * deltaTime;
	}
}

