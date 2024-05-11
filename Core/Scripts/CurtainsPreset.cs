using System;
using NaughtyAttributes;
using UnityEngine;

namespace Curtains2D {
    /// <summary>
    /// ScriptableObject of a Curtains preset.
    /// </summary>
    [CreateAssetMenu(fileName = "New Curtains Preset", menuName = "Curtains/Curtains Preset")]
    public class CurtainsPreset : ScriptableObject {
        private bool finished;


        [Tooltip("The length of the transition (in seconds).")] [MinValue(0.001f)] [SerializeField]
        private float length = 2f;

        [Tooltip("The animation curve of the transition.")] [CurveRange(0, 0, 1, 1, EColor.Red)] [SerializeField]
        private AnimationCurve cutoffCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [SerializeField] [Tooltip("If true, the transition will be reversed. So a fade-in will be a fade-out.")]
        private bool reversed;

        [SerializeField] [Tooltip("The texture that hold the transition animation.")]
        private Texture transitionTexture;


        [SerializeField] [Tooltip("If true, the screen will be distorted.")]
        private bool distort;

        [SerializeField]
        [Tooltip("The animation of the transitions opaqueness.")]
        [CurveRange(0, 0, 1, 1, EColor.Green)]
        private AnimationCurve fadeCurve = AnimationCurve.Linear(0, 1, 1, 1);

        [SerializeField] [Tooltip("The color of the transition.")]
        private Color transitionColor;

        /// <summary>
        /// The length of the transition (in seconds).
        /// </summary>
        public float Length {
            get => length;
            set => length = value;
        }

        /// <summary>
        /// The animation curve of the transition.
        /// </summary>
        public AnimationCurve CutoffCurve {
            get => cutoffCurve;
            set => cutoffCurve = value;
        }
        /// <summary>
        /// If true, the transition will be reversed. So a fade-in will be a fade-out.
        /// </summary>
        public bool Reversed {
            get => reversed;
            set => reversed = value;
        }

        /// <summary>
        /// The texture that hold the transition animation.
        /// </summary>
        public Texture TransitionTexture {
            get => transitionTexture;
            set => transitionTexture = value;
        }

        /// <summary>
        /// If true, Curtains will is red and blue to distort the image (see colored example textures).
        /// </summary>
        public bool Distort {
            get => distort;
            set => distort = value;
        }

        /// <summary>
        /// The animation of the transitions opaqueness.
        /// </summary>
        public AnimationCurve FadeCurve {
            get => fadeCurve;
            set => fadeCurve = value;
        }

        /// <summary>
        /// The color of the transition.
        /// </summary>
        public Color TransitionColor {
            get => transitionColor;
            set => transitionColor = value;
        }

        /// <summary>
        /// If the transition has finished.
        /// </summary>
        public bool Finished {
            get => finished;
            set => finished = value;
        }
    }
}