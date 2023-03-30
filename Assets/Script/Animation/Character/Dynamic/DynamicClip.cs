using Assets.Script.Animation.Character.Dynamic;
using UnityEngine;
using UnityEngine.Playables;

public class DynamicClip:PlayableAsset
{
    public AnimationClip bipedAnimClip;
    public AnimationClip boneAnimClip;
    public AnimationClip emotionAnimClip;
    public bool isPlayBackwards=false;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var dynamicPlayable= ScriptPlayable<DynamicBehaviour>.Create(graph, 1);
        dynamicPlayable.GetBehaviour().Init(bipedAnimClip, boneAnimClip, emotionAnimClip, isPlayBackwards);
        return dynamicPlayable;
    }
}