using System;
using System.Collections;
using Curtains2D.Scripts;
using NaughtyAttributes;
using UnityEngine;

namespace Curtains2D {
    [ExecuteInEditMode]
    public class Curtains : MonoBehaviour {
        #region Materials + Blit

        [SerializeField] private Material curtainsMaterial;
        public static Material cMaterial;

        void OnRenderImage(RenderTexture src, RenderTexture dst) {
            if (curtainsMaterial != null)
                Graphics.Blit(src, dst, curtainsMaterial);
        }

        #endregion

        #region Material Variables

        private static readonly int CutoffId = Shader.PropertyToID("_Cutoff");

        private static float Cutoff {
            get => cMaterial.GetFloat(CutoffId);
            set => cMaterial.SetFloat(CutoffId, value);
        }

        private static readonly int FadeId = Shader.PropertyToID("_Fade");

        private static float Fade {
            get => cMaterial.GetFloat(FadeId);
            set => cMaterial.SetFloat(FadeId, value);
        }

        private static readonly int TransitionColorId = Shader.PropertyToID("_Color");

        private static Color TransitionColor {
            get => cMaterial.GetColor(TransitionColorId);
            set => cMaterial.SetColor(TransitionColorId, value);
        }

        private static readonly int DistortId = Shader.PropertyToID("_Distort");

        private static bool Distort {
            get => cMaterial.GetInt(DistortId) == 1;
            set => cMaterial.SetInt(DistortId, value ? 1 : 0);
        }


        private static readonly int TransitionTextureId = Shader.PropertyToID("_TransitionTex");

        private static Texture TransitionTexture {
            get => cMaterial.GetTexture(TransitionTextureId);
            set => cMaterial.SetTexture(TransitionTextureId, value);
        }

        #endregion

        /// <summary>
        /// True if a transition is finished or if no transition is playing.
        /// </summary>
        public static bool Finished => CCI.finished;

        [SerializeField] [Range(0, 1)] [OnValueChanged("SliderUpdate")]
        private float curtainsSlider = 0;

        private void SliderUpdate() {
            curtainsMaterial.SetFloat(CutoffId, curtainsSlider);
        }

        [HorizontalLine] [SerializeField] [Expandable]
        private CurtainsPreset testPreset;

        [Button("Test Curtains Preset", EButtonEnableMode.Playmode)]
        public void TestPreset() {
            if (testPreset) Play(testPreset);
        }

        private static readonly CurtainsInstance CCI = new CurtainsInstance();
        
        private void Awake() {
            cMaterial = curtainsMaterial ? curtainsMaterial : throw new Exception("Material not found.");
            
            Cutoff = 0;
            Fade = 0;
            CCI.CopyParameters(testPreset);
            CCI.finished = true;
            Distort = CCI.distort;
            TransitionColor = CCI.transitionColor;
            TransitionTexture = CCI.transitionTexture;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.C)) TestPreset();

            if (CCI.finished) return;
            UpdateCurtains(Time.deltaTime);
        }

        private static void UpdateCurtains(float deltaTime) {
            float p;
            if (!CCI.Reversed) {
                p = 1 - CCI.timeLeft / CCI.length;
            }
            else {
                p = CCI.timeLeft / CCI.length;
            }

            Cutoff = CCI.cutoffCurve.Evaluate(p);
            Fade = CCI.fadeCurve.Evaluate(p);


            if (CCI.timeLeft <= 0) {
                CCI.finished = true;
            }

            CCI.timeLeft -= deltaTime;
        }

        /// <summary>
        /// Plays a Curtains preset.
        /// </summary>
        /// <param name="curtainsPreset">The preset you want to play.</param>
        public static void Play(CurtainsPreset curtainsPreset) {
            if (!CCI.finished) return;
            CCI.CopyParameters(curtainsPreset);
            Distort = CCI.distort;
            TransitionColor = CCI.transitionColor;
            TransitionTexture = CCI.transitionTexture;
        }

        
        [Button("Reset Curtains")]
        private void ResetCurtains() {
            if (!CCI.finished) return;
            Cutoff = 0;
            Fade = 0;
        }

        /// <summary>
        /// Resets curtains, and cancels any running transitions.
        /// </summary>
        public void Reset() {
            CCI.finished = true;
            ResetCurtains();
        }
        /// <summary>
        /// returns when Finished is true.
        /// <para>Used in Coroutines:</para>
        /// "yield return StartCoroutine(WaitForFinished());"
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitForFinished() {
            yield return new WaitUntil(() => Finished);

        }
        
        private void OnApplicationQuit() {
            Cutoff = 0;
            Fade = 0;
            CCI.finished = true;
        }
    }
}