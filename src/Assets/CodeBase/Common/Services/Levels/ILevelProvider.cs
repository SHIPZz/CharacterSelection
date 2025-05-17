using UnityEngine;

namespace CodeBase.Common.Services.Levels
{
    public interface ILevelProvider
    {
        Transform PlayerSpawnPosition { get; set; }
       Transform EnemySpawnPoint { get; set; }
    }
}