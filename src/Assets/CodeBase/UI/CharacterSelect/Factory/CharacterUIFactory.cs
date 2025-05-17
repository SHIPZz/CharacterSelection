using CodeBase.Constants;
using CodeBase.Extensions;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Views;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.CharacterSelect.Factory
{
    public class CharacterUIFactory : ICharacterUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public CharacterUIFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public CharacterView CreateCharacterView(Transform parent, CharacterData character)
        {
            CharacterView prefab = _assetProvider.LoadAsset<CharacterView>(AssetPath.CharacterView);
            
           return _instantiator.InstantiatePrefabForComponent<CharacterView>(prefab, parent)
                .With(x => x.Init(character.TypeId, character.Icon, character.Background,character.Progress));
        }
    }
}