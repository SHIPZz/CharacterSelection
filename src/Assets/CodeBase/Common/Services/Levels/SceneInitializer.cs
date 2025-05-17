using UnityEngine;
using Zenject;

namespace CodeBase.Common.Services.Levels
{
    public class SceneInitializer : MonoBehaviour, IInitializable
    {
      [SerializeField]  private Transform _playerSpawnPosition;
      [SerializeField]  private Transform _enemySpawnPosition;
       
      private ILevelProvider _levelProvider;

        [Inject]
        private void Construct(ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
        }
        
        public void Initialize()
        {
            _levelProvider.PlayerSpawnPosition = _playerSpawnPosition;
            _levelProvider.EnemySpawnPoint = _enemySpawnPosition;
        }
    }
}