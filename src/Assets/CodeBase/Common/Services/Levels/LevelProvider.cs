using UnityEngine;

namespace CodeBase.Common.Services.Levels
{
    public class LevelProvider : ILevelProvider
    {
        public Transform PlayerSpawnPosition { get;  set; }
        public Transform EnemySpawnPoint { get; set; }
    }
}