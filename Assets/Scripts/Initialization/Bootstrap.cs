using System.Collections;
using AttentionContent;
using ClientContent;
using SpawnContent;
using UI.Screens;
using UnityEngine;

namespace Initialization
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private AttentionHintViewer _attentionHintViewer;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ClientSpawner _spawner;
        [SerializeField] private ClientQueue _clientQueue;
        [SerializeField] private float _waitTime = 0.165f;

        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_waitTime);
            Initialization();
        }

        private void Initialization()
        {
            StartCoroutine(InitializeGame());
        }

        private IEnumerator InitializeGame()
        {
            _loadingScreen.Show();
            _wallet.Init();
            AttentionHintActivator.Init(_attentionHintViewer);
            _loadingScreen.SetProgress(0.3f);
            yield return _waitForSeconds;
            _inventory.Init();
            _loadingScreen.SetProgress(0.6f);
            yield return _waitForSeconds;
            _spawner.Init();
            yield return _waitForSeconds;
            _loadingScreen.SetProgress(1f);
            yield return _loadingScreen.FadeOut();
            _clientQueue.Init();
        }
    }
}