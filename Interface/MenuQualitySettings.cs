using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    // Quality Settings Menu.
    public class MenuQualitySettings : Element
    {
        #region FIELDS SERIALIZED
        
        [Title(label: "Settings")]

        [Tooltip("Canvas to play animations on.")]
        [SerializeField]
        private GameObject animatedCanvas;

        [Tooltip("Animation played when showing this menu.")]
        [SerializeField]
        private AnimationClip animationShow;

        [Tooltip("Animation played when hiding this menu.")]
        [SerializeField]
        private AnimationClip animationHide;

        #endregion
        
        #region FIELDS
        
        // Animation Component.
        private Animation animationComponent;
        // If true, it means that this menu is enabled and showing properly.
        private bool menuIsEnabled;

        // Main Post Processing Volume.
        private PostProcessVolume postProcessingVolume;
        // Scope Post Processing Volume.
        private PostProcessVolume postProcessingVolumeScope;

        // Depth Of Field Settings.
        private DepthOfField depthOfField;

        #endregion

        #region UNITY

        private void Start()
        {
            //Hide pause menu on start.
            animatedCanvas.GetComponent<CanvasGroup>().alpha = 0;
            //Get canvas animation component.
            animationComponent = animatedCanvas.GetComponent<Animation>();

            //Find post process volumes in scene and assign them.
            postProcessingVolume = GameObject.Find("Post Processing Volume")?.GetComponent<PostProcessVolume>();
            postProcessingVolumeScope = GameObject.Find("Post Processing Volume Scope")?.GetComponent<PostProcessVolume>();
            
            //Get depth of field setting from main post process volume.
            if(postProcessingVolume != null)
                postProcessingVolume.profile.TryGetSettings(out depthOfField);
        }

        protected override void Tick()
        {
            //Switch. Fades in or out the menu based on the cursor's state.
            bool cursorLocked = characterBehaviour.IsCursorLocked();
            switch (cursorLocked)
            {
                //Hide.
                case true when menuIsEnabled:
                    Hide();
                    break;
                //Show.
                case false when !menuIsEnabled:
                    Show();
                    break;
            }
        }

        #endregion

        #region METHODS

        // Shows the menu by playing an animation.
        private void Show()
        {
            //Enabled.
            menuIsEnabled = true;

            //Play Clip.
            animationComponent.clip = animationShow;
            animationComponent.Play();

            //Enable depth of field effect.
            if(depthOfField != null)
                depthOfField.active = true;
        }
        // Hides the menu by playing an animation.
        private void Hide()
        {
            //Disabled.
            menuIsEnabled = false;

            //Play Clip.
            animationComponent.clip = animationHide;
            animationComponent.Play();

            //Disable depth of field effect.
            if(depthOfField != null)
                depthOfField.active = false;
        }

        // Sets whether the post processing is enabled, or disabled.
        private void SetPostProcessingState(bool value = true)
        {
            //Enable/Disable the volumes.
            if(postProcessingVolume != null)
                postProcessingVolume.enabled = value;
            if(postProcessingVolumeScope != null)
                postProcessingVolumeScope.enabled = value;
        }

        // Sets the graphic quality to very low.
        public void SetQualityVeryLow()
        {
            //Set Quality.
            QualitySettings.SetQualityLevel(0);
            //Disable Post Processing.
            SetPostProcessingState(false);
        }
        // Sets the graphic quality to low.
        public void SetQualityLow()
        {
            //Set Quality.
            QualitySettings.SetQualityLevel(1);
            //Disable Post Processing.
            SetPostProcessingState(false);
        }

        // Sets the graphic quality to medium.
        public void SetQualityMedium()
        {
            //Set Quality.
            QualitySettings.SetQualityLevel(2);
            //Enable Post Processing.
            SetPostProcessingState();
        }
        // Sets the graphic quality to high.
        public void SetQualityHigh()
        {
            //Set Quality.
            QualitySettings.SetQualityLevel(3);
            //Enable Post Processing.
            SetPostProcessingState();
        }

        // Sets the graphic quality to very high.
        public void SetQualityVeryHigh()
        {
            //Set Quality.
            QualitySettings.SetQualityLevel(4);
            //Enable Post Processing.
            SetPostProcessingState();
        }
        // Sets the graphic quality to ultra.
        public void SetQualityUltra()
        {
            //Set Quality.
            QualitySettings.SetQualityLevel(5);
            //Enable Post Processing.
            SetPostProcessingState();
        }

        public void Restart()
        {
            //Path.
            string sceneToLoad = SceneManager.GetActiveScene().path;
            
            #if UNITY_EDITOR
            //Load the scene.
            UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsyncInPlayMode(sceneToLoad, new LoadSceneParameters(LoadSceneMode.Single));
            #else
            //Load the scene.
            SceneManager.LoadSceneAsync(sceneToLoad, new LoadSceneParameters(LoadSceneMode.Single));
            #endif
        }
        public void Quit()
        {
            //Quit.
            Application.Quit();
        }

        #endregion
    }
}
