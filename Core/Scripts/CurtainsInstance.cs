using UnityEngine;

namespace Curtains2D.Scripts {
    public class CurtainsInstance {
        /// <summary>
        /// If the transition has finished.
        /// </summary>
        public bool finished = true;

        /// <summary>
        /// The length of the transition (in seconds).
        /// </summary>
        public float length;

        /// <summary>
        /// How much time is left of the transition.
        /// </summary>
        public float timeLeft;

        /// <summary>
        /// The animation curve of the transition.
        /// </summary>
        public AnimationCurve cutoffCurve;

        /// <summary>
        /// If true, the transition will be reversed. So a fade-in will be a fade-out.
        /// </summary>
        public bool Reversed;
        
        /// <summary>
        /// The texture that hold the transition animation.
        /// </summary>
        public Texture transitionTexture;

        /// <summary>
        /// If true, Curtains will is red and blue to distort the image (see colored example textures).
        /// </summary>
        public bool distort;

        /// <summary>
        /// The animation of the transitions opaqueness.
        /// </summary>
        public AnimationCurve fadeCurve;

        /// <summary>
        /// The color of the transition.
        /// </summary>
        public Color transitionColor;

        /// <summary>
        /// Copies the parameters from a Curtains preset.
        /// </summary>
        /// <param name="curtainsPreset">The Curtains preset you want to copy parameters from.</param>
        public void CopyParameters(CurtainsPreset curtainsPreset) {
            length = curtainsPreset.Length;
            timeLeft = curtainsPreset.Length;

            distort = curtainsPreset.Distort;

            cutoffCurve = curtainsPreset.CutoffCurve;
            fadeCurve = curtainsPreset.FadeCurve;

            Reversed = curtainsPreset.Reversed;

            transitionTexture = curtainsPreset.TransitionTexture;
            transitionColor = curtainsPreset.TransitionColor;

            finished = false;
        }
    }
}