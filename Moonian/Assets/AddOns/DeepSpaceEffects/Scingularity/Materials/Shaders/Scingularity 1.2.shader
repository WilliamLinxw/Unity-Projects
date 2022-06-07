//	"Scingularity" black hole shader, version 1.2 by Aaron "problemecium" Kauffman
//	Produces a simulated 3D black hole with optional accretion disk and wormhole exit view.

Shader "problemecium/Scingularity 1.2" {
Properties {
	_Exit("Wormhole Exit (Render Texture)", CUBE) = "black" {}
	[Toggle] _IsWormhole("Is Wormhole", Range(0, 1)) = 0
	_LensOnly("Lens Only", Range(0, 1)) = 0
	_Cutoff("Lensing Cutoff Value", Range(0, 0.1)) = 0.01
	_AccretionRadius("Accretion Disk Radius", float) = 10
	_AccretionTexture("Accretion Disk Texture", 2D) = "white" {}
	_Twist("Accretion Disk Twist", float) = 1
	_Temperature("Accretion Disk Base Temperature", Range(0, 4)) = 1
	_Speed("Accretion Disk Animation Speed", Range(0, 1)) = 0.1
	_Redshift("Accretion Disk Redshift Effect", Range(0, 1)) = 0.5
	[Toggle] _Flip("Flipped Projection Correction", Range(0, 1)) = 1
}
SubShader {
	Tags{"PreviewType" = "Plane" "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector"="True" "DisableBatching" = "True" "ForceNoShadowCasting" = "True"}
	Pass {
		ZTest LEqual
		ZWrite Off
		Lighting Off
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM

		#pragma target 3.0
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		// User-specified uniforms
		uniform samplerCUBE _Cube;
		uniform samplerCUBE _Exit;
		uniform half _IsWormhole;
		uniform half _LensOnly;
		uniform half _Cutoff;
		uniform half _AccretionRadius;
		uniform sampler2D _AccretionTexture;
		uniform half4 _AccretionTexture_ST;
		uniform half _Twist;
		uniform half _Temperature;
		uniform half _Speed;
		uniform half _Redshift;
		uniform half _Flip;

		struct vertexOutput {
			half4 position : SV_POSITION;
			half radius : TEXCOORD0;
			half4 p : TEXCOORD1;
		};

		vertexOutput vert(half4 input : POSITION){
			vertexOutput output;
			output.position = half4(input.x * 2, input.y * 2, 0, 1);	// Fill screen
			output.radius = distance(mul(unity_ObjectToWorld, half4(1, 1, 1, 1)).xyz, mul(unity_ObjectToWorld, half4(0, 0, 0, 1))) * 0.57735;	// Radius = diagonal of cube / sqrt(3)
			if(_Flip > 0 && _ProjectionParams.x <= 0)	output.position.y *= -1;
			output.p = input;
			return output;
		}

		half4 frag(vertexOutput input) : SV_Target{
			half4 result = half4(0, 0, 0, 0);

				// Catch configuration errors
			if(input.radius <= 0)	return result;

				// Not compatible with Orthographic projections
			if(unity_OrthoParams.w != 0)	return result;
			if(abs(UNITY_MATRIX_P[1][1]) <= 0)	return result;
			
				// Prepare the necessary variables
			half2 aspect = half2(_ScreenParams.x / _ScreenParams.y, 1);
			
				// Compute the view direction
			half3 viewDirection = half3(input.position.x / _ScreenParams.x, input.position.y / _ScreenParams.y, -1);
			viewDirection.xy = viewDirection.xy * 2 - 1;
			viewDirection.xy /= half2(UNITY_MATRIX_P[0][0], UNITY_MATRIX_P[1][1]);
			if(_Flip > 0 && _ProjectionParams.x <= 0)	viewDirection.y *= -1;
			viewDirection = mul(viewDirection, (half3x3)UNITY_MATRIX_V);
			viewDirection = normalize(viewDirection);
			half3 dir = normalize(mul(unity_ObjectToWorld, half4(0, 0, 0, 1)) - _WorldSpaceCameraPos);

				// Compute the gravitational lensing
			result.a = 1;
			half viewDistance = length(WorldSpaceViewDir(result));
			if(viewDistance <= 0)	return result;
			half3 impact3D = _WorldSpaceCameraPos + viewDirection * viewDistance;
			impact3D -= mul(unity_ObjectToWorld, result).xyz;
			impact3D /= input.radius;
			half impactParameter = dot(impact3D, impact3D) - 1;
			impactParameter += _LensOnly * 1.31831;	// 1 + 1/pi
			if(impactParameter == 0)	return result;
			half angle = 0;
			if(_IsWormhole && impactParameter < 0)	angle = acos(impactParameter) * 2 * (1 + _LensOnly);	// Exit view
			else	angle = 1 / impactParameter;	// Gravitational lens
			half3 deflected = viewDirection;
			half fringe = 1;
			if(abs(angle) > _Cutoff){
				if(_Cutoff > 0 && abs(angle) < _Cutoff * 3)	result.a = saturate(angle / _Cutoff * 0.5 - 0.5);
				if(result.a < 1)	angle *= result.a;
				half3 axis = cross(viewDirection.xyz, normalize(-impact3D));
				half cosine = cos(angle);	half sine = sin(angle);	// Compute deflected vector based on Rodrigues's Formula
				deflected = cosine * viewDirection + sine * cross(axis, viewDirection) + (1 - cosine) * (dot(axis, viewDirection)) * axis;
				if(impactParameter < 0 && _IsWormhole)	result.rgb = texCUBElod(_Exit, half4(deflected.x, deflected.y, deflected.z, 0));
				else{
					result.rgb = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, deflected, 0);
					result.rgb = DecodeHDR(result, unity_SpecCube0_HDR);
				}

					// Post-process
				fringe = saturate(impactParameter * 4 * (_IsWormhole ? sign(impactParameter) : 1));
				result.rgb *= fringe;
				if(viewDistance < input.radius * 4 && !_IsWormhole){	// Blueshift
					half brighten = viewDistance / input.radius / 4 + _LensOnly;
					if(brighten > 0 && brighten < 1){
						result.rgb /= brighten;
						if(viewDistance < input.radius)	result.rgb += lerp(0, 1, input.radius / viewDistance * pow(saturate(-dot(viewDirection, dir)), 8) * 0.25);
						result.b *= max(1, result.b / brighten);
					}
				}

			}else	result.a = 0;

			

				// Render the accretion disk
			if(_AccretionRadius > (1 - _LensOnly) * 1.4142){
				half animation = (_Time.y * _Speed) % 1;
				half r2 = _AccretionRadius * _AccretionRadius;
				for(int i = 0; i < 2; i++){
					if(i)	deflected = viewDirection;	// Note that "i" is treated as a bool within the loop
					else	deflected = lerp(deflected, viewDirection, _LensOnly);

					half3 n = half3(0, 1, 0);
					n = mul((half3x3)unity_ObjectToWorld, n);
					half d = dot(-impact3D, n) / (dot(deflected, n) == 0 ? 1 : dot(deflected, n));
					half3 intersection3D = impact3D + deflected * d;
					intersection3D /= input.radius;
					intersection3D *= 1 + _LensOnly;
					
					half2 intersection = (intersection3D).xz;	// Determine intersection point of ray with disk
					intersection = mul(transpose((half3x3)unity_ObjectToWorld), intersection3D).xz;
					half intersectionD2 = dot(intersection, intersection);

					half border = UnityObjectToClipPos(mul(transpose((half3x3)unity_ObjectToWorld), intersection3D)).z - UnityObjectToClipPos(half4(0, 0, 0, 1)).z;
					if(_Flip <= 0 || _ProjectionParams.x > 0)	border *= -1;
					if(		intersectionD2 < r2	// Within radius of disk
						&&	(impactParameter > 0 || (i * intersectionD2 > 1))	// Outside event horizon
						&&	(i == border > 0)	// On the correct side for the current rendering pass
						&&	d * input.radius > -viewDistance	// Not behind the camera
							){
						intersection /= _AccretionRadius * 2;
						intersection = half2(	(dot(intersection, intersection) + animation) % 1,
												atan2(intersection.y, intersection.x) + intersectionD2 * _Twist / r2	);
						intersection.y = ((intersection.y / 6.2832) + 1 - animation) % 1;
						intersection *= _AccretionTexture_ST.xy;
						intersection += _AccretionTexture_ST.zw;
						half4 disk = tex2Dlod(_AccretionTexture, half4(intersection.x, intersection.y, 0, 0));
						half temperature = saturate(1 - (intersectionD2 - 1) / r2);
						temperature *= temperature * max(0, _Temperature);
						
						half fringeA = 1;
						if(i)	fringeA = saturate((intersectionD2 - 2) * 4);
						else	fringeA = angle > 6.2832 ? 0 : saturate(6.2832 - angle * 6.2832) * fringe;
						temperature *= lerp(fringeA, 1, _LensOnly);

						half redshift = 1;
						if(_Redshift > 0){
							half3 cr = cross(n, dir);
							redshift = -dot(intersection3D, cr);
							redshift *= -1 + saturate(abs(dot(n, dir)));
							redshift *= sign(_Twist);
							redshift = max(1 - redshift / intersectionD2 * _Redshift * 2, 0);
							temperature *= redshift;
						}

						disk.a = saturate(disk.a * temperature * temperature * 6.25);
						disk.g *= temperature;
						disk.b *= temperature * temperature;
						if(temperature > 1){
							disk.rgb /= lerp(1, temperature * temperature - 1, disk.a);
							disk.g = (disk.g + disk.r) * 0.5;
						}else	disk.rgb = lerp(disk.rgb, disk.rgb / (1 - temperature * temperature), temperature);

						if(_Redshift > 0)	disk.rgb *= saturate(lerp(1, temperature, saturate(1 - intersectionD2 * 0.0625)));

						result.rgb = lerp(result.rgb, disk.rgb, disk.a);
						result.a = max(result.a, disk.a);
					}
				}
			}

			return saturate(result);
		}

		ENDCG
	}
}
}