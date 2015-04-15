﻿using UnityEngine;
using System;
using System.Collections;
using UnityFreeFlight;


namespace UnityFreeFlight {

	[Serializable]
	public class Flapping : Mechanic {

	//	public float regularFlaptime = 0.5f;
	//	public float minimumFlapTime = 0.2f;
		public float flapStrength = 60.0f;
	//	public float downbeatStrength = 150.0f;
		AnimatorStateInfo curstate;
		public AudioClip[] sounds;


		public override void init (GameObject go, FlightPhysics fp, FlightInputs fi) {
			base.init (go, fp, fi);
			name = "Flapping Mechanic";
			animationStateName = "Flapping";
			animationStateHash = Animator.StringToHash (animationStateName);
		}

		public override bool FFInputSatisfied () {
			return flightInputs.inputFlap;
		}

		/// <summary>
		/// Override FFStart to do nothing. The Stock Begin() isn't what we want
		/// </summary>
		public override void FFStart () {}

		public override void FFFixedUpdate () {
			if (!isFlapping()) {
				soundManager.playRandomSound(sounds);
				rigidbody.AddForce (rigidbody.rotation * Vector3.up * flapStrength);
				animator.SetTrigger (animationStateHash);
			}
		}

		/// <summary>
		/// Since flapping animation is done on a trigger, we want to override the default behavior.
		/// </summary>
		public override bool FFFinish () {
			return true;
		}

		public bool isFlapping () {
			curstate = animator.GetCurrentAnimatorStateInfo (0);
			if (curstate.fullPathHash != animationStateHash) {
				return false;
			}
			return true;
		}


	}
}
