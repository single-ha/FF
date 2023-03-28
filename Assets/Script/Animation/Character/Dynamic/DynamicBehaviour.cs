using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Assets.Script.Animation.Character.Dynamic
{
    public class DynamicBehaviour : PlayableBehaviour
    {
        private AnimationLayerMixerPlayable mixerPlayable;

        private PlayableGraph playableGraph;

        public override void OnPlayableCreate(Playable playable)
        {
            base.OnPlayableCreate(playable);
            playableGraph = playable.GetGraph();
            mixerPlayable = AnimationLayerMixerPlayable.Create(playableGraph, 3);
            playable.ConnectInput(0, mixerPlayable, 0);
        }

        public void Init(AnimationClip bipedAnimClip, AnimationClip boneAnimClip, AnimationClip emotionAnimClip)
        {
            var clip0 = AnimationClipPlayable.Create(playableGraph, bipedAnimClip);
            var clip1 = AnimationClipPlayable.Create(playableGraph, boneAnimClip);
            var clip2 = AnimationClipPlayable.Create(playableGraph, emotionAnimClip);
            mixerPlayable.ConnectInput(0, clip0, 0);
            mixerPlayable.ConnectInput(1, clip1, 0);
            mixerPlayable.ConnectInput(2, clip2, 0);
            mixerPlayable.SetInputWeight(0, 1);
            mixerPlayable.SetInputWeight(1, 1);
            mixerPlayable.SetInputWeight(2, 1);
        }
    }
}