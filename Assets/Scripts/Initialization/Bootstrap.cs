using System.Collections;
using AttentionContent;
using UI;
using UnityEngine;

namespace Initialization
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private AttentionHintViewer _attentionHintViewer;
        [SerializeField]private Wallet _wallet;
        [SerializeField] private LoadingScreen _loadingScreen;
        
        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            StartCoroutine(InitializeGame());
        }

        private IEnumerator InitializeGame()
        {
            _wallet.Init();
            AttentionHintActivator.Init(_attentionHintViewer);
            _loadingScreen.Show();
            _loadingScreen.SetProgress(0.1f);
            yield return new WaitForSeconds(0.1f);
            _loadingScreen.SetProgress(0.4f);
            yield return null;
            AttentionHintActivator.Init(_attentionHintViewer);
            _loadingScreen.SetProgress(0.6f);
            yield return new WaitForSeconds(0.165f);
            _loadingScreen.SetProgress(0.9f);
            yield return new WaitForSeconds(0.3f);
            _loadingScreen.SetProgress(1f);
            yield return _loadingScreen.FadeOut();
        }
    }
}