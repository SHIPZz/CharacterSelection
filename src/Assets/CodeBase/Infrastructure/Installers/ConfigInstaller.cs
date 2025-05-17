using CodeBase.UI.CharacterSelect.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private CharacterConfig _characterConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_characterConfig);
        }
    }
}